using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baycambio : MonoBehaviour
{
    public GameObject imagen;
    public float velAnim = 3f;
    void Start()
    {
        imagen.SetActive(true);
        LeanTween.alpha(imagen, 0f, velAnim).setEase(LeanTweenType.easeInOutQuart);
        Invoke("chau", velAnim);
    }

    public void chau(){
        imagen.SetActive(false);
    }
}
