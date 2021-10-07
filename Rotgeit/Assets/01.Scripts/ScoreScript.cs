using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    GamaManager gamaManager;
    void Start()
    {
        gamaManager = FindObjectOfType<GamaManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Score")
        {
            gamaManager.score++;
        }
    }
}
