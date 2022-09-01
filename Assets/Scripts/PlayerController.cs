using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator an;
    public float velocity = 5;
    public float velocityForce = 1.5f;
    public float jumpForce = 5;
    private Vector3 lastCheckpointPosition;
    bool Jump = true;
    const int A_Quieto = 0;
    const int A_Caminar = 1;
    const int A_Correr = 2;
    const int A_Saltar = 3;
    const int A_Atacar = 4;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        an = GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Jump)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Jump = false;
            ChangeAnimation(A_Saltar);
        }
        else if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector2(velocity * velocityForce, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(A_Correr);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(A_Caminar);
        }
        else if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector2(-velocity * velocityForce, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(A_Correr);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(A_Caminar);
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            ChangeAnimation(A_Atacar);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(A_Quieto);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Jump = true;
        if(other.gameObject.name == "DarkHole")
        {
            if(lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Save");
        lastCheckpointPosition = transform.position;
    }
    void ChangeAnimation(int animation)
    {
        an.SetInteger("Estado", animation);
    }
}
