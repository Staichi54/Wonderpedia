using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlDialogos : MonoBehaviour
{
    public variablesNPC variNPC;

    public void verificarQnHablo(string cualNPC){
        //Test Purpose
        if(cualNPC == "npc1"){
            variNPC.habloNPC1 = 1;
        }
        if(cualNPC == "npc2"){
            variNPC.habloNPC2 = 1;
        }

        //Ahora si, las que son
        if(cualNPC == "profe"){
            variNPC.profeExplicacion = 1;
        }

        if(cualNPC == "dialogoIntro" || cualNPC == "z2npc"){
            variNPC.finalVddro += 1;
        }
    }

    public int yaHablo(string cualNPC){
        //Test Purpose
        if (cualNPC == "npc1" && variNPC.habloNPC2 == 1)
        {
            return 3;
        }
        else if (cualNPC == "npc2" && variNPC.habloNPC1 == 1)
        {
            return 3;
        }

        //Ahora si, las que son
        if (cualNPC == "profe" && variNPC.profeExplicacion == 1)
        {
            return 9;
        }

        return 0;
    }
}
