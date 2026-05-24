using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class botonFinalMateria : MonoBehaviour
{
    public Button terminar;
    public ParticleSystem sistemaDeParticulas;
    public string materia;
    public GameObject personaje;

    void Start()
    {
        terminar.onClick.AddListener(terminoMateria);
        sistemaDeParticulas.Play();
        personaje.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    private void terminoMateria(){
        PlayerPrefs.SetInt(materia, 1);
        PlayerPrefs.Save();
        LeanTween.alpha(personaje, 0f, 1.5f);
        sistemaDeParticulas.Stop();
    }
}
