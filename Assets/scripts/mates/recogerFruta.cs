using System.Collections;
using TMPro;
using UnityEngine;

public class recogerFruta : MonoBehaviour
{
    public bool esLaRespuesta;
    public GameObject respG, juego, infoPanel, jugador;
    public TextMeshProUGUI resp;
    public preguntas scriptPregus;
    public controlesPersonaje control;
    public dialogoMAtes textoCual;

    void Start(){
        scriptPregus.cambioPregus();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("jugador"))
        {
            StartCoroutine(respuesta());
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Invoke("desaparecer", 0.5f);
            Invoke("cambioAInfo", 3f);
        }
    }

    private IEnumerator respuesta(){
        if (esLaRespuesta)
        {
            control.prendido = false;
            CambiarColorRespuesta(respG, resp, Color.green, 0.5f);
            textoCual.pantallaNegra.transform.position = new Vector2(1363, textoCual.pantallaNegra.transform.position.y);
            textoCual.moverX(0f, 0.5f, 2f, textoCual.pantallaNegra);
            Invoke("correctoMr", 2.8f);
            textoCual.moverX(-1363f, 0.5f, 2.8f, textoCual.pantallaNegra);
            textoCual.indexLin++;
            yield return new WaitForSeconds(3f);
            
        }
        else
        {
            control.prendido = false;
            CambiarColorRespuesta(respG, resp, Color.red, 0.5f);
            textoCual.pantallaNegra.transform.position = new Vector2(1363, textoCual.pantallaNegra.transform.position.y);
            textoCual.moverX(0f, 0.5f, 2f, textoCual.pantallaNegra);
            Invoke("incorrectoMr", 2.8f);
            textoCual.moverX(-1363f, 0.5f, 2.8f, textoCual.pantallaNegra);
            yield return new WaitForSeconds(3f);
        }
        ReiniciarDialogo();
    }

    void correctoMr(){
        textoCual.checkpoint = textoCual.indexLin;
        textoCual.empezarDialogo();
    }
    void incorrectoMr(){
        textoCual.indexLin = textoCual.checkpoint;
        textoCual.texto.text = textoCual.lineasDialogo[0];
        textoCual.empezarDialogo();
    }

    void desaparecer()
    {
        gameObject.SetActive(false);
    }

    void cambioAInfo(){
        juego.SetActive(false);
        infoPanel.SetActive(true);
        reinicioEscenario();
        scriptPregus.cambioPregus();
        textoCual.volverBot.SetActive(false);
    }

    private void reinicioEscenario(){
        control.prendido = true;
            GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            jugador.transform.position = new Vector2(-1,3.5f);
            CambiarColorRespuesta(respG, resp, Color.blue, 0f);
            gameObject.SetActive(true);
    }

    void ReiniciarDialogo()
    {
        textoCual.indexLin = 0;
        textoCual.empezarDialogo();
    }

    void CambiarColorRespuesta(GameObject objeto, TextMeshProUGUI texto, Color color, float duracion)
    {
        LeanTween.value(objeto, texto.color, color, duracion)
            .setOnUpdate((Color val) =>
            {
                resp.color = val;
            });
    }
}
