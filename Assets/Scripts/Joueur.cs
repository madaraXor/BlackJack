using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Joueur : MonoBehaviour
{
    public GameObject ObjTextCarte;
    public TextMeshProUGUI textCarte;
    public TextMeshProUGUI textArgent;
    public TextMeshProUGUI textMise;
    public GameObject ObjTextMiseEnJeux;
    public TextMeshProUGUI textMiseEnJeux;
    public string[] mainJoueur = { "", "", "", "", "", "", "" };
    public Cartes cartes;
    public float argent = 200f;
    public float miseActuel = 0f;
    public float miseMain = 0;
    public GameManager gameManager;
    public int totalMain = 0;
    public int nbCarte;
    public bool rester;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textCarte.text = " Joueur : " + cartes.CompterMain("joueur");
        textArgent.text = "Argent : " + argent + "$";
        textMise.text = "Mise : " + miseActuel + "$";
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
            if (gameManager.debugMode)
            {
                Debug.Log("Pas assez d'argent");
            }
        }
    }

    public void Valider()
    {
        if (argent >= miseActuel)
        {
            miseMain = miseActuel;
            argent = (argent - miseActuel);
            gameManager.StartCoroutine("StartGame");
            gameManager.panelMise.SetActive(false);
            miseActuel = 0;
            textMiseEnJeux.text = "Mise en jeux : " + miseMain + "$";
            ObjTextMiseEnJeux.SetActive(true);
        }
    }

    public void AjouterGain(float gain)
    {
        argent = (argent + gain);
        if (gameManager.debugMode)
        {
            Debug.Log("Le joueur Gagne : " + gain);
        }
    }

    public void Rester()
    { 
        if (rester == false)
        {
            rester = true;
            gameManager.StartCoroutine("TirageCroupier");
        }
    }
    public void ResetMise()
    {
        miseActuel = 0;
    }
}

