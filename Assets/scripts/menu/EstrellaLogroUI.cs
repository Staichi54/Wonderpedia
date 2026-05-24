using UnityEngine;
using UnityEngine.UI;

public class EstrellaLogroUI : MonoBehaviour
{
    public enum TipoLogro
    {
        Ingles,
        Mates,
        Historia
    }

    [Header("Configuración")]
    public TipoLogro tipoLogro;

    [Header("Estrella")]
    public GameObject estrella;

    private void Start()
    {
        ActualizarEstrella();
    }

    private void OnEnable()
    {
        ActualizarEstrella();
    }

    public void ActualizarEstrella()
    {
        if (estrella == null)
        {
            Debug.LogWarning("No se asignó la estrella en " + gameObject.name);
            return;
        }

        // Hace que la estrella quede por delante del botón/libro en UI
        estrella.transform.SetAsLastSibling();

        // Evita que la estrella bloquee el clic del botón
        Image imagenEstrella = estrella.GetComponent<Image>();

        if (imagenEstrella != null)
        {
            imagenEstrella.raycastTarget = false;
            imagenEstrella.preserveAspect = true;
        }

        int usuarioId = PlayerPrefs.GetInt("UsuarioId", 0);
        bool haySesion = usuarioId > 0;

        if (!haySesion)
        {
            estrella.SetActive(false);
            return;
        }

        bool logroCompletado = false;

        switch (tipoLogro)
        {
            case TipoLogro.Ingles:
                logroCompletado = PlayerPrefs.GetInt("FinalizarIngles", 0) == 1;
                break;

            case TipoLogro.Mates:
                logroCompletado = PlayerPrefs.GetInt("FinalizarMates", 0) == 1;
                break;

            case TipoLogro.Historia:
                logroCompletado = PlayerPrefs.GetInt("FinalizarHistoria", 0) == 1;
                break;
        }

        estrella.SetActive(logroCompletado);
    }
}