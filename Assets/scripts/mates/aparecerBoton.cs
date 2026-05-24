using UnityEngine;
using UnityEngine.UI;

public class aparecerBoton : MonoBehaviour
{
    public Button iniciar;
    public GameObject pantallaNegra, final, botonIni, texto, botonVol, inicio, informacion, panel, partPreg, fondo, juego, infoConT, textoCont;
    void Start()
    {
        juego.SetActive(false);
        panel.SetActive(true);
        inicio.SetActive(true);
        partPreg.SetActive(false);
        informacion.SetActive(false);
        fondo.SetActive(true);
        final.SetActive(false);
        textoCont.SetActive(false);
        alfa(1f, 1f, 6f, botonIni);
        alfa(1f, 1f, 6f, infoConT);
        Invoke("aparLet", 7f);
        iniciar.onClick.AddListener(empezarJuego);

        moverY(-250f, 1.5f, 8f, botonVol);
        alfa(1f, 1f, 8f, botonVol);
    }

    public void moverX(float x, float duracion, float delay, GameObject imagen){
        LeanTween.moveX(imagen.GetComponent<RectTransform>(), x, duracion)
        .setDelay(delay)
        .setEase(LeanTweenType.easeInOutQuart);
    }

    void aparLet(){
        texto.SetActive(true);
        textoCont.SetActive(true);
    }

    void empezarJuego(){
        pantallaNegra.transform.position = new Vector2(1363, pantallaNegra.transform.position.y);
        moverX(0f, 0.5f, 0f, pantallaNegra);
        Invoke("empJue2", 0.8f);
        moverX(-1363f, 0.5f, 1f, pantallaNegra);
    }

    void empJue2(){
        inicio.SetActive(false);
        botonVol.SetActive(false);
        informacion.SetActive(true);
        alfa(0f, 0f, 0f, panel);
    }

    private void moverY(float y, float duracion, float delay, GameObject imagen){
        LeanTween.moveY(imagen.GetComponent<RectTransform>(), y, duracion)
        .setDelay(delay)
        .setEase(LeanTweenType.easeInOutQuart);
    }

    private void alfa(float valore, float duracion, float delay, GameObject imagen){
        LeanTween.alpha(imagen.GetComponent<RectTransform>(), valore, duracion)
        .setDelay(delay)
        .setEase(LeanTweenType.easeInOutQuart);
    }
}
