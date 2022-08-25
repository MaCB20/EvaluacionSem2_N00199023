using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator an;
    public float Horizontal;
    public float velocity = 5f;
    public float jumpForce;
    bool jump = true;
    bool atacar = true;
    const int A_Quieto = 0;
    const int A_Caminar = 1;
    const int A_Saltar = 2;
    const int A_Correr = 3;
    const int A_Atacar = 4;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        an = GetComponent<Animator>();
    }
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal") * velocity;
        ChangeAnimation(A_Quieto);

        if(Horizontal > 0.0f)
        {
            sr.flipX = false;
            ChangeAnimation(A_Caminar);
        }   
        else if(Horizontal < 0.0f)
        {
            sr.flipX = true;
            ChangeAnimation(A_Caminar);
        }

        if(Input.GetKeyDown(KeyCode.Space) && jump)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump = false;
            ChangeAnimation(A_Saltar);
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            atacar = false;
            ChangeAnimation(A_Atacar);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            velocity = 15f;
            ChangeAnimation(A_Correr);

        } else {
            velocity = 5f;
        }
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        jump = true;
        atacar = true;
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Horizontal, rb.velocity.y);
    }
    void ChangeAnimation(int animation)
    {
        an.SetInteger("Estado", animation);
    }
}
