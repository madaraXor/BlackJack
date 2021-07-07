using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Joueur : MonoBehaviour
{
    public TextMeshProUGUI textCarte;
    public string[] mainJoueur = {"null", "null", "null", "null"};

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textCarte.text = mainJoueur[0] + " " + mainJoueur[1] + " " + mainJoueur[2] + " " + mainJoueur[3];
    }

    public void Reset()
    {
        for (int i = 0; i < mainJoueur.Length; i++)
        {
            mainJoueur[i] = "null";
        }
    }
}
