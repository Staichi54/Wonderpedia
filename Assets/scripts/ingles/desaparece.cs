using UnityEngine;
using UnityEngine.UIElements;

public class desaparece : MonoBehaviour
{
    public bool yaseFue;
    private dialogoNPC dialog;
    private BoxCollider2D caja;
    void Start(){
        yaseFue = false;
        caja = GetComponent<BoxCollider2D>();
        dialog = GetComponent<dialogoNPC>();
    }

    void Update(){
        if(dialog.indexLin == dialog.lineasDialogo.Length && !yaseFue){
            yaseFue = true;
            caja.enabled = false;
            LeanTween.alpha(gameObject, 0f, 1.5f);
            Invoke("chauW", 2f);
        }
    }

    void chauW(){
        gameObject.SetActive(false);
    }

}
