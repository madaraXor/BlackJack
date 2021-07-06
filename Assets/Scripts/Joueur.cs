using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Joueur : MonoBehaviour
{
    public TextMeshProUGUI textCarte;
    public string carte1;
    public string carte2;
    public string carte3;
    //public string carte3;
    // Start is called before the first frame update
    void Start()
    {
        carte1 = "null";
        carte2 = "null";
        carte3 = "null";
    }

    // Update is called once per frame
    void Update()
    {
        textCarte.text = carte1 + " " + carte2 + " " + carte3;
    }
}
