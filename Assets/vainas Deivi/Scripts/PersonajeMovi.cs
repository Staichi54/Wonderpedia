using System;
using UnityEngine;

public class PersonaMovi : MonoBehaviour
{
    [SerializeField] private float velocidad; private Rigidbody2D rig; private Animator anim; private SpriteRenderer spritePersonaje; private Dialog dialogo; // Referencia al script del di�logo
   public bool prendido;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spritePersonaje = GetComponentInChildren<SpriteRenderer>();
    }

    void Start(){
        prendido = true;
    }

    private void FixedUpdate()
    {
        if(prendido){
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            rig.velocity = new Vector2(horizontal, vertical) * velocidad;
            anim.SetFloat("Camina", Mathf.Abs(rig.velocity.magnitude));
            if (horizontal > 0)
            {
                spritePersonaje.flipX = false;
            }
            else if (horizontal < 0)
            {
                spritePersonaje.flipX = true;
            }
        }else{
            anim.SetFloat("Camina", 0);
            rig.velocity = new Vector2(0, 0);
        }
        
        // Detectar la entrada de teclado para iniciar el di�logo
        if (Input.GetKeyDown(KeyCode.Space) && dialogo != null && dialogo.IsPlayerInRange())
        {
               dialogo.CargarDialogo(0); // Cargar el texto del primer di�logo
        }
    }
}

