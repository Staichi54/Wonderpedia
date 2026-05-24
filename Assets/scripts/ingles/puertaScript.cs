using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaScript : MonoBehaviour
{
    private variablesEscenas escenario;
    public int escenarioIndex;
    public Vector2 posJug, camMin, camMax;
    private bool tocandoPuerta;

    void Start(){
        escenario = GameObject.Find("controladorEscenas").GetComponent<variablesEscenas>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("jugador"))
        {
            tocandoPuerta = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("jugador"))
        {
            tocandoPuerta = false;
        }
    }

    void Update(){
        if(Input.GetKeyDown("space") && tocandoPuerta){
            //Debug.Log("Pa dentro");
            escenario.cambioEscenario(escenarioIndex, posJug, camMin, camMax);
        }
        
    }
}
