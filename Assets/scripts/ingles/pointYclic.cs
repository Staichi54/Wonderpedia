using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointYclic : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 targetPosition; 
    public bool isMoving = false, sePuedeMover;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator anim;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        sePuedeMover = true;
    }

    void Update()
    {
        if(sePuedeMover){
            if (Input.GetMouseButtonDown(0) && !isMoving) {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.y = transform.position.y;
                isMoving = true;
                
                if (targetPosition.x > transform.position.x)
                {
                    //Debug.Log("Clic a la derecha del personaje");
                    sprite.flipX = false;
                    anim.SetFloat("run", 10);
                }
                else
                {
                    //Debug.Log("Clic a la izquierda del personaje");
                    sprite.flipX = true;
                    anim.SetFloat("run", 10);
                }
            }

        }
        
        if (isMoving) 
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f) 
            {
                anim.SetFloat("run", 0);
                isMoving = false;
            }
        }
    }
}
