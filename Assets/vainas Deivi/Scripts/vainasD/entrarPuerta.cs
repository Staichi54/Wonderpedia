using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class entrarPuerta : MonoBehaviour
{
    public bool esLaRespuesta;
    public GameObject jugador;
    private GameObject negro;
    public dialogoPregunras textoCual;
    public preguntasMundoAbierto scriptPregus;

    private bool next;

    void Start(){
        negro = GameObject.Find("negro");
        negro.transform.position = new Vector2(0, 60);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            LeanTween.moveY(negro, 0, 1.5f).setEase(LeanTweenType.easeInOutQuart);
            Invoke("movetePibe", 1.7f);
            StartCoroutine(respuesta());
            LeanTween.moveY(negro, -60, 1.5f).setDelay(2).setEase(LeanTweenType.easeInOutQuart);
            LeanTween.moveY(negro, 60, 0).setDelay(3f).setEase(LeanTweenType.easeInOutQuart);
        }
    }

    void movetePibe(){
        jugador.transform.position = new Vector2(0,0);
    }

    private IEnumerator respuesta(){
        if(esLaRespuesta){
            Debug.Log("Le ATINASTE");
            textoCual.indexLin++;
            scriptPregus.pregBien++;
            scriptPregus.pregResActual();
            yield return new WaitForSeconds(3f);
        }else{
            scriptPregus.pregResActual();
        }
    }

}
