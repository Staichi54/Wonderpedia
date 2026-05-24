using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class dialogoNPC : MonoBehaviour
{
    public enum TipoLogro
    {
        Ingles,
        Mates,
        Historia
    }

    private bool estaEnRango, empezoChachara;
    private bool logroYaActivado = false;

    [Header("NPC")]
    public string cualNpc;
    public GameObject signoPreg, panelDialogo;
    public bool puedeGirar = true;

    [Header("Logro")]
    public bool activarLogroAlTerminar = false;
    public TipoLogro tipoLogro = TipoLogro.Ingles;

    private GameObject jugador;
    private TMP_Text texto;
    public int indexLin;
    public float velocidadTexto = 0.05f;
    private pointYclic puntaYclick;
    private controlDialogos control;
    private SpriteRenderer sprite;

    [TextArea(4, 6)] public string[] lineasDialogo;

    void Awake()
    {
        jugador = GameObject.Find("ajolote");
        sprite = GetComponent<SpriteRenderer>();
        control = GameObject.Find("controladorVariablesNpc").GetComponent<controlDialogos>();
    }

    void Start()
    {
        texto = panelDialogo.GetComponentInChildren<TMP_Text>();
        puntaYclick = jugador.GetComponent<pointYclic>();
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("jugador"))
        {
            signoPreg.SetActive(true);
            estaEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D colision)
    {
        if (colision.CompareTag("jugador"))
        {
            signoPreg.SetActive(false);
            estaEnRango = false;
        }
    }

    void Update()
    {
        if (puedeGirar)
        {
            if (jugador.transform.position.x > transform.position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        if (Input.GetKeyDown("space") && estaEnRango)
        {
            if (!empezoChachara)
            {
                empezarDialogo();
                control.verificarQnHablo(cualNpc);
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

            if (texto.text == "*")
            {
                texto.text = String.Empty;
                indexLin++;
                puntaYclick.sePuedeMover = true;
                empezoChachara = false;
                panelDialogo.SetActive(false);

                ActivarLogroSiCorresponde();
            }
        }
    }

    private void empezarDialogo()
    {
        puntaYclick.targetPosition = new Vector2(jugador.transform.position.x, jugador.transform.position.y);
        puntaYclick.sePuedeMover = false;
        empezoChachara = true;
        panelDialogo.SetActive(true);
        signoPreg.SetActive(false);

        indexLin = control.yaHablo(cualNpc);

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
            puntaYclick.sePuedeMover = true;
            empezoChachara = false;
            panelDialogo.SetActive(false);

            ActivarLogroSiCorresponde();
        }
    }

    private void ActivarLogroSiCorresponde()
    {
        if (!activarLogroAlTerminar)
        {
            return;
        }

        if (logroYaActivado)
        {
            return;
        }

        int usuarioId = PlayerPrefs.GetInt("UsuarioId", 0);
        string token = PlayerPrefs.GetString("JWT", "");

        if (usuarioId <= 0 || string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("No hay sesión iniciada. No se activará el logro.");
            return;
        }

        if (WonderpediaAPI.Instance == null)
        {
            Debug.LogError("No se encontró WonderpediaAPI.Instance en esta escena.");
            return;
        }

        logroYaActivado = true;

        switch (tipoLogro)
        {
            case TipoLogro.Ingles:
                WonderpediaAPI.Instance.FinalizarIngles();
                Debug.Log("Activando logro de Inglés desde NPC: " + cualNpc);
                break;

            case TipoLogro.Mates:
                WonderpediaAPI.Instance.FinalizarMates();
                Debug.Log("Activando logro de Matemáticas desde NPC: " + cualNpc);
                break;

            case TipoLogro.Historia:
                WonderpediaAPI.Instance.FinalizarHistoria();
                Debug.Log("Activando logro de Historia desde NPC: " + cualNpc);
                break;
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
                texto.text += ch;
            }

            yield return new WaitForSeconds(velocidadTexto);
        }
    }
}