using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FondoInfinitoOnda : MonoBehaviour
{
    [Header("Fondos base que se mueven")]
    public RectTransform fondoA;
    public RectTransform fondoB;

    [Header("Fondos de los paneles")]
    public Image fondoPanelLoginA;
    public Image fondoPanelLoginB;
    public Image fondoPanelRegistroA;
    public Image fondoPanelRegistroB;

    [Header("Movimiento")]
    public float velocidadX = 60f;
    public float amplitudOnda = 25f;
    public float frecuenciaOnda = 2f;

    [Header("Tamaño de cada fondo")]
    public float anchoFondo = 1920f;
    public float altoFondo = 1080f;

    [Header("Cambio entre fondos")]
    public float tiempoEntreCambios = 5f;
    public float duracionFade = 1f;

    private List<RectTransform> copiasA = new List<RectTransform>();
    private List<RectTransform> copiasB = new List<RectTransform>();

    private bool mostrandoA = true;
    private float temporizadorCambio = 0f;

    void Start()
    {
        CrearFondos();
        ConfigurarEstadoInicial();
        MandarFondosAlFondo();
    }

    void Update()
    {
        MoverLista(copiasA);
        MoverLista(copiasB);

        temporizadorCambio += Time.deltaTime;

        if (temporizadorCambio >= tiempoEntreCambios)
        {
            temporizadorCambio = 0f;
            CambiarFondo();
        }
    }

    void CrearFondos()
    {
        copiasA.Clear();
        copiasB.Clear();

        copiasA.Add(fondoA);
        copiasB.Add(fondoB);

        Vector2[] posiciones =
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(0, -1),
            new Vector2(1, 1),
            new Vector2(-1, 1),
            new Vector2(1, -1),
            new Vector2(-1, -1)
        };

        CrearCopias(fondoA, copiasA, posiciones);
        CrearCopias(fondoB, copiasB, posiciones);
    }

    void CrearCopias(RectTransform fondoOriginal, List<RectTransform> lista, Vector2[] posiciones)
    {
        for (int i = 1; i < posiciones.Length; i++)
        {
            RectTransform copia = Instantiate(fondoOriginal, fondoOriginal.parent);
            copia.name = fondoOriginal.name + "_Copia_" + i;
            lista.Add(copia);
        }

        for (int i = 0; i < lista.Count; i++)
        {
            RectTransform fondo = lista[i];

            fondo.anchoredPosition = new Vector2(
                posiciones[i].x * anchoFondo,
                posiciones[i].y * altoFondo
            );

            Image imagen = fondo.GetComponent<Image>();

            if (imagen != null)
            {
                imagen.raycastTarget = false;
            }
        }
    }

    void ConfigurarEstadoInicial()
    {
        fondoA.gameObject.SetActive(true);
        fondoB.gameObject.SetActive(true);

        foreach (RectTransform fondo in copiasA)
        {
            CambiarAlphaRect(fondo, 1f);
        }

        foreach (RectTransform fondo in copiasB)
        {
            CambiarAlphaRect(fondo, 0f);
        }

        // Fondos de paneles: empieza mostrando los A
        CambiarAlphaImage(fondoPanelLoginA, 1f);
        CambiarAlphaImage(fondoPanelLoginB, 0f);

        CambiarAlphaImage(fondoPanelRegistroA, 1f);
        CambiarAlphaImage(fondoPanelRegistroB, 0f);

        DesactivarRaycastPaneles();

        mostrandoA = true;
    }

    void MoverLista(List<RectTransform> lista)
    {
        foreach (RectTransform fondo in lista)
        {
            Vector2 pos = fondo.anchoredPosition;

            pos.x += velocidadX * Time.deltaTime;

            float onda = Mathf.Sin(Time.time * frecuenciaOnda + pos.x * 0.01f) * amplitudOnda;
            pos.y += onda * Time.deltaTime;

            if (pos.x >= anchoFondo * 1.5f)
            {
                pos.x -= anchoFondo * 3f;
            }

            fondo.anchoredPosition = pos;
        }
    }

    void CambiarFondo()
    {
        if (mostrandoA)
        {
            // Fondo grande
            FadeLista(copiasA, 0f);
            FadeLista(copiasB, 1f);

            // Fondos de panel
            FadeImage(fondoPanelLoginA, 0f);
            FadeImage(fondoPanelLoginB, 1f);

            FadeImage(fondoPanelRegistroA, 0f);
            FadeImage(fondoPanelRegistroB, 1f);
        }
        else
        {
            // Fondo grande
            FadeLista(copiasA, 1f);
            FadeLista(copiasB, 0f);

            // Fondos de panel
            FadeImage(fondoPanelLoginA, 1f);
            FadeImage(fondoPanelLoginB, 0f);

            FadeImage(fondoPanelRegistroA, 1f);
            FadeImage(fondoPanelRegistroB, 0f);
        }

        mostrandoA = !mostrandoA;
    }

    void FadeLista(List<RectTransform> lista, float alphaFinal)
    {
        foreach (RectTransform fondo in lista)
        {
            LeanTween.alpha(fondo, alphaFinal, duracionFade)
                .setEase(LeanTweenType.easeInOutSine);
        }
    }

    void FadeImage(Image imagen, float alphaFinal)
    {
        if (imagen == null) return;

        LeanTween.alpha(imagen.rectTransform, alphaFinal, duracionFade)
            .setEase(LeanTweenType.easeInOutSine);
    }

    void CambiarAlphaRect(RectTransform fondo, float alpha)
    {
        Image imagen = fondo.GetComponent<Image>();

        if (imagen != null)
        {
            Color color = imagen.color;
            color.a = alpha;
            imagen.color = color;
        }
    }

    void CambiarAlphaImage(Image imagen, float alpha)
    {
        if (imagen == null) return;

        Color color = imagen.color;
        color.a = alpha;
        imagen.color = color;
    }

    void DesactivarRaycastPaneles()
    {
        if (fondoPanelLoginA != null) fondoPanelLoginA.raycastTarget = false;
        if (fondoPanelLoginB != null) fondoPanelLoginB.raycastTarget = false;

        if (fondoPanelRegistroA != null) fondoPanelRegistroA.raycastTarget = false;
        if (fondoPanelRegistroB != null) fondoPanelRegistroB.raycastTarget = false;
    }

    void MandarFondosAlFondo()
    {
        for (int i = copiasB.Count - 1; i >= 0; i--)
        {
            copiasB[i].SetAsFirstSibling();
        }

        for (int i = copiasA.Count - 1; i >= 0; i--)
        {
            copiasA[i].SetAsFirstSibling();
        }
    }
}