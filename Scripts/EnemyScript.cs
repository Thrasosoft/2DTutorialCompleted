using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject attackBox;
    Animator anim;
    GameObject player;
    public float attackDistance = 3f;
    public float seekDistance = 5f;
    public float speed = 10f;
    public bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        attackBox = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < attackDistance)
            {
                anim.SetTrigger("IsAttacking");
                Invoke("Attack", .4f);
            }
            else if (distance < seekDistance)
            {
                MoveTowardPlayer();
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }
        }
    }

    void MoveTowardPlayer()
    {
        if (!dying)
        {
            int direction = player.transform.position.x < transform.position.x ? -1 : 1;
            if (player.transform.position.x < transform.position.x)
            {
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-2.5f, 2.5f, 0f);
                }
            }
            else
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(2.5f, 2.5f, 0f);
                }
            }
            transform.Translate(transform.right * direction * speed * Time.deltaTime);
            anim.SetBool("IsRunning", true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSword"))
        {
            dying = true;
            anim.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
    }

    private void Attack()
    {
        if (!dying)
        { 
            attackBox.SetActive(true);
            Invoke("EndAttack", 0.26f);
        }
    }

    void EndAttack()
    {
        attackBox.SetActive(false);
    }
    
}
