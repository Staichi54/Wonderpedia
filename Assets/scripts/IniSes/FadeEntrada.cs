using UnityEngine;

public class FadeEntrada : MonoBehaviour
{
    [Header("Imagen negra que cubre la pantalla")]
    public GameObject imagenNegra;

    [Header("Duración del difuminado")]
    public float duracion = 2f;

    void Start()
    {
        imagenNegra.SetActive(true);

        LeanTween.alpha(imagenNegra.GetComponent<RectTransform>(), 0f, duracion)
            .setEase(LeanTweenType.easeInOutQuart)
            .setOnComplete(() =>
            {
                imagenNegra.SetActive(false);
            });
    }
}