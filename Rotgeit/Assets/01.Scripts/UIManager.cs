using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private GamaManager gamaManagerScript;

    public CanvasGroup gameOverPanel;
    [Header("Button")]
    public Button pauseBtn;

    [Header("Text")]
    public Text jumpCountText;
    public Text gameOverJumpText;


    void Start()
    {
        gamaManagerScript = FindObjectOfType<GamaManager>();

        gameOverPanel.interactable = false;
    }

    void Update()
    {
        GameOver();
        JumpCount();
    }

    void GameOver()
    {
        if (gamaManagerScript.gameOver)
        {
            gameOverPanel.alpha = 1;
            gameOverPanel.interactable = true;
        }

        if(!gamaManagerScript.gameOver)
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

    void JumpCount()
    {
        jumpCountText.text = gamaManagerScript.score.ToString();
        gameOverJumpText.text = jumpCountText.text;
    }
}
