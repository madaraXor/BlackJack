using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    //public GameObject cartesObj;
    public Cartes cartes;
    public Joueur joueur;
    public Croupier croupier;
    // variable UI
    public GameObject boutonTirer;
    public GameObject boutonRester;
    public GameObject boutonRecommencer;
    public GameObject panelMise;
    public GameObject panelMessage;
    public TextMeshProUGUI textMessage;
    public bool bustJoueur = false;
    public bool bustCroupier = false;
    public bool debugMode;
    public bool premiereCarte;
    public bool blackJackJoueur;
    public bool blackJackCroupier;

    // Start is called before the first frame update
    void Start()
    {
        blackJackJoueur = false;
        blackJackCroupier = false;
        joueur.ObjTextMiseEnJeux.SetActive(false);
        joueur.rester = false;
        boutonRecommencer.SetActive(false);
        boutonTirer.SetActive(false);
        boutonRester.SetActive(false);
        panelMessage.SetActive(false);
        joueur.ObjTextCarte.SetActive(false);
        croupier.ObjTextCarteCroupier.SetActive(false);
        panelMise.SetActive(true);
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator StartGame()
    {
        joueur.ObjTextMiseEnJeux.SetActive(true);
        joueur.ObjTextCarte.SetActive(true);
        croupier.ObjTextCarteCroupier.SetActive(true);
        croupier.carteCroupierAff = false;
        joueur.Reset();
        croupier.Reset();
        cartes.Melanger();
        premiereCarte = true;
        yield return new WaitForSeconds(0.3f);
        cartes.StartCoroutine("TirerUneCarte", "croupier");
        yield return new WaitForSeconds(0.6f);
        cartes.StartCoroutine("TirerUneCarte", "croupier");
        yield return new WaitForSeconds(0.6f);
        cartes.StartCoroutine("TirerUneCarte", "joueur");
        yield return new WaitForSeconds(0.6f);
        cartes.StartCoroutine("TirerUneCarte", "joueur");
        yield return new WaitForSeconds(0.3f);
        if (blackJackJoueur == false && blackJackCroupier == false)
        {
            boutonTirer.SetActive(true);
            boutonRester.SetActive(true);
        }
        if (debugMode)
        {
            Debug.Log("StartGame");
        }
    }
    // Si parti perdu : Melanger
    public void GameOver()
    {
        boutonRecommencer.SetActive(true);
        if (debugMode)
        {
            Debug.Log("GameOver");
        }
    }
    public void BustJoueur()
    {
        boutonTirer.SetActive(false);
        boutonRester.SetActive(false);
        AfficherMessage("Le Joueur Bust");
        bustJoueur = true;
        if (debugMode)
        {
            Debug.Log("BustJoueur");
        }
    }
    public void BustCroupier()
    {
        boutonTirer.SetActive(false);
        boutonRester.SetActive(false);
        AfficherMessage("Le Croupier Bust");
        bustCroupier = true;
        if (debugMode)
        {
            Debug.Log("BustCroupier");
        }
        joueur.AjouterGain(joueur.miseMain * 2);
    }
    public void Recommencer()
    {
        blackJackJoueur = false;
        blackJackCroupier = false;
        joueur.ObjTextMiseEnJeux.SetActive(false);
        joueur.rester = false;
        cartes.SuppCarteSpawn();
        bustCroupier = false;
        bustJoueur = false;
        joueur.Reset();
        croupier.Reset();
        boutonRecommencer.SetActive(false);
        if (debugMode)
        {
            Debug.Log("Recommencer");
        }
        panelMise.SetActive(true);
        joueur.ObjTextCarte.SetActive(false);
        croupier.ObjTextCarteCroupier.SetActive(false);
    }
    public IEnumerator TirageCroupier()
    {
        boutonTirer.SetActive(false);
        boutonRester.SetActive(false);
        // activer animation pour retourner premiere carte du croupier
        cartes.listeCarteSpawn[0].GetComponent<Animator>().SetInteger("nbAnimCroupier", 0);
        croupier.carteCroupierAff = true;
        yield return new WaitForSeconds(0.6f);
        while (bustCroupier == false && cartes.CompterMain("croupier") < 17) // croupier.totalMain
        {
            cartes.StartTirerUneCarte("croupier");
            yield return new WaitForSeconds(0.5f);
        }
        if (debugMode)
        {
            Debug.Log("Test des mains");
        }
        TestVictoire();
  
    }
    public void TestVictoire()
    {
        if (bustCroupier == false && bustJoueur == false)
        {
            if (cartes.CompterMain("joueur") > cartes.CompterMain("croupier"))
            {
                if (debugMode)
                {
                    Debug.Log("Joueur Gagne , x2");
                }
                joueur.AjouterGain(joueur.miseMain * 2);
                AfficherMessage("Le Joueur Gagne !");
            }
            if (cartes.CompterMain("joueur") < cartes.CompterMain("croupier"))
            {
                if (debugMode)
                {
                    Debug.Log("Joueur Perd");
                }
                AfficherMessage("Le Joueur Perd !");
            }
            if (cartes.CompterMain("joueur") == cartes.CompterMain("croupier"))
            {
                if (debugMode)
                {
                    Debug.Log("le Joueur se rembourse");
                }
                AfficherMessage("Le Joueur se rembourse");
                joueur.AjouterGain(joueur.miseMain);
            }
        }
    }


    public void BlackJackJoueur()
    {
        // Si on a un BlakJack
        blackJackJoueur = true;
        boutonTirer.SetActive(false);
        boutonRester.SetActive(false);
        AfficherMessage("BlackJack du Joueur");
        if (debugMode)
        {
            Debug.Log("BlackJack du Joueur");
        }
        joueur.AjouterGain(joueur.miseMain * 2.5f);
    }

    public IEnumerator BlackJackCroupier()
    {
        // Si le croupier a un BlakJack
        blackJackCroupier = true;
        boutonTirer.SetActive(false);
        boutonRester.SetActive(false);
        cartes.listeCarteSpawn[0].GetComponent<Animator>().SetInteger("nbAnimCroupier", 0);
        croupier.carteCroupierAff = true;
        yield return new WaitForSeconds(0.5f);
        AfficherMessage("BlackJack du Croupier");
        if (debugMode)
        {
            Debug.Log("BlackJack du Croupier");
        }
    }

    public void AfficherMessage(string textAfficher)
    {
        joueur.ObjTextMiseEnJeux.SetActive(false);
        panelMessage.SetActive(true);
        textMessage.text = textAfficher;
    }
    public void FermerMessage()
    {
        panelMessage.SetActive(false);
        textMessage.text = "";
        GameOver();
    }
}
