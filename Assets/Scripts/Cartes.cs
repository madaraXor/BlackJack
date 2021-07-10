using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartes : MonoBehaviour
{
    public GameObject joueurObj;
    public Joueur joueur;
    public GameObject croupierObj;
    public Croupier croupier;
    //public GameObject croupierObj;
    public GameManager gameManager;

    // V = valet, D = dame, R = roi
    // C = coeur, K = carreau, P = pique, T = trefle
    public string[] cartes;
    public string[] cartesPlein;
    public string carteString;
    public int depart;
    public GameObject[] carteCoeur;
    public Transform carteRetourner;
    public Transform spawnerCartes;
    public int nbArrayCarte;
    public GameObject[] listeCarteSpawn;
    public int nbArrayListeCarteSpawn = 0;
    public AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        CreerPacket();
    }

    void Awake()
    {
        joueur = joueurObj.GetComponent<Joueur>();
        croupier = croupierObj.GetComponent<Croupier>();

    }

    public void StartTirerUneCarte(string cible)
    {
        StartCoroutine("TirerUneCarte", cible);
    }
    // Update is called once per frame
    public IEnumerator TirerUneCarte(string cible)
    {
        audioManager.Play("Carte");
        yield return new WaitForSeconds(0.4f);
        if (CompterMain("joueur") <= 21 && joueur.rester == false)
        {
            if (cible == "joueur")
            {
                string carteTirer = "";
                int choixCarte = 0;
                while (carteTirer == "")
                {
                    choixCarte = Random.Range(0, (cartes.Length - 1));
                    carteTirer = cartes[choixCarte];
                }
                cartes[choixCarte] = "";
                bool ajouter = false;
                for (int i = 0; i < joueur.mainJoueur.Length; i++)
                {
                    if (joueur.mainJoueur[i] == "" && ajouter == false)
                    {
                        joueur.mainJoueur[i] = carteTirer;
                        ajouter = true;
                        joueur.nbCarte = (i + 1);

                    }
                }
                //Spawn de la carte
                SpawnCarte(carteTirer, "joueur");
                //Bust Joueur
                if (CompterMain("joueur") > 21)
                {
                    gameManager.BustJoueur();
                }
                if (joueur.mainJoueur[0] != "" && joueur.mainJoueur[1] != "" && joueur.mainJoueur[2] == "" && joueur.totalMain == 21)
                {
                    //BlackJack Joueur
                    gameManager.BlackJackJoueur();
                }
            }
        }
        if (cible == "croupier")
        {
            string carteTirer = "";
            int choixCarte = 0;
            while (carteTirer == "")
            {
                choixCarte = Random.Range(0, (cartes.Length - 1));
                carteTirer = cartes[choixCarte];
            }
            cartes[choixCarte] = "";
            if (gameManager.debugMode)
            {
                Debug.Log(carteTirer);
            }
            bool ajouter = false;
            for (int i = 0; i < croupier.mainCroupier.Length; i++)
            {
                if (croupier.mainCroupier[i] == "" && ajouter == false)
                {
                    croupier.mainCroupier[i] = carteTirer;
                    ajouter = true;
                    croupier.nbCarte = (i + 1);
                    Debug.Log("Carte Incrementer");
                    CompterMain("croupier");
                }
            }
            //Spawn de la carte
            SpawnCarte(carteTirer, "croupier");
            //Bust Croupier
            if (CompterMain("croupier") > 21)
            {
                gameManager.BustCroupier();
            }
            if (croupier.mainCroupier[0] != "" && croupier.mainCroupier[1] != "" && croupier.mainCroupier[2] == "" && croupier.totalMain == 21)
            {
                //BlackJack Croupier
                gameManager.StartCoroutine("BlackJackCroupier");
            }
        }
    }

    public void Melanger()
    {
        if (gameManager.debugMode)
        {
            Debug.Log("Melanger");
        }
        for (int i = 0; i < cartes.Length; i++)
        {
            cartes[i] = cartesPlein[i];
        }
    }



    // Compter la valeurs des mains
    public int CompterMain(string main)
    {
        if (main == "joueur")
        {
            int totalMain = 0;
            for (int i = 0; i < joueur.mainJoueur.Length; i++)
            {
                if (joueur.mainJoueur[i] != "null")
                {
                    int valeurCarte = 0;
                    totalMain = (totalMain + valeurCarte);
                    joueur.totalMain = totalMain;
                    valeurCarte = TestCarte(joueur.mainJoueur[i], "joueur");
                    totalMain = (totalMain + valeurCarte);
                    joueur.totalMain = totalMain;
                }
            }
            //Debug.Log("Total Main Joueur : " + totalMain);
            return totalMain;
        }
        if (main == "croupier")
        {
            int totalMain = 0;
            for (int i = 0; i < croupier.mainCroupier.Length; i++)
            {
                if (croupier.mainCroupier[i] != "null")
                {
                    int valeurCarte = 0;
                    totalMain = (totalMain + valeurCarte);
                    croupier.totalMain = totalMain;
                    valeurCarte = TestCarte(croupier.mainCroupier[i], "croupier");
                    totalMain = (totalMain + valeurCarte);
                    croupier.totalMain = totalMain;
                }
            }
            //Debug.Log("Total Main Croupier : " + totalMain);
            return totalMain;
        }
        return 0;
    }

    public string TestGagnant(int valMainJoueur, int valMainCroupier)
    {
        if (valMainJoueur >= valMainCroupier)
        {
            return "joueur";
        }
        else
        {
            return "croupier";
        }

    }

    // Definir Valeur des cartes
    public int TestCarte(string textCarte, string dest)
    {
        for (int i = 1; i < 10; i++)
        {
            if (textCarte.Contains(i.ToString()) && textCarte.Contains("10"))
            {
                return 10;
            }
            else if (textCarte.Contains("1") && textCarte.Contains("10") == false)
            {
                if (dest == "joueur")
                {
                    if ((joueur.totalMain + 11) > 21)
                    {
                        return 1;
                    }
                    else
                    {
                        return 11;
                    }
                }
                if (dest == "croupier")
                {
                    if ((croupier.totalMain + 11) > 21)
                    {
                        return 1;
                    }
                    else
                    {
                        return 11;
                    }
                }
            }
            else if (textCarte.Contains(i.ToString()))
            {
                return i;
            }
            else if (textCarte.Contains("V") || textCarte.Contains("D") || textCarte.Contains("R"))
            {
                return 10;
            }
        }
        return 0;
    }

    public void CreerPacket()
    {
        depart = 1;
        int chiffreCarte;
        for (int o = 0; o < 6; o++)
        {
            for (int i = depart; i < (52 + depart); i++)
            {
                chiffreCarte = 0;
                if (i <= (13 + depart - 1)) // C
                {
                    chiffreCarte = (i - depart + 1);
                    if (chiffreCarte == 11)
                    {
                        carteString = "VC";
                    }
                    else if (chiffreCarte == 12)
                    {
                        carteString = "DC";
                    }
                    else if (chiffreCarte == 13)
                    {
                        carteString = "RC";
                    }
                    else
                    {
                        carteString = chiffreCarte.ToString() + "C";
                    }
                    cartes[(i - 1)] = carteString;
                }
                else if (i < (27 + depart - 1)) // P
                {
                    chiffreCarte = (i - 13 - depart + 1);
                    if (chiffreCarte == 11)
                    {
                        carteString = "VP";
                    }
                    else if (chiffreCarte == 12)
                    {
                        carteString = "DP";
                    }
                    else if (chiffreCarte == 13)
                    {
                        carteString = "RP";
                    }
                    else
                    {
                        carteString = chiffreCarte.ToString() + "P";
                    }
                    cartes[(i - 1)] = carteString;
                }
                else if (i < (40 + depart - 1)) // K
                {
                    chiffreCarte = (i - 26 - depart + 1);
                    if (chiffreCarte == 11)
                    {
                        carteString = "VK";
                    }
                    else if (chiffreCarte == 12)
                    {
                        carteString = "DK";
                    }
                    else if (chiffreCarte == 13)
                    {
                        carteString = "RK";
                    }
                    else
                    {
                        carteString = chiffreCarte.ToString() + "K";
                    }
                    cartes[(i - 1)] = carteString;
                }
                else if (i < (53 + depart - 1)) // T
                {
                    chiffreCarte = (i - 39 - depart + 1);
                    if (chiffreCarte == 11)
                    {
                        carteString = "VT";
                    }
                    else if (chiffreCarte == 12)
                    {
                        carteString = "DT";
                    }
                    else if (chiffreCarte == 13)
                    {
                        carteString = "RT";
                    }
                    else
                    {
                        carteString = chiffreCarte.ToString() + "T";
                    }
                    cartes[(i - 1)] = carteString;
                }

            }
            depart = (depart + 52);
        }
        cartesPlein = cartes;
    }

    public void SpawnCarte(string carteSpawn, string cible)
    {
        for (int i = 2; i < 10; i++)
        {
            if (carteSpawn.Contains(i.ToString()))
            {
                nbArrayCarte = (i - 1);
            }
        }
        if (carteSpawn.Contains("1") && carteSpawn.Contains("10") == false)
        {
            nbArrayCarte = 0;
        }
        if (carteSpawn.Contains("10"))
        {
            nbArrayCarte = 9;
        }
        if (carteSpawn.Contains("V"))
        {
            nbArrayCarte = 10;
        }
        if (carteSpawn.Contains("D"))
        {
            nbArrayCarte = 11;
        }
        if (carteSpawn.Contains("R"))
        {
            nbArrayCarte = 12;
        }
        if (carteSpawn.Contains("C"))
        {
            // Instancier les Coeurs
            if (gameManager.premiereCarte)
            {
                GameObject cartePrefabSpawn =
                //autre carte retoruner
                Instantiate(carteCoeur[nbArrayCarte], spawnerCartes.transform.position, carteRetourner.transform.rotation) as GameObject;
                Animator cartePrefabSpawnAnimator = cartePrefabSpawn.GetComponent<Animator>();
                listeCarteSpawn[nbArrayListeCarteSpawn] = cartePrefabSpawn;
                nbArrayListeCarteSpawn = (nbArrayListeCarteSpawn + 1);
                cartePrefabSpawnAnimator.SetInteger("nbAnimCroupier", croupier.nbCarte);
                gameManager.premiereCarte = false;
            }
            else
            {
                GameObject cartePrefabSpawn =
                Instantiate(carteCoeur[nbArrayCarte], spawnerCartes.transform.position, carteCoeur[2].GetComponent<Transform>().transform.rotation) as GameObject;
                Animator cartePrefabSpawnAnimator = cartePrefabSpawn.GetComponent<Animator>();
                listeCarteSpawn[nbArrayListeCarteSpawn] = cartePrefabSpawn;
                nbArrayListeCarteSpawn = (nbArrayListeCarteSpawn + 1);
                if (cible == "joueur")
                {
                    cartePrefabSpawnAnimator.SetInteger("nbAnimJoueur", joueur.nbCarte);
                }
                else if (cible == "croupier")
                {
                    cartePrefabSpawnAnimator.SetInteger("nbAnimCroupier", croupier.nbCarte);
                }
            }
        }
        if (carteSpawn.Contains("P"))
        {
            // Instancier les Piques
            if (gameManager.premiereCarte)
            {
                GameObject cartePrefabSpawn =
                //autre carte retoruner
                Instantiate(carteCoeur[nbArrayCarte], spawnerCartes.transform.position, carteRetourner.transform.rotation) as GameObject;
                Animator cartePrefabSpawnAnimator = cartePrefabSpawn.GetComponent<Animator>();
                listeCarteSpawn[nbArrayListeCarteSpawn] = cartePrefabSpawn;
                nbArrayListeCarteSpawn = (nbArrayListeCarteSpawn + 1);
                cartePrefabSpawnAnimator.SetInteger("nbAnimCroupier", croupier.nbCarte);
                gameManager.premiereCarte = false;
            }
            else
            {
                GameObject cartePrefabSpawn =
                Instantiate(carteCoeur[nbArrayCarte], spawnerCartes.transform.position, carteCoeur[2].GetComponent<Transform>().transform.rotation) as GameObject;
                Animator cartePrefabSpawnAnimator = cartePrefabSpawn.GetComponent<Animator>();
                listeCarteSpawn[nbArrayListeCarteSpawn] = cartePrefabSpawn;
                nbArrayListeCarteSpawn = (nbArrayListeCarteSpawn + 1);
                if (cible == "joueur")
                {
                    cartePrefabSpawnAnimator.SetInteger("nbAnimJoueur", joueur.nbCarte);
                }
                else if (cible == "croupier")
                {
                    cartePrefabSpawnAnimator.SetInteger("nbAnimCroupier", croupier.nbCarte);
                }
            }
        }
        if (carteSpawn.Contains("K"))
        {
            // Instancier les Careaux
            if (gameManager.premiereCarte)
            {
                GameObject cartePrefabSpawn =
                //autre carte retoruner
                Instantiate(carteCoeur[nbArrayCarte], spawnerCartes.transform.position, carteRetourner.transform.rotation) as GameObject;
                Animator cartePrefabSpawnAnimator = cartePrefabSpawn.GetComponent<Animator>();
                listeCarteSpawn[nbArrayListeCarteSpawn] = cartePrefabSpawn;
                nbArrayListeCarteSpawn = (nbArrayListeCarteSpawn + 1);
                cartePrefabSpawnAnimator.SetInteger("nbAnimCroupier", croupier.nbCarte);
                gameManager.premiereCarte = false;
            }
            else
            {
                GameObject cartePrefabSpawn =
                Instantiate(carteCoeur[nbArrayCarte], spawnerCartes.transform.position, carteCoeur[2].GetComponent<Transform>().transform.rotation) as GameObject;
                Animator cartePrefabSpawnAnimator = cartePrefabSpawn.GetComponent<Animator>();
                listeCarteSpawn[nbArrayListeCarteSpawn] = cartePrefabSpawn;
                nbArrayListeCarteSpawn = (nbArrayListeCarteSpawn + 1);
                if (cible == "joueur")
                {
                    cartePrefabSpawnAnimator.SetInteger("nbAnimJoueur", joueur.nbCarte);
                }
                else if (cible == "croupier")
                {
                    cartePrefabSpawnAnimator.SetInteger("nbAnimCroupier", croupier.nbCarte);
                }
            }
        }
        if (carteSpawn.Contains("T"))
        {
            // Instancier les Trefles
            if (gameManager.premiereCarte)
            {
                GameObject cartePrefabSpawn =
                //autre carte retoruner
                Instantiate(carteCoeur[nbArrayCarte], spawnerCartes.transform.position, carteRetourner.transform.rotation) as GameObject;
                Animator cartePrefabSpawnAnimator = cartePrefabSpawn.GetComponent<Animator>();
                listeCarteSpawn[nbArrayListeCarteSpawn] = cartePrefabSpawn;
                nbArrayListeCarteSpawn = (nbArrayListeCarteSpawn + 1);
                cartePrefabSpawnAnimator.SetInteger("nbAnimCroupier", croupier.nbCarte);
                gameManager.premiereCarte = false;
            }
            else
            {
                GameObject cartePrefabSpawn =
                Instantiate(carteCoeur[nbArrayCarte], spawnerCartes.transform.position, carteCoeur[2].GetComponent<Transform>().transform.rotation) as GameObject;
                Animator cartePrefabSpawnAnimator = cartePrefabSpawn.GetComponent<Animator>();
                listeCarteSpawn[nbArrayListeCarteSpawn] = cartePrefabSpawn;
                nbArrayListeCarteSpawn = (nbArrayListeCarteSpawn + 1);
                if (cible == "joueur")
                {
                    cartePrefabSpawnAnimator.SetInteger("nbAnimJoueur", joueur.nbCarte);
                }
                else if (cible == "croupier")
                {
                    cartePrefabSpawnAnimator.SetInteger("nbAnimCroupier", croupier.nbCarte);
                }
            }
        }
    }

    public void SuppCarteSpawn()
    {
        for (int i = 0; i < listeCarteSpawn.Length; i++)
        {
            Destroy(listeCarteSpawn[i]);
        }
        nbArrayListeCarteSpawn = 0;
    }
}
