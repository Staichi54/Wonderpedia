using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public variablesNPCMundoAbierto variNPC;

    public void verificarQnHablo(string cualNPC)
    {
        if (cualNPC == "NPC1")
        {
            variNPC.habloNPC1 = 1;
            if (!variNPC.yaHablo1)
            {
                variNPC.sumaYaHablaron++;
                variNPC.yaHablo1 = true;
            }
        }
        if (cualNPC == "NPC2")
        {
            variNPC.habloNPC2 = 1;
            if (!variNPC.yaHablo2)
            {
                variNPC.sumaYaHablaron++;
                variNPC.yaHablo2 = true;
            }
        }
        if (cualNPC == "NPC3")
        {
            variNPC.habloNPC3 = 1;
            if (!variNPC.yaHablo3)
            {
                variNPC.sumaYaHablaron++;
                variNPC.yaHablo3 = true;
            }
        }
    }

    public int yaHablo(string cualNPC)
    {
        if (cualNPC == "NPC1" && variNPC.habloNPC2 == 1 && variNPC.habloNPC3 == 1)
        {
            if (variNPC.sumaYaHablaron == 3)
            {
                return 6;
            }
            else
            {
                return 0;
            }

        }
        else if (cualNPC == "NPC2" && variNPC.habloNPC1 == 1 && variNPC.habloNPC3 == 1)
        {
            if (variNPC.sumaYaHablaron == 3)
            {
                return 6;
            }
            else
            {
                return 0;
            }
        }
        else if (cualNPC == "NPC3" && variNPC.habloNPC1 == 1 && variNPC.habloNPC2 == 1)
        {
            if (variNPC.sumaYaHablaron == 3)
            {
                return 6;
            }
            else
            {
                return 0;
            }
        }
        return 0;
    }
}
