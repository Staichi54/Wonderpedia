using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class cartelPregus : MonoBehaviour
{
    public TMP_Text texto;
    public int indexLin;
    public PersonaMovi perMov;
    public float velocidadTexto = 0.05f;
    private bool estaCerca, empezoChachara;
    public GameObject panelPregunta;
    public preguntasMundoAbierto preg;
    [TextArea(4, 6)] public string[] respuestas;

    public void OnTriggerEnter2D(){
        estaCerca = true;
    }

    public void OnTriggerExit2D(){
        estaCerca = false;
    }

    void Update(){
        if(Input.GetKeyDown("space") && estaCerca){
            if(!empezoChachara){
                perMov.prendido = false;
                empezarDialogo();
            }else if(texto.text == respuestas[indexLin]){
                siguienteDialog();
            }
            else{
                StopAllCoroutines();
                texto.text = respuestas[indexLin];
            }
            
            if (texto.text == "$")
            {
                perMov.prendido = true;
                texto.text = String.Empty;
                empezoChachara = false;
                panelPregunta.SetActive(false);
                preg.pregResActual();
            }
        }
    }

    public void empezarDialogo()
    {
        empezoChachara = true;
        perMov.prendido = false;
        panelPregunta.SetActive(true);
        for (int i = 0; i < respuestas.Length; i++)
        {
            respuestas[i] = QuitarTildes(respuestas[i]);
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
        if (indexLin < respuestas.Length)
        {
            StartCoroutine(mostrarLinea());
        }
        else
        {
            panelPregunta.SetActive(false);
            perMov.prendido = true;
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
        foreach (char ch in respuestas[indexLin])
        {
            texto.text += ch;
            yield return new WaitForSeconds(velocidadTexto);
        }
    }

}
