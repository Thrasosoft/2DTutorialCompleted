using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpHieght = 10f;
    public Collider2D groundCollider;
    private Boolean isGrounded = false;
    private Rigidbody2D rigid;
    private Collider2D collider;
    private Animator anim;
    private GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = transform.GetChild(0).GetComponent<Collider2D>();
        sword = transform.GetChild(1).gameObject;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        bool isMoving = xAxis != 0;
        anim.SetBool("isMoving", isMoving);
        if (xAxis > 0)
        {
            transform.localScale = new Vector3(3, 3);
        }
        if (xAxis < 0)
        {
            transform.localScale = new Vector3(-3, 3);
        }
        transform.Translate(transform.right * xAxis * Time.deltaTime * moveSpeed);

        //Determine if CRTL is being pressed
        bool attackButtonPressed = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);
        if (attackButtonPressed)
        {    //Trigger Attack() if CRTL is pressed
            Attack();
        }
    }

    private void FixedUpdate()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        if (yAxis > 0 || Input.GetKeyDown("space"))
        {
            Jump(xAxis);
        }
        if (collider.IsTouching(groundCollider))
        {
            isGrounded = true;
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isJumping", !isGrounded);
        }

    }

    void Jump(float xAxis)
    {
        if (isGrounded)
        {
            anim.SetTrigger("isJumping");
            Vector2 force = new Vector2(xAxis, jumpHieght);
            rigid.AddForce(force);
            isGrounded = false;
            anim.SetBool("isGrounded", isGrounded);
        }
    }
    private void Attack()
    {
        Invoke("EnableSword", .25f);
        anim.SetTrigger("isAttacking");
    }

    void EnableSword()
    {
        sword.SetActive(true);
        Invoke("DisableSword", .25f);
    }

    void DisableSword()
    {
        sword.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            Invoke("Death",.25f);
        }
    }

    private void Death() {
        anim.SetBool("Death", true);
        Destroy(gameObject, .5f);
    }
}
