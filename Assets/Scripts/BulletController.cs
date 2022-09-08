using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    public float velocity = 20;
    private GameManagerController gameManager;
    float realVelocity;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public void SetRightDirection()
    {
        realVelocity = velocity;

    }
    public void SetLeftDirection()
    {
        realVelocity = -velocity;
        
    }
    // public void SetScoreText(Text scoreText)
    // {
    //     this.scoreText = scoreText;
    // }
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        //Destroy(this.gameObject, 0.1f); //Este elemento se va a ejecutar despues de 5 segundos
        Destroy(this.gameObject, 5);

    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(realVelocity, 0);
    }
    void OnCollisionEnter2D(Collision2D other){

        Destroy(this.gameObject);
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            //gameManager.GanarPuntos(10);
            gameManager.restarBalas(1);
            //scoreText.text = "Puntaje Modificado";
            
        }
    }
}
