using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cambiadordesena : MonoBehaviour
{
    public string sceneIndex;
    public variablesNPCMundoAbierto variNPC;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && variNPC.sumaYaHablaron == 3)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
