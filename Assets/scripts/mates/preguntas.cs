using TMPro;
using UnityEngine;

public class preguntas : MonoBehaviour
{
    public TMP_Text laPregu, resA, resB, resC, resD;
    public dialogoMAtes controlDialogoMats;
    public recogerFruta frut1, frut2, frut3, frut4;
    int min = 1, max = 300, numA, numB, respuestaToo;

    //Para añadir mas, se debe colocar el numero del arreglo donde va el "$" + 1

    public void cambioPregus(){
        switch(controlDialogoMats.checkpoint){
            case int n when n >= 0 && n <= 11:
                numA = Random.Range(min, max + 1);
                numB = Random.Range(min, max + 1);
                laPregu.text = "Cuanto es " + numA + " + " + numB + "?";
                respuestaToo = numA + numB;
                Debug.Log("numA = " + numA + ", numB = " + numB + ", respuesta = " + respuestaToo);
                elegirManzana();
                break;
            case int n when n >= 12 && n <= 21:
                numA = Random.Range(min, max + 1);
                numB = Random.Range(min, max + 1);
                if(numA < numB){
                    laPregu.text = "Cuanto es " + numB + " - " + numA + "?";
                    respuestaToo = numB - numA;
                    Debug.Log("numB = " + numB + ", numA = " + numA + ", respuesta = " + respuestaToo);
                }else{
                    laPregu.text = "Cuanto es " + numA + " - " + numB + "?";
                    respuestaToo = numA - numB;
                    Debug.Log("numA = " + numA + ", numB = " + numB + ", respuesta = " + respuestaToo);
                }
                elegirManzana();
                break;
            case int n when n >= 22 && n <= 41:
                numA = Random.Range(min, max + 1);
                numB = Random.Range(min, 10);
                laPregu.text = "Cuanto es " + numA + " X " + numB + "?";
                respuestaToo = numA * numB;
                Debug.Log("numA = " + numA + ", numB = " + numB + ", respuesta = " + respuestaToo);
                elegirManzana();
                break;
            case int n when n >= 42 && n <= 61:
                numA = Random.Range(min, max + 1);
                numB = Random.Range(min, 10);
                laPregu.text = "Cuanto es " + numA + " / " + numB + "?";
                respuestaToo = numA / numB;
                Debug.Log("numA = " + numA + ", numB = " + numB + ", respuesta = " + respuestaToo);
                elegirManzana();
                break;
            case int n when n >= 62 && n <= 73:
                numA = Random.Range(0, 6);
                numB = Random.Range(0, 6);
                laPregu.text = "Cuanto es " + numA + " a la " + numB + "?";
                float resPot = Mathf.Pow(numA, numB);
                respuestaToo = (int)resPot;
                Debug.Log("numA = " + numA + ", numB = " + numB + ", respuesta = " + resPot);
                elegirManzana();
                break;
            case int n when n >= 74 && n <= controlDialogoMats.lineasDialogo.Length - 1:
                numA = Random.Range(min, max + 1);
                laPregu.text = "Cuanto es √" + numA + "?";
                float raCua = Mathf.Sqrt(numA);
                respuestaToo = (int)raCua;
                Debug.Log("numA = " + numA + ", respuesta = " + raCua);
                elegirManzana();
                break;
            default:
                break;
        }
    }

    private void elegirManzana(){
    int eleccion = Random.Range(1, 5); 
    switch(eleccion){
        case 1:
            frut1.esLaRespuesta = true;
            frut2.esLaRespuesta = false;
            frut3.esLaRespuesta = false;
            frut4.esLaRespuesta = false; 
            resA.text = respuestaToo.ToString();
            resB.text = Random.Range(min, max + 1).ToString();
            resC.text = Random.Range(min, max + 1).ToString();
            resD.text = Random.Range(min, max + 1).ToString();
            Debug.Log("Es la 1");
            break;
        case 2:
            frut1.esLaRespuesta = false;
            frut2.esLaRespuesta = true;
            frut3.esLaRespuesta = false;
            frut4.esLaRespuesta = false; 
            resA.text = Random.Range(min, max + 1).ToString();
            resB.text = respuestaToo.ToString();
            resC.text = Random.Range(min, max + 1).ToString();
            resD.text = Random.Range(min, max + 1).ToString();
            Debug.Log("Es la 2");
            break;
        case 3:
            frut1.esLaRespuesta = false;
            frut2.esLaRespuesta = false;
            frut3.esLaRespuesta = true;
            frut4.esLaRespuesta = false; 
            resA.text = Random.Range(min, max + 1).ToString();
            resB.text = Random.Range(min, max + 1).ToString();
            resC.text = respuestaToo.ToString();
            resD.text = Random.Range(min, max + 1).ToString();
            Debug.Log("Es la 3");
            break;
        case 4:
            frut1.esLaRespuesta = false;
            frut2.esLaRespuesta = false;
            frut3.esLaRespuesta = false;
            frut4.esLaRespuesta = true; 
            resA.text = Random.Range(min, max + 1).ToString();
            resB.text = Random.Range(min, max + 1).ToString();
            resC.text = Random.Range(min, max + 1).ToString();
            resD.text = respuestaToo.ToString();
            Debug.Log("Es la 4");
            break;
        default:
            Debug.Log("Que");
            break;
        }
    }
}

