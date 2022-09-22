using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator an;
    public float velocity;
    public float velocityForce = 1.5f;
    public float jumpForce = 5;
    public int maxSaltos = 2;
    public int saltoActual = 0;
    int contbullets = 0;

    [HideInInspector]
    public bool usingLadder = false;
    [HideInInspector]
    public bool isGrounded = true;
    public GameObject bullet;
    AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip bulletClip;
    public AudioClip powerClip;
    public AudioClip coinClip;
    
    private RespawnScript respawn;
    private BoxCollider2D checkpoint;

    bool Jump = true;
    const int A_Quieto = 0;
    const int A_Caminar = 1;
    const int A_Correr = 2;
    const int A_Saltar = 3;
    const int A_Atacar = 4;
    const int A_Desliz = 5;
    private GameManagerController gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        an = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
        
        gameManager = FindObjectOfType<GameManagerController>();

        checkpoint = GetComponent<BoxCollider2D>();

        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();
    }
    void Update()
    {
        if (!usingLadder)
        {
            an.SetFloat("vSpeed", rb.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            ChangeAnimation(A_Desliz);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && (Jump || maxSaltos > saltoActual))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Jump = false;
            saltoActual++;
            ChangeAnimation(A_Saltar);
            audioSource.PlayOneShot(jumpClip);
            
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
            ChangeAnimation(A_Correr);
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
            ChangeAnimation(A_Correr);
        }
        else if(Input.GetKeyDown(KeyCode.Z) && (contbullets < 5))
        {
            if(sr.flipX == true)
            {
                var bulletPosition = transform.position + new Vector3(-1, 0, 0);
                var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BulletController>();
                c.SetLeftDirection();
                contbullets++;
                //c.SetScoreText(scoreText);
                ChangeAnimation(A_Atacar);
                audioSource.PlayOneShot(bulletClip);
                
            }
            else 
            {
                var bulletPosition = transform.position + new Vector3(1, 0, 0);
                var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BulletController>();
                c.SetRightDirection();
                contbullets++;
                //c.SetScoreText(scoreText);
                ChangeAnimation(A_Atacar);
                audioSource.PlayOneShot(bulletClip);
            }
        }
        // else if(Input.GetKey(KeyCode.C))
        // {
        //     ChangeAnimation(A_Desliz);
        // }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(A_Quieto);

            //Código para correr automaticamente

            // rb.velocity = new Vector2(velocity * velocityForce, rb.velocity.y);
            // sr.flipX = false;
            // ChangeAnimation(A_Correr);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Jump = true;
        saltoActual = 0;

        if(other.gameObject.tag == "Checkpoint")
        {
            gameManager.SaveGame();
        }
        if(other.gameObject.tag == "Poder")
        {
            Destroy(other.gameObject);
            an.transform.localScale = new Vector3(0.4f, 0.4f,0.4f);
            audioSource.PlayOneShot(powerClip);
        }
        if(other.gameObject.tag == "Balas")
        {
            Destroy(other.gameObject);
            contbullets = contbullets - 5;
        }
        if(other.gameObject.tag == "Bronze")
        {
            Destroy(other.gameObject);
            gameManager.GanarPuntos(10);
            gameManager.GanarBronzeCoins(1);
            audioSource.PlayOneShot(coinClip);
            gameManager.SaveGame();
        }
        if(other.gameObject.tag == "Silver")
        {
            Destroy(other.gameObject);
            gameManager.GanarPuntos(20);
            gameManager.GanarSilverCoins(1);
            audioSource.PlayOneShot(coinClip);
            gameManager.SaveGame();
        }
        if(other.gameObject.tag == "Gold")
        {
            Destroy(other.gameObject);
            gameManager.GanarPuntos(30);
            gameManager.GanarGoldenCoins(1);
            audioSource.PlayOneShot(coinClip);
            gameManager.SaveGame();
        }
        if(other.gameObject.tag == "NextScene")
        {
            SceneManager.LoadScene(3);
            gameManager.SaveGame();
        }
    }
    void ChangeAnimation(int animation)
    {
        an.SetInteger("Estado", animation);
    }
}
