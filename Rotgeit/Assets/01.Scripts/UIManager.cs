using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerMove playerScript;

    public CanvasGroup gameOverPanel;

    public Button pauseBtn;


    void Start()
    {
        playerScript = FindObjectOfType<PlayerMove>();

        gameOverPanel.interactable = false;
    }

    void Update()
    {
        GameOver();
    }

    void GameOver()
    {
        if (playerScript.gameOver)
        {
            gameOverPanel.alpha = 1;
            gameOverPanel.interactable = true;
        }

        if(!playerScript.gameOver)
        {
            gameOverPanel.alpha = 0;
            gameOverPanel.interactable = false;
        }
    }

    void Pause()
    {
        pauseBtn.onClick.AddListener(() =>
        {

        });
    }
}
