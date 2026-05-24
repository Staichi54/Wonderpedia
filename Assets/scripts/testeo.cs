using UnityEngine;
using UnityEngine.UI;

public class testeo : MonoBehaviour
{
    public Button botonEnviar; // Referencia al botón de la UI

    void Start()
    {
        botonEnviar.onClick.AddListener(EnviarMensaje); // Asignar función al clic
    }

    public void EnviarMensaje()
    {
        Debug.Log("¡Hola mundo desde el botón!");
    }
}
