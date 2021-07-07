using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool bustJoueur = false;
    public bool bustCroupier = false;


    // Start is called before the first frame update
    void Start()
    {
        boutonRecommencer.SetActive(false);
        boutonTirer.SetActive(false);
        boutonRester.SetActive(false);
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        joueur.Reset();
        croupier.Reset();
        cartes.Melanger();
        cartes.TirerUneCarte("croupier");
        cartes.TirerUneCarte("croupier");
        cartes.TirerUneCarte("joueur");
        cartes.TirerUneCarte("joueur");
        boutonTirer.SetActive(true);
        boutonRester.SetActive(true);
        Debug.Log("StartGame");
    }
    // Si parti perdu : Melanger
    public void GameOver()
    {
        boutonRecommencer.SetActive(true);
        boutonTirer.SetActive(false);
        boutonRester.SetActive(false);
        Debug.Log("GameOver");
    }
    public void BustJoueur()
    {
        GameOver();
        bustJoueur = true;
        Debug.Log("BustJoueur");
    }
    public void BustCroupier()
    {
        GameOver();
        bustCroupier = true;
        Debug.Log("BustCroupier");
        joueur.AjouterGain(joueur.miseMain * 2);
    }
    public void Recommencer()
    {
        bustCroupier = false;
        bustJoueur = false;
        joueur.Reset();
        croupier.Reset();
        boutonRecommencer.SetActive(false);
        Debug.Log("Recommencer");
        panelMise.SetActive(true);
    }
    public void TirageCroupier()
    {
        while (bustCroupier == false && cartes.CompterMain("croupier") < 17)
        {
            cartes.TirerUneCarte("croupier");
        }
        Debug.Log("Test des mains");
        TestVictoire();
        GameOver();
    }
    public void TestVictoire()
    {
        if (bustCroupier == false && bustJoueur == false)
        {
            if (cartes.CompterMain("joueur") > cartes.CompterMain("croupier"))
            {
                Debug.Log("Joueur Gagne , x2");
                joueur.AjouterGain(joueur.miseMain * 2);
            }
            if (cartes.CompterMain("joueur") < cartes.CompterMain("croupier"))
            {
                Debug.Log("Joueur Perd");
            }
            if (cartes.CompterMain("joueur") == cartes.CompterMain("croupier"))
            {
                Debug.Log("le Joueur ce rembourse");
                joueur.AjouterGain(joueur.miseMain);
            }
        }
    }

    
    public void BlackJackJoueur()
    {
        // Si on a un BlakJack
        GameOver();
        Debug.Log("BlackJack du Joueur");
        joueur.AjouterGain(joueur.miseMain * 2.5f);
    }

    public void BlackJackCroupier()
    {
        // Si le croupier a un BlakJack
        GameOver();
        Debug.Log("BlackJack du Croupier");
    }
    
}
