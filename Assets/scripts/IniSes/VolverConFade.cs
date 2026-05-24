using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverConFade : MonoBehaviour
{
    [Header("Imagen morada que cubre la pantalla")]
    public GameObject imagenMorada;

    [Header("Duración del difuminado")]
    public float duracion = 1f;

    [Header("Escena a cargar")]
    public string escenaDestino = "inicio";

    public void Volver()
    {
        StartCoroutine(VolverCoroutine());
    }

    IEnumerator VolverCoroutine()
    {
        imagenMorada.SetActive(true);

        LeanTween.alpha(imagenMorada.GetComponent<RectTransform>(), 1f, duracion)
            .setEase(LeanTweenType.easeInOutQuart);

        yield return new WaitForSeconds(duracion);

        SceneManager.LoadScene(escenaDestino);
    }
}