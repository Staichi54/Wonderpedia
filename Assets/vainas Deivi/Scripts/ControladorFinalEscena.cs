using UnityEngine;

public class ControladorFinalEscena : MonoBehaviour
{
    [Header("Botón que siempre está visible")]
    [SerializeField] private GameObject botonVolver;

    [Header("Panel final")]
    [SerializeField] private GameObject panelFinal;

    private void Start()
    {
        if (panelFinal != null)
        {
            panelFinal.SetActive(false);
        }

        if (botonVolver != null)
        {
            botonVolver.SetActive(true);
        }
    }

    public void MostrarPanelFinal()
    {
        if (botonVolver != null)
        {
            botonVolver.SetActive(false);
        }

        if (panelFinal != null)
        {
            panelFinal.SetActive(true);
        }
    }
}