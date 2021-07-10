using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Croupier : MonoBehaviour
{
    public GameObject ObjTextCarteCroupier;
    public TextMeshProUGUI textCarteCroupier;
    public string[] mainCroupier = { "", "", "", "", "", "", "" };
    public Cartes cartes;
    public int totalMain = 0;
    public int nbCarte;
    public bool carteCroupierAff;   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (carteCroupierAff)
        {
            textCarteCroupier.text = " Croupier : " + cartes.CompterMain("croupier");
        }
        else
        {
            if ((cartes.TestCarte(mainCroupier[1], "croupier") + cartes.TestCarte(mainCroupier[2], "croupier") + cartes.TestCarte(mainCroupier[3], "croupier") + cartes.TestCarte(mainCroupier[4], "croupier")) == 1)
            {
                textCarteCroupier.text = " Croupier : " + 11;
            }
            else
            {
                textCarteCroupier.text = " Croupier : " + (cartes.TestCarte(mainCroupier[1], "croupier") + cartes.TestCarte(mainCroupier[2], "croupier") + cartes.TestCarte(mainCroupier[3], "croupier") + cartes.TestCarte(mainCroupier[4], "croupier"));
            }
        }
    }

    public void Reset()
    {
        for (int i = 0; i < mainCroupier.Length; i++)
        {
            mainCroupier[i] = "";
        }
    }

}
