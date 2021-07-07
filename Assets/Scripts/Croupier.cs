using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Croupier : MonoBehaviour
{
    public TextMeshProUGUI textCarteCroupier;
    public string[] mainCroupier = {"", "", "", "", "", "", ""};
    public Cartes cartes;
    public int totalMain = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textCarteCroupier.text = mainCroupier[0] + " " + mainCroupier[1] + " " + mainCroupier[2] + " " + mainCroupier[3] + " " + mainCroupier[4] + " " + mainCroupier[5] + " " + mainCroupier[6] + " Total : " + cartes.CompterMain("croupier");
    }

    public void Reset()
    {
        for (int i = 0; i < mainCroupier.Length; i++)
        {
            mainCroupier[i] = "";
        }
    }

}
