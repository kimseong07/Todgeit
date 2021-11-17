using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamaManager : MonoBehaviour
{
    public static GamaManager instance;

    private PlayerMove playerScript;

    public GameObject backGround;

    public Button restartBtn;

    private bool restartGame;

    public float score;

    public bool gameStart = false;
    public bool gameOver = false;

    public float curResDelay;
    public float resDelay;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 가마매니저가 실행중입니다.");
        }
        instance = this;
    }

    void Start()
    {
        playerScript = FindObjectOfType<PlayerMove>();

        gameOver = false;

        restartGame = false;

        backGround.transform.localScale = new Vector3((Camera.main.orthographicSize * 2) / Screen.height * Screen.width, Camera.main.orthographicSize * 2);
    }

    private void FixedUpdate()
    {
        if(resDelay >= 0)
        {
            resDelay = resDelay - Time.deltaTime;
        }
    }
    void Update()
    {
        if (resDelay <= 0)
        {
            if (gameOver)
            {
                restartBtn.onClick.AddListener(() =>
                    {
                        restartGame = true;
                        resDelay = curResDelay;
                    });

                if (Input.GetKeyDown(KeyCode.R))
                {
                    restartGame = true;
                    resDelay = curResDelay;
                }
            }

            if (!gameOver)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    gameStart = false;
                    gameOver = true;
                    restartGame = true;
                    resDelay = curResDelay;
                }
            }
        }

        RestartPlayerState();

        ResetScorePos();
    }

    void RestartPlayerState()
    {
        if(restartGame)
        {
            gameOver = false;

            score = 0;
            playerScript.transform.position = new Vector2(0, 0);
            playerScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerScript.GetComponent<Rigidbody2D>().gravityScale = 0;
            playerScript.particleCount = 0;
            playerScript.gameObject.SetActive(true);

            ColorManager.instance.fillSprite.fillAmount = 1f;

            GameManager.instance.ResetScore();

            AudioManager.instance.mainAudio.Play();
            restartGame = false;
        }
    }

    void ResetScorePos()
    {
        if(Input.GetKeyDown(KeyCode.Q) && gameStart)
        {
            if (score > 0)
            {
                score--;
                GameManager.instance.ResetScore();
            }
        }
    }
}
