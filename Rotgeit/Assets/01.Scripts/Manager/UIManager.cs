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
    public Button optionBtn;
    public Button trophyBtn;
    public Button outBtn;

    [Header("Text")]
    public Text jumpCountText;
    public Text gameOverJumpText;

    public CanvasGroup gamePausePanel;

    public CanvasGroup audioPanel;

    public bool onPaues;

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
        Pause();
        BtnCollection();
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

    public void JumpCount()
    {
        jumpCountText.text = gamaManagerScript.score.ToString();
        gameOverJumpText.text = jumpCountText.text;
    }

    void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !onPaues)
        {
            TruePanel(gamePausePanel);
            onPaues = true;
            Time.timeScale = 0f;
            GamaManager.instance.resDelay = 600f;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && onPaues)
        {
            FalsePanel(gamePausePanel);
            onPaues = false;

            Time.timeScale = 1f;
            GamaManager.instance.resDelay = 0f;
        }
    }

    void BtnCollection()
    {
        outBtn.onClick.AddListener(() =>
        {
            Debug.Log("나가기");
            Application.Quit();
        });

        optionBtn.onClick.AddListener(() =>
        {
            Debug.Log("설정");
            TruePanel(audioPanel);
        });

        trophyBtn.onClick.AddListener(() =>
        {
            Debug.Log("니얼굴");
        });
    }

    public void TruePanel(CanvasGroup canvG)
    {
        canvG.alpha = 1;
        canvG.interactable = true;
        canvG.blocksRaycasts = true;
    }

    public void FalsePanel(CanvasGroup canvG)
    {
        canvG.alpha = 0;
        canvG.interactable = false;
        canvG.blocksRaycasts = false;
    }
}
