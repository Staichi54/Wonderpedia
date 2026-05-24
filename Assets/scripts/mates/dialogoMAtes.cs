using System.Collections;
using UnityEngine;
using TMPro;

public class dialogoMAtes : MonoBehaviour
{
    public controlesPersonaje control;
    public float velocidadTexto = 0.05f;
    [TextArea(4, 6)] public string[] lineasDialogo;
    public GameObject panelDialogo, juego, preguntas, volverBot, final, pantallaNegra;
    public TMP_Text texto;
    public int indexLin, checkpoint = 0;

    void Start()
    {  
        indexLin = 0;
        checkpoint = 0;
        juego.SetActive(false);
        empezarDialogo();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && panelDialogo.activeSelf)
        {
            if (texto.text == lineasDialogo[indexLin])
            {
                siguienteDialog();
            }
            else
            {
                StopAllCoroutines();
                texto.text = lineasDialogo[indexLin];
            }
            if (texto.text == "$")
            {
                texto.text = "";
                pantallaNegra.transform.position = new Vector2(1363, pantallaNegra.transform.position.y);
                moverX(0f, 0.5f, 0f, pantallaNegra);
                Invoke("cargarSignoDolar", 0.8f);
                moverX(-1363f, 0.5f, 1f, pantallaNegra);
            }
            if (texto.text == "*")
            {
                texto.text = "";
                pantallaNegra.transform.position = new Vector2(1363, pantallaNegra.transform.position.y);
                moverX(0f, 0.5f, 0f, pantallaNegra);
                Invoke("finalEscape", 0.8f);
                moverX(-1363f, 0.5f, 1f, pantallaNegra);
            }
        }    
    }

    void finalEscape()
    {
        siguienteDialog();
        panelDialogo.SetActive(false);
        final.SetActive(true);
        indexLin = 0;
        checkpoint = 0;
    }

    private void cargarSignoDolar()
    {
        panelDialogo.SetActive(false);
        juego.SetActive(true);
        volverBot.SetActive(true);
        preguntas.SetActive(true);
        control.prendido = true;
    }

    public void empezarDialogo()
    {
        for (int i = 0; i < lineasDialogo.Length; i++)
        {
            lineasDialogo[i] = QuitarTildes(lineasDialogo[i]);
        }
        StartCoroutine(mostrarLinea());
    }

    private string QuitarTildes(string input)
    {
        input = input.Replace('á', 'a');
        input = input.Replace('é', 'e');
        input = input.Replace('í', 'i');
        input = input.Replace('ó', 'o');
        input = input.Replace('ú', 'u');
        input = input.Replace('Á', 'A');
        input = input.Replace('É', 'E');
        input = input.Replace('Í', 'I');
        input = input.Replace('Ó', 'O');
        input = input.Replace('Ú', 'U');
        input = input.Replace('¿', ' ');
        input = input.Replace('¡', ' ');
        input = input.Replace('×', 'x');
        return input;
    }

    private void siguienteDialog()
    {
        indexLin++;
        if (indexLin < lineasDialogo.Length)
        {
            StartCoroutine(mostrarLinea());
        }
        else
        {
            panelDialogo.SetActive(false);
            juego.SetActive(true);
            preguntas.SetActive(true);
        }
    }

    public void moverX(float x, float duracion, float delay, GameObject imagen)
    {
        LeanTween.moveX(imagen.GetComponent<RectTransform>(), x, duracion)
            .setDelay(delay)
            .setEase(LeanTweenType.easeInOutQuart);
    }

    private IEnumerator mostrarLinea()
    {
        texto.text = string.Empty;
        foreach (char ch in lineasDialogo[indexLin])
        {
            if (char.IsDigit(ch))
            {
                texto.text += "<color=red>" + ch + "</color>";
            }
            else
            {
                texto.text += ch;
            }
            yield return new WaitForSeconds(velocidadTexto);
        }
    }
}
