using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePattern : MonoBehaviour
{
    public float speed = 0f;

    private float explosionTime = 0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        //Vector3 circleVecPlus = new Vector3(0, transform.position.y);
        //if (transform.position != circleVecPlus)
        //{
        //    transform.Translate(Vector2.left * speed);
        //}
        //else
        //{
        //    transform.Translate(Vector2.right * speed);
        //}

        transform.Translate(Vector2.right * (speed / 100));
    }

    public void SetPos(Vector3 pos, float angle = 0f)
    {
        transform.position = pos;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            this.gameObject.SetActive(false);
            //GameManager.instance.circleCount--;
        }
    }
}
