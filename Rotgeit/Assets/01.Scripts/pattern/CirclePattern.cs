using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePattern : MonoBehaviour
{
    GameManager gameManager;
    public float speed = 0f;

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

        StartCoroutine(EndLife());

        transform.Translate(Vector2.left * (speed / 100));
    }

   IEnumerator EndLife()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.ResetCircle();
    }
}
