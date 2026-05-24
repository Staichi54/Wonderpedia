using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class WonderpediaAPI : MonoBehaviour
{
    public static WonderpediaAPI Instance;

    [Header("API")]
    [SerializeField] private string baseUrl = "https://localhost:7030/api/Usuarios";

    [Header("Login UI")]
    public TMP_InputField loginUsuarioOCorreoInput;
    public TMP_InputField loginPasswordInput;

    [Header("Registro UI")]
    public TMP_InputField registroNombreInput;
    public TMP_InputField registroCorreoInput;
    public TMP_InputField registroPasswordInput;

    [Header("Escenas")]
    public string escenaInicio = "inicio";
    public string escenaLogin = "Login";

    [Header("Transición Login")]
    public VolverConFade volverConFade;

    [System.Serializable]
    public class Usuario
    {
        public int id;
        public string nombre;
        public string correo;
        public bool finalizarIngles;
        public bool finalizarMates;
        public bool finalizarHistoria;
    }

    [System.Serializable]
    public class RegistroRequest
    {
        public string nombre;
        public string correo;
        public string password;
    }

    [System.Serializable]
    public class LoginRequest
    {
        public string nombreOCorreo;
        public string password;
    }

    [System.Serializable]
    public class AuthResponse
    {
        public string mensaje;
        public string token;
        public Usuario usuario;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == escenaLogin)
        {
            RevisarSesionGuardada();
        }
    }

    public void RevisarSesionGuardada()
    {
        int usuarioId = PlayerPrefs.GetInt("UsuarioId", 0);
        string token = PlayerPrefs.GetString("JWT", "");

        if (usuarioId > 0 && !string.IsNullOrEmpty(token))
        {
            Debug.Log("Sesión encontrada. Usuario ID: " + usuarioId);

            if (SceneManager.GetActiveScene().name != escenaInicio)
            {
                SceneManager.LoadScene(escenaInicio);
            }
        }
        else
        {
            Debug.Log("No hay sesión guardada.");
        }
    }

    public void RegistrarDesdeUI()
    {
        if (registroNombreInput == null || registroCorreoInput == null || registroPasswordInput == null)
        {
            Debug.LogError("Faltan referencias de registro en el Inspector.");
            return;
        }

        string nombre = registroNombreInput.text.Trim();
        string correo = registroCorreoInput.text.Trim();
        string password = registroPasswordInput.text;

        if (string.IsNullOrWhiteSpace(nombre) ||
            string.IsNullOrWhiteSpace(correo) ||
            string.IsNullOrWhiteSpace(password))
        {
            Debug.LogWarning("Debes llenar todos los campos de registro.");
            return;
        }

        StartCoroutine(RegistrarUsuarioCoroutine(nombre, correo, password));
    }

    private IEnumerator RegistrarUsuarioCoroutine(string nombre, string correo, string password)
    {
        RegistroRequest datos = new RegistroRequest
        {
            nombre = nombre,
            correo = correo,
            password = password
        };

        string json = JsonUtility.ToJson(datos);

        UnityWebRequest request = new UnityWebRequest(baseUrl + "/registrar", "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.certificateHandler = new CertificadoLocal();

        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("Enviando registro a: " + baseUrl + "/registrar");
        Debug.Log("JSON enviado: " + json);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Registro correcto:");
            Debug.Log(request.downloadHandler.text);

            AuthResponse response = JsonUtility.FromJson<AuthResponse>(request.downloadHandler.text);

            if (response != null && response.usuario != null && !string.IsNullOrEmpty(response.token))
            {
                GuardarSesion(response);
            }
            else
            {
                Debug.LogError("La respuesta del servidor no contiene usuario o token.");
            }
        }
        else
        {
            Debug.LogError("Error al registrar:");
            Debug.LogError("Código HTTP: " + request.responseCode);
            Debug.LogError("Error Unity: " + request.error);
            Debug.LogError("Respuesta servidor: " + request.downloadHandler.text);
        }

        request.Dispose();
    }

    public void LoginDesdeUI()
    {
        if (loginUsuarioOCorreoInput == null || loginPasswordInput == null)
        {
            Debug.LogError("Faltan referencias de login en el Inspector.");
            return;
        }

        string usuarioOCorreo = loginUsuarioOCorreoInput.text.Trim();
        string password = loginPasswordInput.text;

        if (string.IsNullOrWhiteSpace(usuarioOCorreo) ||
            string.IsNullOrWhiteSpace(password))
        {
            Debug.LogWarning("Debes llenar usuario/correo y contraseña.");
            return;
        }

        StartCoroutine(LoginCoroutine(usuarioOCorreo, password));
    }

    private IEnumerator LoginCoroutine(string usuarioOCorreo, string password)
    {
        LoginRequest datos = new LoginRequest
        {
            nombreOCorreo = usuarioOCorreo,
            password = password
        };

        string json = JsonUtility.ToJson(datos);

        UnityWebRequest request = new UnityWebRequest(baseUrl + "/login", "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.certificateHandler = new CertificadoLocal();

        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("Enviando login a: " + baseUrl + "/login");
        Debug.Log("JSON enviado: " + json);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Login correcto:");
            Debug.Log(request.downloadHandler.text);

            AuthResponse response = JsonUtility.FromJson<AuthResponse>(request.downloadHandler.text);

            if (response != null && response.usuario != null && !string.IsNullOrEmpty(response.token))
            {
                GuardarSesion(response);
            }
            else
            {
                Debug.LogError("La respuesta del servidor no contiene usuario o token.");
            }
        }
        else
        {
            Debug.LogError("Error al iniciar sesión:");
            Debug.LogError("Código HTTP: " + request.responseCode);
            Debug.LogError("Error Unity: " + request.error);
            Debug.LogError("Respuesta servidor: " + request.downloadHandler.text);
        }

        request.Dispose();
    }

    private void GuardarSesion(AuthResponse response)
    {
        Usuario usuario = response.usuario;

        PlayerPrefs.SetInt("UsuarioId", usuario.id);
        PlayerPrefs.SetString("UsuarioNombre", usuario.nombre);
        PlayerPrefs.SetString("UsuarioCorreo", usuario.correo);
        PlayerPrefs.SetString("JWT", response.token);

        PlayerPrefs.SetInt("FinalizarIngles", usuario.finalizarIngles ? 1 : 0);
        PlayerPrefs.SetInt("FinalizarMates", usuario.finalizarMates ? 1 : 0);
        PlayerPrefs.SetInt("FinalizarHistoria", usuario.finalizarHistoria ? 1 : 0);

        PlayerPrefs.Save();

        Debug.Log("Sesión guardada para: " + usuario.nombre);
        Debug.Log("ID guardado: " + usuario.id);
        Debug.Log("JWT guardado correctamente.");

        if (volverConFade != null)
        {
            volverConFade.escenaDestino = escenaInicio;
            volverConFade.Volver();
        }
        else
        {
            Debug.LogWarning("No se asignó VolverConFade. Cargando escena sin transición.");
            SceneManager.LoadScene(escenaInicio);
        }
    }

    public void CerrarSesion()
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

        SceneManager.LoadScene(escenaLogin);
    }

    public void LimpiarSesionLocal()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs limpiado.");
    }

    public int ObtenerUsuarioId()
    {
        return PlayerPrefs.GetInt("UsuarioId", 0);
    }

    public bool HaySesion()
    {
        int usuarioId = PlayerPrefs.GetInt("UsuarioId", 0);
        string token = PlayerPrefs.GetString("JWT", "");

        return usuarioId > 0 && !string.IsNullOrEmpty(token);
    }

    public string ObtenerNombreUsuario()
    {
        return PlayerPrefs.GetString("UsuarioNombre", "");
    }

    public string ObtenerToken()
    {
        return PlayerPrefs.GetString("JWT", "");
    }

    public bool ObtenerFinalizarIngles()
    {
        return PlayerPrefs.GetInt("FinalizarIngles", 0) == 1;
    }

    public bool ObtenerFinalizarMates()
    {
        return PlayerPrefs.GetInt("FinalizarMates", 0) == 1;
    }

    public bool ObtenerFinalizarHistoria()
    {
        return PlayerPrefs.GetInt("FinalizarHistoria", 0) == 1;
    }

    public void FinalizarIngles()
    {
        int id = ObtenerUsuarioId();
        StartCoroutine(ActualizarLogroCoroutine(id, "ingles"));
    }

    public void FinalizarMates()
    {
        int id = ObtenerUsuarioId();
        StartCoroutine(ActualizarLogroCoroutine(id, "mates"));
    }

    public void FinalizarHistoria()
    {
        int id = ObtenerUsuarioId();
        StartCoroutine(ActualizarLogroCoroutine(id, "historia"));
    }

    private IEnumerator ActualizarLogroCoroutine(int id, string logro)
    {
        if (id <= 0)
        {
            Debug.LogError("No hay usuario logueado.");
            yield break;
        }

        string token = ObtenerToken();

        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("No hay token JWT guardado. Inicia sesión nuevamente.");
            yield break;
        }

        string url = baseUrl + "/" + id + "/logro/" + logro;

        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.certificateHandler = new CertificadoLocal();

        request.SetRequestHeader("Authorization", "Bearer " + token);

        Debug.Log("Actualizando logro en: " + url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Logro actualizado: " + logro);
            Debug.Log(request.downloadHandler.text);

            if (logro == "ingles")
            {
                PlayerPrefs.SetInt("FinalizarIngles", 1);
            }
            else if (logro == "mates")
            {
                PlayerPrefs.SetInt("FinalizarMates", 1);
            }
            else if (logro == "historia")
            {
                PlayerPrefs.SetInt("FinalizarHistoria", 1);
            }

            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Error actualizando logro:");
            Debug.LogError("Código HTTP: " + request.responseCode);
            Debug.LogError("Error Unity: " + request.error);
            Debug.LogError("Respuesta servidor: " + request.downloadHandler.text);

            if (request.responseCode == 401)
            {
                Debug.LogError("Token inválido o expirado. Debes iniciar sesión nuevamente.");
            }

            if (request.responseCode == 403)
            {
                Debug.LogError("El token no corresponde al usuario que intentas modificar.");
            }
        }

        request.Dispose();
    }

    public void EliminarProgreso()
    {
        int id = ObtenerUsuarioId();

        if (id <= 0)
        {
            Debug.LogError("No hay usuario logueado.");
            return;
        }

        StartCoroutine(EliminarProgresoCoroutine(id));
    }

    public IEnumerator EliminarProgresoCoroutine(int id, System.Action<bool> alTerminar = null)
    {
        string token = ObtenerToken();

        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("No hay token JWT guardado. Inicia sesión nuevamente.");
            alTerminar?.Invoke(false);
            yield break;
        }

        string url = baseUrl + "/" + id + "/progreso/reset";

        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.certificateHandler = new CertificadoLocal();

        request.SetRequestHeader("Authorization", "Bearer " + token);

        Debug.Log("Eliminando progreso en: " + url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Progreso eliminado correctamente:");
            Debug.Log(request.downloadHandler.text);

            PlayerPrefs.SetInt("FinalizarIngles", 0);
            PlayerPrefs.SetInt("FinalizarMates", 0);
            PlayerPrefs.SetInt("FinalizarHistoria", 0);
            PlayerPrefs.Save();

            alTerminar?.Invoke(true);
        }
        else
        {
            Debug.LogError("Error eliminando progreso:");
            Debug.LogError("Código HTTP: " + request.responseCode);
            Debug.LogError("Error Unity: " + request.error);
            Debug.LogError("Respuesta servidor: " + request.downloadHandler.text);

            if (request.responseCode == 401)
            {
                Debug.LogError("Token inválido o expirado. Debes iniciar sesión nuevamente.");
            }

            if (request.responseCode == 403)
            {
                Debug.LogError("El token no corresponde al usuario que intentas modificar.");
            }

            if (request.responseCode == 404)
            {
                Debug.LogError("No se encontró el endpoint /progreso/reset o el usuario no existe.");
            }

            alTerminar?.Invoke(false);
        }

        request.Dispose();
    }

    public void EnviarProgresoCorreo()
    {
        int id = ObtenerUsuarioId();

        if (id <= 0)
        {
            Debug.LogError("No hay usuario logueado.");
            return;
        }

        StartCoroutine(EnviarProgresoCorreoCoroutine(id));
    }

    private IEnumerator EnviarProgresoCorreoCoroutine(int id)
    {
        string token = ObtenerToken();

        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("No hay token JWT guardado. Inicia sesión nuevamente.");
            yield break;
        }

        string url = baseUrl + "/" + id + "/enviar-progreso";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.certificateHandler = new CertificadoLocal();

        request.SetRequestHeader("Authorization", "Bearer " + token);

        Debug.Log("Enviando progreso por correo desde API: " + url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Correo enviado correctamente:");
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error enviando correo:");
            Debug.LogError("Código HTTP: " + request.responseCode);
            Debug.LogError("Error Unity: " + request.error);
            Debug.LogError("Respuesta servidor: " + request.downloadHandler.text);

            if (request.responseCode == 401)
            {
                Debug.LogError("Token inválido o expirado. Debes iniciar sesión nuevamente.");
            }

            if (request.responseCode == 403)
            {
                Debug.LogError("El token no corresponde al usuario que intentas enviar.");
            }

            if (request.responseCode == 500)
            {
                Debug.LogError("La API no pudo enviar el correo. Revisa SMTP/appsettings.json.");
            }
        }

        request.Dispose();
    }

    private class CertificadoLocal : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}