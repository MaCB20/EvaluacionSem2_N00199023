using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float velocity = 20;
    float realVelocity;
    private GameManagerController gameManager;

    public float dieTime, damage;
    public GameObject diePEffect;
    public void SetRightDirection()
    {
        realVelocity = velocity;
    }
    public void SetLeftDirection()
    {
        realVelocity = -velocity;
    }
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

        GameObject collisionGameObject = other.gameObject;

        Destroy(this.gameObject);
        //gameManager.restarBalas();
        if(other.gameObject.tag == "Enemy")
        {
            if(collisionGameObject.GetComponent<HealthScript>() != null)
            {
                collisionGameObject.GetComponent<HealthScript>().TakeDamage(damage);
            }
            //Destroy(other.gameObject);
            Die();
           // gameManager.GanarPuntos(10);
            gameManager.SaveGame();
        }
    }
    void Die()
    {
        if(diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
