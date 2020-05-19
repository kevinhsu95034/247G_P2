using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public bool canMove;

    private Rigidbody rb;
    protected Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            anim.SetFloat("HSpeed", 0);
            return;
        }
        float hSpeed = Input.GetAxis("Horizontal") * speed;
        float vSpeed = Input.GetAxis("Vertical") * speed;

        rb.velocity = new Vector3(hSpeed, 0, vSpeed);

        if (hSpeed > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (hSpeed < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        anim.SetFloat("HSpeed", Mathf.Abs(hSpeed)+ Mathf.Abs(vSpeed));
    }

    public void SetCanMove(bool b) { canMove = b; }
}
