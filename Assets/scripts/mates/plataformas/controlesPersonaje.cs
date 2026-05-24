using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlesPersonaje : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 500f, multiCaida = 2.5f, lowJumpMilti = 2f;
    public bool prendido = true;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (prendido){
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            if (Input.GetKey("left"))
            {
                sprite.flipX = true;
                anim.SetFloat("run", rb.velocity.x);
            }
            else if (Input.GetKey("right"))
            {
                sprite.flipX = false;
                anim.SetFloat("run", rb.velocity.x);
            }
            else
            {
                anim.SetFloat("run", rb.velocity.x);
            }

            if (Input.GetKey("space") && detectorPiso.isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }else{
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetFloat("run", 0);
        }


        if(detectorPiso.isGrounded){
            anim.SetBool("Jump", false);
        }

        if(!detectorPiso.isGrounded){
            anim.SetBool("Jump", true);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (multiCaida - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMilti - 1) * Time.deltaTime;
        }
    }
}
