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


    // Start is called before the first frame update
    void Start()
    {
        boutonRecommencer.SetActive(false);
        //cartes = cartesObj.GetComponent<Cartes>();
        GameOver();
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
        Debug.Log("StartGame");
    }
    // Si parti perdu : Melanger
    public void GameOver()
    {
        joueur.Reset();
        croupier.Reset();
        cartes.Melanger();
        boutonRecommencer.SetActive(true);
        Debug.Log("GameOver");
    }
    public void Bust()
    {
        joueur.Reset();
        croupier.Reset();
        cartes.Melanger();
        Debug.Log("Buste");
    }
    public void Recommencer()
    {
        boutonRecommencer.SetActive(false);
        StartGame();
        Debug.Log("Recommencer");
    }
    /*
    public void BlackJack();
    {
        // Si on a un BlakJack
    }
    */
}
