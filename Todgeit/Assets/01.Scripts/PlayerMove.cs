using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    public float jumpPower = 1f;
    [SerializeField]
    private int jumpCount = 0;

    public float forcePower = 100;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.useGravity = false;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        Move();
    }

    void Jump()
    {
        rigid.useGravity = true;

        Vector3 jumpVelo = new Vector3(0, jumpPower);

        rigid.AddForce(jumpVelo, ForceMode.Impulse);

        jumpCount++;
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(Vector3.forward * forcePower);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(Vector3.back * forcePower);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(Vector3.left * forcePower);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(Vector3.right * forcePower);
        }
    }
}
