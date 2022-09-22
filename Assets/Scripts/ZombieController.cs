using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float walkSpeed;

    [HideInInspector]
    public bool mustPatrol; //Debe Patrullar
    private bool mustTurn;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;

    SpriteRenderer sr;
    Animator an;
    const int A_Caminar = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        an = GetComponent<Animator>();

        mustPatrol = true;
    }
    void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }
    }
    private void FixedUpdate()
    {
        if(mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }
    void Patrol()
    {
        if(mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void Flip() //Función para dar vuelta
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
    void ChangeAnimation(int animation)
    {
        an.SetInteger("Estado", animation);
    }
}
