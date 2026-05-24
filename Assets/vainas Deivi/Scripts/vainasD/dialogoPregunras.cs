using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class dialogoPregunras : MonoBehaviour
{
    public float velocidadTexto = 0.05f;

    [Header("Objetos principales")]
    public GameObject panelDialogo, final, pantallaNegra, jugador, juego;

    [Header("Botón que se oculta al finalizar")]
    public GameObject botonVolver;

    private GameObject negro;

    public PersonaMovi perMov;
    public preguntasMundoAbierto preg;
    public TMP_Text texto;
    public int indexLin;

    private bool estaCerca, empezoChachara;

    [TextArea(4, 6)] public string[] lineasDialogo;

    void Start()
    {
        negro = GameObject.Find("negro");

        pantallaNegra.transform.position = new Vector2(0, 39);

        indexLin = 0;

        if (final != null)
        {
            final.SetActive(false);
        }

        if (botonVolver != null)
        {
            botonVolver.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            estaCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            estaCerca = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && estaCerca)
        {
            if (!empezoChachara)
            {
                empezarDialogo();
            }
            else if (texto.text == lineasDialogo[indexLin])
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
                perMov.prendido = true;
                texto.text = String.Empty;
                empezoChachara = false;
                panelDialogo.SetActive(false);
                preg.pregResActual();
            }

            if (texto.text == "*")
            {
                LeanTween.moveY(negro, 0, 1.5f)
                    .setEase(LeanTweenType.easeInOutQuart);

                Invoke("finalEscape", 1.7f);

                LeanTween.moveY(negro, -60, 1.5f)
                    .setDelay(2)
                    .setEase(LeanTweenType.easeInOutQuart);

                LeanTween.moveY(negro, 60, 0)
                    .setDelay(3f)
                    .setEase(LeanTweenType.easeInOutQuart);
            }
        }
    }

    void finalEscape()
    {
        if (panelDialogo != null)
        {
            panelDialogo.SetActive(false);
        }

        if (juego != null)
        {
            juego.SetActive(false);
        }

        if (botonVolver != null)
        {
            botonVolver.SetActive(false);
        }

        if (final != null)
        {
            final.SetActive(true);
        }

        indexLin = 0;
    }

    public void empezarDialogo()
    {
        empezoChachara = true;
        perMov.prendido = false;
        panelDialogo.SetActive(true);

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
            texto.text += ch;
            yield return new WaitForSeconds(velocidadTexto);
        }
    }
}