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

    public float mujeogTime = 0f;

    private float mujeongCTime = 8f;
    private float muejongMTime = 8f;

    public bool mujeog = false;

    public ParticleSystem explosion;
    public float particleCount;


    void Start()
    {
        gamaManager = FindObjectOfType<GamaManager>();

        rigid = GetComponent<Rigidbody2D>();



        explosion = FindObjectOfType<ParticleSystem>();

        rigid.gravityScale = 0;

        mujeongCTime = 0f;

        mujeog = false;

    }
    void Update()
    {
        if (gamaManager.gameStart)
        {
            rigid.gravityScale = 1;

            if (mujeongCTime <= 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && !mujeog)
                {
                    Debug.Log("무적");
                    mujeog = true;
                    ColorManager.instance.BlackColor();
                    StartCoroutine(Invincibility());
                }
            }
        }

        if (!gamaManager.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump();

                //GameManager.instance.StartCor();
            }
            Move();
        }


        if (mujeongCTime >= 0)
        {
            mujeongCTime = mujeongCTime - Time.deltaTime;
        }

        if(gamaManager.gameOver && particleCount < 1)
        {
            explosion.Play();
            this.gameObject.SetActive(false);

            particleCount++;
        }

        explosion.gameObject.transform.position = this.gameObject.transform.position;
    }

    void Jump()
    {
        gamaManager.gameStart = true;

        Vector3 jumpVelo = new Vector3(0, jumpPower);
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
        if (collision.gameObject.tag == "Obstacle")
        {
            ResetGame();
        }

        if (!mujeog)
        {
            if (collision.gameObject.tag == "ObstaclePatterb")
            {
                ResetGame();
            }
        }

    }

    void ResetGame()
    {
        gamaManager.gameOver = true;
        gamaManager.gameStart = false;

        mujeongCTime = 0f;
        
        //GameManager.instance.ResetCircle();

        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0;
    }

    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(mujeogTime);
        mujeog = false;
        ColorManager.instance.WhiteColor();
        mujeongCTime = muejongMTime;
    }
}