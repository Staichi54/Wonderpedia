using UnityEngine;
using UnityEngine.Rendering;

public class variablesEscenas : MonoBehaviour
{
    // Lista de escenarios disponibles
    public GameObject[] escenarios;
    public GameObject jugador, transicion, camaraGam, baibai;
    public seguirJugador camara;
    public pointYclic control;

    private int indiceA; 
    private Vector2 personajeA, camMinA, camMaxA;

    void Start(){
        transicion.transform.position = new Vector2(transicion.transform.position.x, 11f);
        jugador.SetActive(true);
        Vector2 posJug = new Vector2(-7, -3);
        Vector2 camMin = new Vector2(0,0);
        Vector2 camMax = new Vector2(0,0);
        cambioEscenario(0, posJug, camMin, camMax);
    }

    void Update(){
        Vector3 nuevaPosicion1 = transicion.transform.position;
        Vector3 nuevaPosicion2 = baibai.transform.position;
        nuevaPosicion1.x = camaraGam.transform.position.x;
        nuevaPosicion2.x = camaraGam.transform.position.x;
        transicion.transform.position = nuevaPosicion1;
        baibai.transform.position = nuevaPosicion2;
    }
    
    // Método para activar un escenario específico
    public void cambioEscenario(int indice, Vector2 personaje, Vector2 camMin, Vector2 camMax){
        indiceA = indice;
        personajeA = personaje;
        camMinA = camMin;
        camMaxA = camMax;
        control.sePuedeMover = false;
        // Activar el escenario seleccionado
        if (indice >= 0 && indice < escenarios.Length){
            moverY(0f, 0.7f, 0f, transicion);
            Invoke("DesactivarEscenarios", 1.2f);
            Invoke("invocarEscenario", 1.3f);
            moverY(-11f, 0.7f, 1.1f, transicion);
            moverY(11f, 0f, 2f, transicion);
        }
        else{
            Debug.LogError("Índice de escenario fuera de rango.");
        }
    }

    void invocarEscenario(){
        escenarios[indiceA].SetActive(true);
        jugador.transform.position = personajeA;
        camara.minXAndY = camMinA;
        camara.maxXAndY = camMaxA;
        control.sePuedeMover = true;
    }

    // Método para desactivar todos los escenarios
    private void DesactivarEscenarios(){
        foreach (GameObject escenario in escenarios){
            escenario.SetActive(false);
        }
    }

    private void moverY(float y, float duracion, float delay, GameObject imagen){
        LeanTween.moveY(imagen.GetComponent<RectTransform>(), y, duracion)
        .setDelay(delay)
        .setEase(LeanTweenType.easeInOutQuart);
    }
}
