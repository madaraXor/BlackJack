using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Joueur : MonoBehaviour
{
    public TextMeshProUGUI textCarte;
    public TextMeshProUGUI textargent;
    public TextMeshProUGUI textMise;
    public string[] mainJoueur = { "", "", "", "", "", "", "" };
    public Cartes cartes;
    public float argent = 200f;
    public float miseActuel = 0f;
    public float miseMain = 0;
    public GameManager gameManager;
    public int totalMain = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textCarte.text = mainJoueur[0] + " " + mainJoueur[1] + " " + mainJoueur[2] + " " + mainJoueur[3] + " " + mainJoueur[4] + " " + mainJoueur[5] + " " + mainJoueur[6] + " Total : " + cartes.CompterMain("joueur");
        textargent.text = "argent : " + argent + "$";
        textMise.text = "Mise : " + miseActuel;
    }

    public void Reset()
    {
        for (int i = 0; i < mainJoueur.Length; i++)
        {
            mainJoueur[i] = "";
        }
    }

    public void Miser(int mise)
    {
        if (argent >= (miseActuel + mise))
        {
            miseActuel = (miseActuel + mise);
        }
        else
        {
            Debug.Log("Pas assez d'argent");
        }
    }

    public void Valider()
    {
        if (argent >= miseActuel)
        {
            miseMain = miseActuel;
            argent = (argent-miseActuel);
            gameManager.StartGame();
            gameManager.panelMise.SetActive(false);
            miseActuel = 0;
        }
    }

    public void AjouterGain(float gain)
    {
        argent = (argent+gain);
        Debug.Log("Le joueur Gagne : " + gain);
    }

    public void Rester()
    {
        gameManager.TirageCroupier();
    }
}

