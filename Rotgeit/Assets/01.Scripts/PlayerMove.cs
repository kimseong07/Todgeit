using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float jumpPower = 1f;
    [SerializeField]
    private int jumpCount = 0;

    public float forcePower = 10f;

    public float maxPower = 5f;

    public float slowPower = 1.2f;

    bool gameStart = false;
    public bool gameOver = false;
    
    void Start()
    {
        gameOver = false;

        rigid = GetComponent<Rigidbody2D>();

        rigid.gravityScale = 0;

    }
    void Update()
    {
        if(gameStart)
        {
            rigid.gravityScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        Move();
    }

    void Jump()
    {
        gameStart = true;

        Vector3 jumpVelo = new Vector3( 0 , jumpPower);

        rigid.AddForce(jumpVelo, ForceMode2D.Impulse);

        jumpCount++;
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameStart = true;
            rigid.AddForce(Vector2.left * forcePower);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gameStart = true;
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
            gameOver = true;
            gameStart = false;
        }
    }
}