using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variablesNPCMundoAbierto : MonoBehaviour
{
    public int sumaYaHablaron = 0;
    public bool yaHablo1 = false, yaHablo2 = false, yaHablo3 = false;
    public int habloNPC1;
    public int habloNPC2;
    public int habloNPC3;
    void Start()
    {
        habloNPC1 = 0;
        habloNPC2 = 0;
        habloNPC3 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(habloNPC1 + habloNPC2 + habloNPC3);
    }
}
