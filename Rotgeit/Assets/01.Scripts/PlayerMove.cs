using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private GamaManager gamaManager;

    Rigidbody2D rigid;
    public float jumpPower = 1f;

    public int jumpCount = 0;

    public float forcePower = 10f;

    public float maxPower = 5f;

    public float slowPower = 1.2f;


    
    void Start()
    {
        gamaManager = FindObjectOfType<GamaManager>();


        rigid = GetComponent<Rigidbody2D>();

        rigid.gravityScale = 0;

    }
    void Update()
    {
        if(gamaManager.gameStart)
        {
            rigid.gravityScale = 1;
        }



        if (!gamaManager.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
            Move();
        }
    }

    void Jump()
    {
        gamaManager.gameStart = true;

        Vector3 jumpVelo = new Vector3( 0 , jumpPower);

        rigid.AddForce(jumpVelo, ForceMode2D.Impulse);

        jumpCount++;
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gamaManager.gameStart = true;
            rigid.AddForce(Vector2.left * forcePower);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gamaManager.gameStart = true;
            rigid.AddForce(Vector2.right * forcePower);
        }

        MaxPower();
    }

    void MaxPower()
    {
        //A입력 <-
        if (rigid.velocity.x < -maxPower)
        {
            rigid.velocity = new Vector2(-maxPower, rigid.velocity.y);
        }
        //D입력 ->
        else if (rigid.velocity.x > maxPower)
        {
            rigid.velocity = new Vector2(maxPower, rigid.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            gamaManager.gameOver = true;
            gamaManager.gameStart = false;

            rigid.velocity = Vector2.zero;
            rigid.gravityScale = 0;
        }
    }
}