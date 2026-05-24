using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiarescena : MonoBehaviour
{
    public GameObject imagen;
    public float velocidadAnimacion = 1f;

    public void CambiarEscena(string nombreEscena)
    {
        StartCoroutine(Cambio(imagen, nombreEscena));
    }

    IEnumerator Cambio(GameObject imagen, string nombreEscena)
    {
        // Mostrar imagen y animar su alpha a 1
        imagen.SetActive(true);
        LeanTween.alpha(imagen, 1f, velocidadAnimacion)
            .setEase(LeanTweenType.easeInOutQuart);

        // Esperar a que la animación termine
        yield return new WaitForSeconds(velocidadAnimacion);

        // Cargar la siguiente escena
        SceneManager.LoadScene(nombreEscena);

    }
}
