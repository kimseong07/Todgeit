using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    GamaManager gamaManager;
    GameManager gameManager;
    void Start()
    {
        gamaManager = FindObjectOfType<GamaManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gamaManager.score++;
            ActiveFalse();
        }
    }

    public void ActiveFalse()
    {
        gameManager.scoreCount--;
        this.gameObject.SetActive(false);
    }
}
