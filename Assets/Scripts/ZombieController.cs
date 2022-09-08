using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator an;
    public float velocity;
    const int A_Caminar = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-velocity, rb.velocity.y);
        sr.flipX = true;
        ChangeAnimation(A_Caminar);
    }
    void ChangeAnimation(int animation)
    {
        an.SetInteger("Estado", animation);
    }
}
