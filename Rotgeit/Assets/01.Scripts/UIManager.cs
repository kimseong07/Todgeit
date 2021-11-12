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

    public bool onPaues;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("�ټ��� UI�Ŵ����� �������Դϴ�.");
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
            gamePausePanel.alpha = 1;
            gamePausePanel.interactable = true;
            onPaues = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && onPaues)
        {
            gamePausePanel.alpha = 0;
            gamePausePanel.interactable = false;
            onPaues = false;
        }
    }

    void BtnCollection()
    {
        outBtn.onClick.AddListener(() =>
        {
            Debug.Log("������");
        });

        optionBtn.onClick.AddListener(() =>
        {
            Debug.Log("����");
        });

        trophyBtn.onClick.AddListener(() =>
        {
            Debug.Log("�Ͼ�");
            Application.Quit();
        });
    }
}
