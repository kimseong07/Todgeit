using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private GamaManager gamaManagerScript;

    public CanvasGroup gameOverPanel;
    [Header("Button")]
    public Button pauseBtn;

    [Header("Text")]
    public Text jumpCountText;
    public Text gameOverJumpText;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 UI매니저가 실행중입니다.");
        }
        instance = this;
    }
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

    public void GameOver()
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

    public void JumpCount()
    {
        jumpCountText.text = gamaManagerScript.score.ToString();
        gameOverJumpText.text = jumpCountText.text;
    }
}
