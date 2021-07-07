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
    public string[] cartesPlein;
    public string[] cartes;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        joueur = joueurObj.GetComponent<Joueur>();
        croupier = croupierObj.GetComponent<Croupier>();

    }

    // Update is called once per frame
    public void TirerUneCarte(string cible)
    {
        if (CompterMain("joueur") <= 21)
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

                    }
                }
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
            Debug.Log(carteTirer);
            bool ajouter = false;
            for (int i = 0; i < croupier.mainCroupier.Length; i++)
            {
                if (croupier.mainCroupier[i] == "" && ajouter == false)
                {
                    croupier.mainCroupier[i] = carteTirer;
                    ajouter = true;
                }
            }
            //Bust Croupier
            if (CompterMain("croupier") > 21)
            {
                gameManager.BustCroupier();
            }
            if (croupier.mainCroupier[0] != "" && croupier.mainCroupier[1] != "" && croupier.mainCroupier[2] == "" && croupier.totalMain == 21)
            {
                //BlackJack Croupier
                gameManager.BlackJackCroupier();
            }
        }
    }

    public void Melanger()
    {
        Debug.Log("Melanger");
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
    int TestCarte(string textCarte, string dest)
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

}
