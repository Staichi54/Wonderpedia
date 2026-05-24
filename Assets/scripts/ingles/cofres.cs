using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cofres : MonoBehaviour
{
    private bool estaEnRango, empezoChachara;
    public GameObject panelDialogo;
    private GameObject jugador;
    private TMP_Text texto;
    public int indexLin;
    public float velocidadTexto = 0.05f;
    private pointYclic puntaYclick;
    private SpriteRenderer sprite;
    [TextArea(4, 6)] public string[] lineasDialogo;

    void Awake(){
        jugador = GameObject.Find("ajolote");
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start(){
        texto = panelDialogo.GetComponentInChildren<TMP_Text>();
        puntaYclick = jugador.GetComponent<pointYclic>();
    }

    private void OnTriggerEnter2D(Collider2D colision){
        if (colision.CompareTag("jugador")){
            estaEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D colision){
        if (colision.CompareTag("jugador")){
            estaEnRango = false;
        }
    }

    void Update()
    {
        if (jugador.transform.position.x > transform.position.x){
                sprite.flipX = false;
            }else{
                sprite.flipX = true;
            }

        if (Input.GetKeyDown("space") && estaEnRango)
        {
            if(!empezoChachara){
                empezarDialogo();
            }else if(texto.text == lineasDialogo[indexLin]){
                siguienteDialog();
            }else{
                StopAllCoroutines();
                texto.text = lineasDialogo[indexLin];
            }
        }    
    }
    private void empezarDialogo(){
        puntaYclick.targetPosition = new Vector2(jugador.transform.position.x, jugador.transform.position.y);
        puntaYclick.sePuedeMover = false;
        empezoChachara = true;
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
            sprite.enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            indexLin = 0;
            puntaYclick.sePuedeMover = true;
            empezoChachara = false;
            panelDialogo.SetActive(false);
        }
    }

    private IEnumerator mostrarLinea()
    {
        texto.text = string.Empty;
        bool dentroParentesis = false;
        foreach (char ch in lineasDialogo[indexLin])
        {
            if (ch == '(')
            {
                dentroParentesis = true;
                texto.text += "<color=red>";
            }
            else if (ch == ')')
            {
                dentroParentesis = false;
                texto.text += "</color>";
            }
            else
            {
                if (dentroParentesis)
                {
                    texto.text += ch;
                }
                else
                {
                    texto.text += ch;
                }
            }
            yield return new WaitForSeconds(velocidadTexto);
        }
    }
}
