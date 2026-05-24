using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paredes : MonoBehaviour
{
    public pointYclic control;
    public GameObject jugador;
    private bool tocaPared;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("jugador"))
        {
            tocaPared = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("jugador"))
        {
            tocaPared = false;
        }
    }

    void Update(){
        if(tocaPared){
            control.targetPosition = new Vector2(jugador.transform.position.x, jugador.transform.position.y);
        }
    }
}
