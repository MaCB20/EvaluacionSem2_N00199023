using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator an;
    private float moveHorizontal;
    private float moveVertical;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int maxJump = 2;
    int actualJump = 0;
    private bool isJumping = true;

    public float timer = 0;
    public float timer1 = 3.1f;

    Transform Shooting1;
    bool timerOn;
    int contbullets = 0;
    public GameObject bullet;
    public GameObject bullet1;
    public GameObject bullet2;

    const int A_Idle = 0;
    const int A_Walk = 1;
    const int A_Jump = 2;
    const int A_Attack = 3;

    private GameManagerController gameManager;
    
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
        Jump();
        Attack();
    }
    void Idle()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        SetAnimation(A_Idle);
    }
    void Walk()
    {
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
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (isJumping || maxJump > actualJump))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
            actualJump++;
            SetAnimation(A_Jump);
        }
    }
    void Attack()
    {
        if(Input.GetKeyUp(KeyCode.Z) && (contbullets < 100))
        {
            // if (timer < 3)
            // {
            //     //timerOn = true;
            //     if(sr.flipX == true)
            //     {
            //         var bulletPosition = transform.position + new Vector3(-1, 0, 0);
            //         var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            //         var c = o.GetComponent<BulletController>();
            //         c.SetLeftDirection();
            //         contbullets++;
            //         timer -= Time.deltaTime;
            //         SetAnimation(A_Attack);
            //     }
            //     else 
            //     {
            //         var bulletPosition = transform.position + new Vector3(1, 0, 0);
            //         var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            //         var c = o.GetComponent<BulletController>();
            //         c.SetRightDirection();
            //         contbullets++;
            //         timer -= Time.deltaTime;
            //         SetAnimation(A_Attack);
            //     }
            // }
            // if (timer > 3)
            // {
            //     //timerOn = false;
            //     if(sr.flipX == true)
            //     {
            //         var bulletPosition = transform.position + new Vector3(-1, 0, 0);
            //         var o = Instantiate(bullet1, bulletPosition, Quaternion.identity) as GameObject;
            //         var c = o.GetComponent<BulletController>();
            //         c.SetLeftDirection();
            //         contbullets++;
            //         timer -= Time.deltaTime;
            //         SetAnimation(A_Attack);
            //     }
            //     else 
            //     {
            //         var bulletPosition = transform.position + new Vector3(1, 0, 0);
            //         var o = Instantiate(bullet1, bulletPosition, Quaternion.identity) as GameObject;
            //         var c = o.GetComponent<BulletController>();
            //         c.SetRightDirection();
            //         contbullets++;
            //         timer -= Time.deltaTime;
            //         SetAnimation(A_Attack);
            //     }
            // }
            if(sr.flipX == true)
                {
                    var bulletPosition = transform.position + new Vector3(-1, 0, 0);
                    var o = Instantiate(bullet1, bulletPosition, Quaternion.identity) as GameObject;
                    var c = o.GetComponent<BulletController>();
                    c.SetLeftDirection();
                    contbullets++;
                    //timer -= Time.deltaTime;
                    SetAnimation(A_Attack);
            }
                else 
            {
                    var bulletPosition = transform.position + new Vector3(1, 0, 0);
                    var o = Instantiate(bullet1, bulletPosition, Quaternion.identity) as GameObject;
                    var c = o.GetComponent<BulletController>();
                    c.SetRightDirection();
                    contbullets++;
                    //timer -= Time.deltaTime;
                    SetAnimation(A_Attack);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        isJumping = true;
        actualJump = 0;

    }
    private void SetAnimation(int animation)
    {
        an.SetInteger("Estado", animation);
    }

    void Timer()
    {
        if(timerOn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }
}
