using UnityEngine;

public class jajaCfue : MonoBehaviour
{
    public variablesEscenas escenario;
    public int escenarioIndex;
    private GameObject jugador;
    public Vector2 posJug, camMin, camMax;
    private bool tocandoFondo;
    private pointYclic puntaYclick;

    void Awake(){
        jugador = GameObject.Find("ajolote");
    }

    void Start(){
        puntaYclick = jugador.GetComponent<pointYclic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("jugador"))
        {
            tocandoFondo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("jugador"))
        {
            tocandoFondo = false;
        }
    }

    void Update()
    {
        if (tocandoFondo)
        {
            puntaYclick.anim.SetFloat("run", 0);
            puntaYclick.isMoving = false;
            escenario.cambioEscenario(escenarioIndex, posJug, camMin, camMax);
        }
    }
}
