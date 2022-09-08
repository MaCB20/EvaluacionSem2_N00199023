using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator an;
    public float velocity;
    public float velocityForce = 1.5f;
    public float jumpForce = 5;
    public GameObject bullet;
    bool Jump = true;
    public int maxSaltos = 2;
    public int saltoActual = 0;
    const int A_Quieto = 0;
    const int A_Caminar = 1;
    const int A_Correr = 2;
    const int A_Saltar = 3;
    const int A_Atacar = 4;

    public GameObject bolt;
    public Transform Canon;
    public float fireRatep;
    public float fireRatem;
    private float nextFire = 0.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        an = GetComponent<Animator>();

        nextFire = nextFire + Random.Range (fireRatep, fireRatem);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (Jump || maxSaltos > saltoActual))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Jump = false;
            saltoActual++;
            ChangeAnimation(A_Saltar);
        }
        // else if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X))
        // {
            // rb.velocity = new Vector2(velocity * velocityForce, rb.velocity.y);
            // sr.flipX = false;
            // ChangeAnimation(A_Correr);
        // }
        // else if(Input.GetKey(KeyCode.RightArrow))
        // {
        //     rb.velocity = new Vector2(velocity, rb.velocity.y);
        //     sr.flipX = false;
        //     ChangeAnimation(A_Caminar);
        // }
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
            if(sr.flipX == true)
            {
                var bulletPosition = transform.position + new Vector3(1, 0, 0);
                var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BulletController>();
                c.SetLeftDirection();
                //c.SetScoreText(scoreText);
                ChangeAnimation(A_Atacar);
                
            }
            else 
            {
                var bulletPosition = transform.position + new Vector3(1, 0, 0);
                var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BulletController>();
                c.SetRightDirection();
                //c.SetScoreText(scoreText);
                ChangeAnimation(A_Atacar);
            }
        }
        else
        {
            // rb.velocity = new Vector2(0, rb.velocity.y);
            // ChangeAnimation(A_Quieto);
            rb.velocity = new Vector2(velocity * velocityForce, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(A_Correr);
        }
        // rb.velocity = new Vector2(velocity * velocityForce, rb.velocity.y);
        // sr.flipX = false;
        // ChangeAnimation(A_Correr);
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Jump = true;
        saltoActual = 0;
    }
    void ChangeAnimation(int animation)
    {
        an.SetInteger("Estado", animation);
    }
    // void FixedUpdate ()
    // {
    //     if ((Time.time > nextFire) && (GameObject.FindWithTag("Enemy").Length < 5)) 
    //     {
    //         nextFire = Time.time + Random.Range (fireRatep, fireRatem);
    //         Instantiate (bolt, Canon.position, Canon.rotation);
    //     }
    // }
}
