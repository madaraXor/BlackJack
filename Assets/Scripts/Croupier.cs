using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Croupier : MonoBehaviour
{
    public TextMeshProUGUI textCarteCroupier;
    public string[] mainCroupier = {"null", "null", "null", "null"};
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textCarteCroupier.text = mainCroupier[0] + " " + mainCroupier[1] + " " + mainCroupier[2] + " " + mainCroupier[3];
    }

    public void Reset()
    {
        for (int i = 0; i < mainCroupier.Length; i++)
        {
            mainCroupier[i] = "null";
        }
    }

}
