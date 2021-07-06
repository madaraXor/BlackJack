using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Croupier : MonoBehaviour
{
    public TextMeshProUGUI textCarteCroupier;
    public string carteCroupier1;
    public string carteCroupier2;
    public string carteCroupier3;
    // Start is called before the first frame update
    void Start()
    {
        carteCroupier1 = "null";
        carteCroupier2 = "null";
        carteCroupier3 = "null";
    }

    // Update is called once per frame
    void Update()
    {
        textCarteCroupier.text = carteCroupier1 + " " + carteCroupier2 + " " + carteCroupier3;
    }
}
