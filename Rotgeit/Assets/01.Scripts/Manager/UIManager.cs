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

    public CanvasGroup rankPanel;

    public Image option;
    public Image trophy;
    public Image outImg;

    public bool onPaues;

    bool onPanel = false;
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
            TruePanel(gamePausePanel);
            onPaues = true;
            Time.timeScale = 0f;
            GamaManager.instance.resDelay = 600f;

            TrueImageRay();

            ObjectManager.instance.ResetEnemy();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && onPaues && !onPanel)
        {
            FalsePanel(gamePausePanel);
            onPaues = false;
            Time.timeScale = 1f;

            FalseImageRay();

            GamaManager.instance.resDelay = 0f;
        }
    }

    void BtnCollection()
    {
        outBtn.onClick.AddListener(() =>
        {
            Debug.Log("������");
            Application.Quit();
        });

        optionBtn.onClick.AddListener(() =>
        {
            Debug.Log("����");
            TruePanel(audioPanel);
            onPanel = true;
        });

        trophyBtn.onClick.AddListener(() =>
        {
            Debug.Log("�Ͼ�");
            TruePanel(rankPanel);
            onPanel = true;
        });
    }

    public void TrueImageRay()
    {
        option.raycastTarget = true;
        trophy.raycastTarget = true;
        outImg.raycastTarget = true;
    }

    public void FalseImageRay()
    {
        option.raycastTarget = false;
        trophy.raycastTarget = false;
        outImg.raycastTarget = false;
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

        onPanel = false;
    }
}
