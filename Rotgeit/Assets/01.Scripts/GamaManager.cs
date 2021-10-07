using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamaManager : MonoBehaviour
{
    private PlayerMove playerScript;

    public Button restartBtn;


    private bool restartGame;

    void Start()
    {
        playerScript = FindObjectOfType<PlayerMove>();

        restartGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        restartBtn.onClick.AddListener(() =>
        {
            restartGame = true;
        });

        if(Input.GetKeyUp(KeyCode.R) && playerScript.gameOver)
        {
            restartGame = true;
        }

        RestartPlayerState();
    }

    void RestartPlayerState()
    {
        if(restartGame)
        {
            playerScript.gameOver = false;
            playerScript.transform.position = new Vector2(0, 0);
            playerScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerScript.GetComponent<Rigidbody2D>().gravityScale = 0;

            restartGame = false;
        }
    }
}
