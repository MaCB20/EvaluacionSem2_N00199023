using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator an;
    private float moveHorizontal;
    private float moveVertical;

    [SerializeField]
    private float moveSpeed;
    private bool isFlying = true;

    const int A_Idle = 0;
    const int A_Walk = 1;
    const int A_Fly = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        an = GetComponent<Animator>();
    }
    void Update()
    {
        Idle();
        Walk();
    }
    void Idle()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        SetAnimation(A_Idle);
    }
    void Walk()
    {
        moveVertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, moveVertical * moveSpeed);

        moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        if(moveHorizontal > 0.1f) 
        {
            sr.flipX = false;
            SetAnimation(A_Walk);
        }
        if(moveHorizontal < 0) 
        {
            sr.flipX = true;
            SetAnimation(A_Walk);
        }
        if(moveVertical > 0.1f) 
        {
            sr.flipX = false;
            isFlying = false;
            SetAnimation(A_Fly);
        }
        if(moveVertical < 0) 
        {
            sr.flipX = true;
            isFlying = false;
            SetAnimation(A_Fly);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        isFlying = true;
    }
    private void SetAnimation(int animation)
    {
        an.SetInteger("Estado", animation);
    }
}
