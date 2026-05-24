using UnityEngine;

public class PanelesLoginSlide : MonoBehaviour
{
    [Header("Paneles")]
    public RectTransform panelLogin;
    public RectTransform panelRegistro;

    [Header("Animación")]
    public float duracion = 0.6f;
    public LeanTweenType tipoAnimacion = LeanTweenType.easeInOutQuart;

    private Vector2 loginPosInicial;
    private Vector2 registroPosInicial;

    private Vector2 loginPosFueraIzquierda;
    private Vector2 registroPosVisible;

    private CanvasGroup canvasLogin;
    private CanvasGroup canvasRegistro;

    private bool animando = false;

    void Start()
    {
        loginPosInicial = panelLogin.anchoredPosition;
        registroPosInicial = panelRegistro.anchoredPosition;

        float distanciaEntrePaneles = registroPosInicial.x - loginPosInicial.x;

        loginPosFueraIzquierda = new Vector2(
            loginPosInicial.x - distanciaEntrePaneles,
            loginPosInicial.y
        );

        registroPosVisible = new Vector2(
            loginPosInicial.x,
            registroPosInicial.y
        );

        canvasLogin = PrepararCanvasGroup(panelLogin.gameObject);
        canvasRegistro = PrepararCanvasGroup(panelRegistro.gameObject);

        MostrarLoginInstantaneo();
    }

    CanvasGroup PrepararCanvasGroup(GameObject obj)
    {
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();

        if (cg == null)
        {
            cg = obj.AddComponent<CanvasGroup>();
        }

        return cg;
    }

    public void IrARegistro()
    {
        if (animando) return;

        animando = true;

        canvasLogin.interactable = false;
        canvasLogin.blocksRaycasts = false;

        canvasRegistro.interactable = false;
        canvasRegistro.blocksRaycasts = false;

        LeanTween.move(panelLogin, loginPosFueraIzquierda, duracion)
            .setEase(tipoAnimacion);

        LeanTween.move(panelRegistro, registroPosVisible, duracion)
            .setEase(tipoAnimacion)
            .setOnComplete(() =>
            {
                canvasRegistro.interactable = true;
                canvasRegistro.blocksRaycasts = true;
                animando = false;
            });
    }

    public void IrALogin()
    {
        if (animando) return;

        animando = true;

        canvasLogin.interactable = false;
        canvasLogin.blocksRaycasts = false;

        canvasRegistro.interactable = false;
        canvasRegistro.blocksRaycasts = false;

        LeanTween.move(panelLogin, loginPosInicial, duracion)
            .setEase(tipoAnimacion);

        LeanTween.move(panelRegistro, registroPosInicial, duracion)
            .setEase(tipoAnimacion)
            .setOnComplete(() =>
            {
                canvasLogin.interactable = true;
                canvasLogin.blocksRaycasts = true;
                animando = false;
            });
    }

    void MostrarLoginInstantaneo()
    {
        panelLogin.anchoredPosition = loginPosInicial;
        panelRegistro.anchoredPosition = registroPosInicial;

        canvasLogin.alpha = 1f;
        canvasRegistro.alpha = 1f;

        canvasLogin.interactable = true;
        canvasLogin.blocksRaycasts = true;

        canvasRegistro.interactable = false;
        canvasRegistro.blocksRaycasts = false;
    }
}