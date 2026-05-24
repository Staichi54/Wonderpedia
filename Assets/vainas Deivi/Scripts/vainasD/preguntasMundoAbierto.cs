using TMPro;
using UnityEngine;

public class preguntasMundoAbierto : MonoBehaviour
{
    public int pregBien;
    public dialogoPregunras controlDialogoMats;
    public cartelPregus cartelPre1, cartelPre2, cartelPre3;
    public entrarPuerta puer1, puer2, puer3;

    //Para añadir mas, se debe colocar el numero del arreglo donde va el "$" + 1

    void Start(){
        pregResActual();
    }

    public void pregResActual(){
        switch(pregBien){
            case 0:
                controlDialogoMats.indexLin = 0;
                cualEsResp(true, false, false);
                cartelPre1.indexLin = 0;
                cartelPre2.indexLin = 0;
                cartelPre3.indexLin = 0;
                break;
            case 1:
                controlDialogoMats.indexLin = 3;
                cualEsResp(false, true, false);
                cartelPre1.indexLin = 2;
                cartelPre2.indexLin = 2;
                cartelPre3.indexLin = 2;
                break;
            case 2:
                controlDialogoMats.indexLin = 5;
                cualEsResp(false, false, true);
                cartelPre1.indexLin = 4;
                cartelPre2.indexLin = 4;
                cartelPre3.indexLin = 4;
                break;
            case 3:
                controlDialogoMats.indexLin = 7;
                break;
        }
    }

        private void cualEsResp(bool a, bool b, bool c){
        Debug.Log("cual es respuesta?");
        puer1.esLaRespuesta = a;
        puer2.esLaRespuesta = b;
        puer3.esLaRespuesta = c;
    }
}


