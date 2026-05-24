using UnityEngine;
using TMPro;

public class animUI : MonoBehaviour
{
    [SerializeField] private GameObject logo, hist, izq, der, ingles, mates, fondo;

    [Header("Estrellas de logros")]
    [SerializeField] private GameObject estrellaHistoria;

    [Header("UI Sesión")]
    [SerializeField] private GameObject botonLogin;
    [SerializeField] private GameObject botonCerrarSesion;
    [SerializeField] private GameObject botonEnviarCorreo;
    [SerializeField] private GameObject botonEliminarProgreso;
    [SerializeField] private TMP_Text textoSaludo;

    public Animator histA, ingA, matsA;

    private CanvasGroup botonLoginCanvas;
    private CanvasGroup botonCerrarSesionCanvas;
    private CanvasGroup botonEnviarCorreoCanvas;
    private CanvasGroup botonEliminarProgresoCanvas;
    private CanvasGroup textoSaludoCanvas;

    private CanvasGroup estrellaHistoriaCanvas;

    void Start()
    {
        histA = hist.GetComponent<Animator>();
        hist.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        ingA = ingles.GetComponent<Animator>();
        ingles.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        matsA = mates.GetComponent<Animator>();
        mates.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        izq.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        der.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        histA.enabled = false;
        ingA.enabled = false;
        matsA.enabled = false;

        ConfigurarUISesion();
        ConfigurarEstrellaHistoria();

        // Se agranda
        escalar(2f, 2f, 1.5f, 0.5f, logo);

        // Sube
        moverY(0f, 1.5f, 0f, logo);

        // Sube pt 2
        moverY(241f, 1.5f, 2f, logo);

        // Se achica
        escalar(1f, 1f, 1.5f, 2.1f, logo);

        // Aparecer historia, inglés, mates y fondo
        alfa(1f, 1f, 3f, fondo);

        LeanTween.alpha(hist, 1f, 1.5f).setDelay(3f);
        LeanTween.alpha(ingles, 1f, 1.5f).setDelay(3f);
        LeanTween.alpha(mates, 1f, 1.5f).setDelay(3f);

        // Aparecer estrella de Historia si el logro está completado
        AparecerEstrellaHistoriaSegunLogro(3f);

        Invoke("prenderAnims", 4f);

        moverY(0f, 1.5f, 3f, hist);

        // Aparecer flechas
        LeanTween.alpha(der, 1f, 1.5f).setDelay(3f);
        moverY(0f, 1.5f, 3f, der);

        LeanTween.alpha(izq, 1f, 1.5f).setDelay(3f);
        moverY(0f, 1.5f, 3f, izq);

        // Aparecer UI según sesión
        AparecerUISegunSesion(4f);
    }

    void prenderAnims()
    {
        histA.enabled = true;
        ingA.enabled = true;
        matsA.enabled = true;
    }

    private void ConfigurarUISesion()
    {
        if (botonLogin != null)
        {
            botonLoginCanvas = ObtenerCanvasGroup(botonLogin);
            OcultarCanvasGroup(botonLoginCanvas);
            botonLogin.SetActive(true);
        }

        if (botonCerrarSesion != null)
        {
            botonCerrarSesionCanvas = ObtenerCanvasGroup(botonCerrarSesion);
            OcultarCanvasGroup(botonCerrarSesionCanvas);
            botonCerrarSesion.SetActive(true);
        }

        if (botonEnviarCorreo != null)
        {
            botonEnviarCorreoCanvas = ObtenerCanvasGroup(botonEnviarCorreo);
            OcultarCanvasGroup(botonEnviarCorreoCanvas);
            botonEnviarCorreo.SetActive(true);
        }

        if (botonEliminarProgreso != null)
        {
            botonEliminarProgresoCanvas = ObtenerCanvasGroup(botonEliminarProgreso);
            OcultarCanvasGroup(botonEliminarProgresoCanvas);
            botonEliminarProgreso.SetActive(true);
        }

        if (textoSaludo != null)
        {
            textoSaludoCanvas = ObtenerCanvasGroup(textoSaludo.gameObject);
            OcultarCanvasGroup(textoSaludoCanvas);
            textoSaludo.gameObject.SetActive(true);
        }
    }

    private void ConfigurarEstrellaHistoria()
    {
        if (estrellaHistoria == null) return;

        estrellaHistoriaCanvas = ObtenerCanvasGroup(estrellaHistoria);
        OcultarCanvasGroup(estrellaHistoriaCanvas);

        estrellaHistoria.SetActive(true);
        estrellaHistoria.transform.SetAsLastSibling();
    }

