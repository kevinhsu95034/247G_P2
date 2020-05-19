using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : PlayerController
{
    private Rigidbody2D rb;

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hSpeed = Input.GetAxis("Horizontal") * speed;
        if(!canMove) hSpeed = 0;

        rb.velocity = new Vector2(hSpeed, rb.velocity.y);

        if (isGrounded && canMove && Input.GetAxis("Vertical") > 0){
            rb.AddForce(Vector2.up * jumpForce * 100);
            isGrounded = false;
        }

        if (hSpeed > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (hSpeed < 0)
            transform.localScale = new Vector3(-1, 1, 1);

            anim.SetFloat("HSpeed", Mathf.Abs(hSpeed));
            anim.SetFloat("VSpeed", rb.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.isStatic && collision.transform.position.y<transform.position.y) {
            isGrounded = true;
        }
    }
}
