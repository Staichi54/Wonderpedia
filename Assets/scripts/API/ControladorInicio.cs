using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControladorInicio : MonoBehaviour
{
    [Header("UI")]
    public GameObject botonIniciarSesion;
    public GameObject botonCerrarSesion;
    public TMP_Text textoSaludo;

    [Header("Botones de usuario logueado")]
    public GameObject botonEnviarCorreo;
    public GameObject botonEliminarProgreso;

    [Header("Objetos que desaparecen al cerrar sesión")]
    public GameObject[] objetosADesaparecer;

    [Header("Escenas")]
    public string escenaLogin = "Login";

    [Header("Transición")]
    public cambiarescena transicionEscena;

    [Header("Cierre de sesión")]
    public float velocidadCerrarSesion = 1f;

    private void Start()
    {
        if (transicionEscena == null)
        {
            transicionEscena = GetComponent<cambiarescena>();
        }

        ActualizarUIInicioSesion();
    }

    private void ActualizarUIInicioSesion()
    {
        int usuarioId = PlayerPrefs.GetInt("UsuarioId", 0);
        string token = PlayerPrefs.GetString("JWT", "");
        string nombreUsuario = PlayerPrefs.GetString("UsuarioNombre", "");

        bool haySesion = usuarioId > 0 && !string.IsNullOrEmpty(token);

        if (haySesion)
        {
            if (botonIniciarSesion != null)
            {
                botonIniciarSesion.SetActive(false);
            }

            if (botonCerrarSesion != null)
            {
                botonCerrarSesion.SetActive(true);
            }

            if (botonEnviarCorreo != null)
            {
                botonEnviarCorreo.SetActive(true);
            }

            if (botonEliminarProgreso != null)
            {
                botonEliminarProgreso.SetActive(true);
            }

            if (textoSaludo != null)
            {
                textoSaludo.gameObject.SetActive(true);
                textoSaludo.text = "Bienvenido, " + nombreUsuario;
            }

            Debug.Log("Sesión activa. Usuario: " + nombreUsuario);
        }
        else
        {
            if (botonIniciarSesion != null)
            {
                botonIniciarSesion.SetActive(true);
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

            if (textoSaludo != null)
            {
                textoSaludo.text = "";
                textoSaludo.gameObject.SetActive(false);
            }

            Debug.Log("No hay sesión activa.");
        }
    }

    public void IrALogin()
    {
        if (transicionEscena != null)
        {
            transicionEscena.CambiarEscena(escenaLogin);
        }
        else
        {
            SceneManager.LoadScene(escenaLogin);
        }
    }

    public void CerrarSesion()
    {
        StartCoroutine(CerrarSesionCoroutine());
    }

    private IEnumerator CerrarSesionCoroutine()
    {
        PlayerPrefs.DeleteKey("UsuarioId");
        PlayerPrefs.DeleteKey("UsuarioNombre");
        PlayerPrefs.DeleteKey("UsuarioCorreo");
        PlayerPrefs.DeleteKey("JWT");

        PlayerPrefs.DeleteKey("FinalizarIngles");
        PlayerPrefs.DeleteKey("FinalizarMates");
        PlayerPrefs.DeleteKey("FinalizarHistoria");

        PlayerPrefs.Save();

        Debug.Log("Sesión cerrada.");

        yield return StartCoroutine(DesaparecerObjetos());

        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }

    public void EliminarProgreso()
    {
        StartCoroutine(EliminarProgresoCoroutine());
    }

    private IEnumerator EliminarProgresoCoroutine()
    {
        int usuarioId = PlayerPrefs.GetInt("UsuarioId", 0);
        string token = PlayerPrefs.GetString("JWT", "");

        if (usuarioId <= 0 || string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("No hay sesión iniciada. No se puede eliminar el progreso.");
            yield break;
        }

        if (WonderpediaAPI.Instance == null)
        {
            Debug.LogError("No se encontró WonderpediaAPI.Instance en la escena inicio.");
            yield break;
        }

        bool progresoEliminado = false;

        yield return StartCoroutine(WonderpediaAPI.Instance.EliminarProgresoCoroutine(usuarioId, resultado =>
        {
            progresoEliminado = resultado;
        }));

        if (!progresoEliminado)
        {
            Debug.LogError("No se pudo eliminar el progreso. No se reiniciará la escena.");
            yield break;
        }

        Debug.Log("Progreso eliminado. Desapareciendo objetos...");

        yield return StartCoroutine(DesaparecerObjetos());

        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }

    public void EnviarProgresoCorreo()
    {
        int usuarioId = PlayerPrefs.GetInt("UsuarioId", 0);
        string token = PlayerPrefs.GetString("JWT", "");

        if (usuarioId <= 0 || string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("No hay sesión iniciada. No se puede enviar el progreso.");
            return;
        }

        if (WonderpediaAPI.Instance == null)
        {
            Debug.LogError("No se encontró WonderpediaAPI.Instance en la escena inicio.");
            return;
        }

        WonderpediaAPI.Instance.EnviarProgresoCorreo();

        Debug.Log("Solicitud enviada a la API para enviar el progreso por correo.");
    }

    private IEnumerator DesaparecerObjetos()
    {
        float tiempo = 0f;

        while (tiempo < velocidadCerrarSesion)
        {
            tiempo += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, tiempo / velocidadCerrarSesion);

            foreach (GameObject objeto in objetosADesaparecer)
            {
                CambiarAlpha(objeto, alpha);
            }

            yield return null;
        }

        foreach (GameObject objeto in objetosADesaparecer)
        {
            CambiarAlpha(objeto, 0f);
        }
    }

    private void CambiarAlpha(GameObject objeto, float alpha)
    {
        if (objeto == null) return;

        CanvasGroup canvasGroup = objeto.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        Graphic[] graficos = objeto.GetComponentsInChildren<Graphic>(true);

        foreach (Graphic grafico in graficos)
        {
            Color color = grafico.color;
            color.a = alpha;
            grafico.color = color;
        }

        SpriteRenderer[] sprites = objeto.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer sprite in sprites)
        {
            Color color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }
    }
}