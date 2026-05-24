using System.Collections;
using TMPro;
using UnityEngine;

public class numMAtes : MonoBehaviour
{
    public TextMeshProUGUI textoTMP;
    public float tiempo = 0.1f, inicio = 2;

    void Start()
    {
        StartCoroutine(EscribirNumerosAleatorios());
    }

    private readonly string[] numeros = {
        "0", "20", "360", "7165", "10238", "715052", "2540351",
        "71025505", "397125252", "2584125782", "84269324685",
        "87126552638", "20658168281", "74152515105", "M5273685172",
        "Ma632418532", "Mat84235723", "Mate8465471", "Matem416581", "Matema18205",
        "Matemat3941", "Matemati702", "Matematic06", "Matematica9", "Matematicas"
    };

    IEnumerator EscribirNumerosAleatorios()
    {
        yield return new WaitForSeconds(inicio);

        foreach (string numero in numeros)
        {
            textoTMP.text = numero;
            yield return new WaitForSeconds(tiempo);
        }
    }

    private void alfa(float valore, float duracion, float delay, GameObject imagen){
        LeanTween.alpha(imagen.GetComponent<RectTransform>(), valore, duracion)
        .setDelay(delay)
        .setEase(LeanTweenType.easeInOutQuart);
    }
}
