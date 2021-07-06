using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartes : MonoBehaviour
{
    public GameObject joueurObj;
    public Joueur joueur;
    public GameObject croupierObj;
    public Croupier croupier;

    // V = valet, D = dame, R = roi
    // C = coeur, K = carreau, P = pique, T = trefle
    public string[] cartesPlein = {"1C", "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "10C", "VC", "DC", "RC"};
    public string[] cartes = {"1C", "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "10C", "VC", "DC", "RC"};

    // Start is called before the first frame update
    void Start()
    {
        joueur = joueurObj.GetComponent<Joueur>();
        croupier = croupierObj.GetComponent<Croupier>();
        TirerUneCarte("croupier");
        TirerUneCarte("croupier");
        TirerUneCarte("joueur");
        TirerUneCarte("joueur");
        //Melanger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TirerUneCarte(string cible)
    {
        string carteTirer = "null";
        int choixCarte = 0;
        while(carteTirer=="null")
        {
            choixCarte = Random.Range(0, 12);
            carteTirer = cartes[choixCarte];
        }
        cartes[choixCarte] = "null";
        Debug.Log(carteTirer);
        if (cible == "joueur")
        {
            if (joueur.carte1 == "null")
            {
                joueur.carte1 = carteTirer;
            }
            else if (joueur.carte2 == "null")
            {
                joueur.carte2 = carteTirer;
            }
            else if (joueur.carte3 == "null")
            {
                joueur.carte3 = carteTirer;
            }
        }
        if (cible == "croupier")
        {
            if (croupier.carteCroupier1 == "null")
            {
                croupier.carteCroupier1 = carteTirer;
            }
            else if (croupier.carteCroupier2 == "null")
            {
                croupier.carteCroupier2 = carteTirer;
            }
            else if (croupier.carteCroupier3 == "null")
            {
                croupier.carteCroupier3 = carteTirer;
            }
        }
    }

    void Melanger()
    {
        cartes = cartesPlein;
    }
}