    private CanvasGroup ObtenerCanvasGroup(GameObject objeto)
    {
        CanvasGroup canvasGroup = objeto.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = objeto.AddComponent<CanvasGroup>();
        }

        return canvasGroup;
    }

    private void OcultarCanvasGroup(CanvasGroup canvasGroup)
    {
        if (canvasGroup == null) return;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void AparecerEstrellaHistoriaSegunLogro(float delay)
    {
        if (estrellaHistoria == null || estrellaHistoriaCanvas == null) return;

        bool haySesion = PlayerPrefs.GetInt("UsuarioId", 0) > 0;
        bool historiaTerminada = PlayerPrefs.GetInt("FinalizarHistoria", 0) == 1;

        if (!haySesion || !historiaTerminada)
        {
            estrellaHistoriaCanvas.alpha = 0f;
            estrellaHistoria.SetActive(false);
            return;
        }

        estrellaHistoria.SetActive(true);
        estrellaHistoria.transform.SetAsLastSibling();

        LeanTween.alphaCanvas(estrellaHistoriaCanvas, 1f, 1.5f)
            .setDelay(delay)
            .setEase(LeanTweenType.easeInOutQuart);
    }

    private void AparecerUISegunSesion(float delay)
    {
        bool haySesion = PlayerPrefs.GetInt("UsuarioId", 0) > 0;

        if (haySesion)
        {
            string nombre = PlayerPrefs.GetString("UsuarioNombre", "Usuario");

            if (textoSaludo != null)
            {
                textoSaludo.text = "Bienvenido " + nombre;
                AparecerUI(textoSaludo.gameObject, textoSaludoCanvas, delay);
            }

            if (botonCerrarSesion != null)
            {
                AparecerUI(botonCerrarSesion, botonCerrarSesionCanvas, delay);
            }

            if (botonEnviarCorreo != null)
            {
                AparecerUI(botonEnviarCorreo, botonEnviarCorreoCanvas, delay);
            }

            if (botonEliminarProgreso != null)
            {
                AparecerUI(botonEliminarProgreso, botonEliminarProgresoCanvas, delay);
            }

            if (botonLogin != null)
            {
                botonLogin.SetActive(false);
            }
        }
        else
        {
            if (botonLogin != null)
            {
                AparecerUI(botonLogin, botonLoginCanvas, delay);
            }

            if (textoSaludo != null)
            {
                textoSaludo.gameObject.SetActive(false);
            }

            if (botonCerrarSesion != null)
            {
                botonCerrarSesion.SetActive(false);
            }

            if (botonEnviarCorreo != null)
            {
                botonEnviarCorreo.SetActive(false);
            }

            if (botonEliminarProgreso != null)
            {
                botonEliminarProgreso.SetActive(false);
            }
        }
    }

    private void AparecerUI(GameObject objeto, CanvasGroup canvasGroup, float delay)
    {
        if (objeto == null || canvasGroup == null) return;

        objeto.SetActive(true);

        RectTransform rect = objeto.GetComponent<RectTransform>();

        Vector2 posicionFinal = rect.anchoredPosition;
        rect.anchoredPosition = new Vector2(posicionFinal.x, posicionFinal.y - 80f);

        LeanTween.alphaCanvas(canvasGroup, 1f, 1.5f)
            .setDelay(delay)
            .setEase(LeanTweenType.easeInOutQuart);

        LeanTween.moveY(rect, posicionFinal.y, 1.5f)
            .setDelay(delay)
            .setEase(LeanTweenType.easeInOutQuart)
            .setOnComplete(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
    }

    private void escalar(float x, float y, float duracion, float delay, GameObject imagen)
    {
        LeanTween.scale(imagen.GetComponent<RectTransform>(), new Vector3(x, y, 0), duracion)
            .setDelay(delay)
            .setEase(LeanTweenType.easeInOutQuart);
    }

    private void moverY(float y, float duracion, float delay, GameObject imagen)
    {
        LeanTween.moveY(imagen.GetComponent<RectTransform>(), y, duracion)
            .setDelay(delay)
            .setEase(LeanTweenType.easeInOutQuart);
    }

    private void alfa(float valore, float duracion, float delay, GameObject imagen)
    {
        LeanTween.alpha(imagen.GetComponent<RectTransform>(), valore, duracion)
            .setDelay(delay)
            .setEase(LeanTweenType.easeInOutQuart);
    }
}