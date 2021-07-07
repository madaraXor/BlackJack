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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TirerUneCarte(string cible)
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
            bool ajouter = false;
            for (int i = 0; i < joueur.mainJoueur.Length; i++)
            {
                if (joueur.mainJoueur[i] == "null" && ajouter == false)
                {
                    joueur.mainJoueur[i] = carteTirer;
                    ajouter = true;
                }
            }
            CompterMain("joueur");
        }
        if (cible == "croupier")
        {
            bool ajouter = false;
            for (int i = 0; i < croupier.mainCroupier.Length; i++)
            {
                if (croupier.mainCroupier[i] == "null" && ajouter == false)
                {
                    croupier.mainCroupier[i] = carteTirer;
                    ajouter = true;
                }
            }
            CompterMain("croupier");
        }
    }

    public void Melanger()
    {
        cartes = cartesPlein;
    }

    // Compter la valeurs des mains
    int CompterMain(string main)
    {
        if(main == "joueur")
        {
            int totalMain = 0;
            for (int i = 0; i < joueur.mainJoueur.Length; i++)
            {
                if(joueur.mainJoueur[i] != "null")
                {
                    int valeurCarte = TestCarte(joueur.mainJoueur[i]);
                    totalMain = (totalMain + valeurCarte);
                }
            }
            Debug.Log("Total Main Joueur : " + totalMain);
            return totalMain;
        }
        if(main == "croupier")
        {
            int totalMain = 0;
            for (int i = 0; i < croupier.mainCroupier.Length; i++)
            {
                if(croupier.mainCroupier[i] != "null")
                {
                    int valeurCarte = TestCarte(croupier.mainCroupier[i]);
                    totalMain = (totalMain + valeurCarte);
                }
            }
            Debug.Log("Total Main Croupier : " + totalMain);
            return totalMain;
        }
        return 0;
    }

    // Definir Valeur des cartes
    int TestCarte(string textCarte)
    {
        if (textCarte == "1C")
        {
            return 1;
        }
        if (textCarte == "2C")
        {
            return 2;
        }
        if (textCarte == "3C")
        {
            return 3;
        }
        if (textCarte == "4C")
        {
            return 4;
        }
        if (textCarte == "5C")
        {
            return 5;
        }
        if (textCarte == "6C")
        {
            return 6;
        }
        if (textCarte == "7C")
        {
            return 7;
        }
        if (textCarte == "8C")
        {
            return 8;
        }
        if (textCarte == "9C")
        {
            return 9;
        }
        if (textCarte == "10C")
        {
            return 10;
        }
        if (textCarte == "VC")
        {
            return 11;
        }
        if (textCarte == "DC")
        {
            return 12;
        }
        if (textCarte == "RC")
        {
            return 13;
        }
        return 0;
    }

}
