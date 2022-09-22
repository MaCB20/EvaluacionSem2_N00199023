using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraScript : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an;
    PlayerController pc;

    public BoxCollider2D plataforma;

    [HideInInspector]
    public bool onLadder = false;
    public float vSpeed;
    public float climbSpeed;
    public float exitHop;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        an = GetComponent<Animator>();

        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Ladder"))
        {
            if(Input.GetAxisRaw("Vertical") != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);
                rb.gravityScale = 0;
                onLadder = true;

                plataforma.enabled = false;

                pc.usingLadder = onLadder;
            }
            else if(Input.GetAxisRaw("Vertical") == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            an.SetBool("onLadder", onLadder);
            an.SetFloat("vSpeed", Mathf.Abs(Input.GetAxisRaw("Vertical")));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ladder") && onLadder)
        {
            rb.gravityScale = 3;
            onLadder = false;
            pc.usingLadder = onLadder;
            plataforma.enabled = true;

            an.SetBool("onLadder", onLadder);

            if(!pc.isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, exitHop);
            }
        }
    }
}
