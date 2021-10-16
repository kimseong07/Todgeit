using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GamaManager gamaManager;

    public static GameManager instance;

    public float startTime;
    public float curtime;

    public GameObject spawnPoint;

    [Header ("score")]
    public GameObject scorePrefab;
    public float createTime = 5.0f;
    public int maxScoreObj = 1;
    public int scoreCount = 0;
    private float maxScoreX = 8.8f;
    private float maxScoreY = 4.8f;

    public List<ScoreScript> scoreList = new List<ScoreScript>();

    [Header("circlePattern")]
    public GameObject circlePettern;
    public float createCircleTime = 5.0f;
    public int maxCircleObj = 5;
    public int circleCount = 0;

    public List<CirclePattern> circlePatternList = new List<CirclePattern>();

    private WaitForSeconds wsSpawn;
    private WaitForSeconds ciSpawn;

    public Transform player;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 게임매니저가 실행중입니다.");
        }
        instance = this;

        for (int i = 0; i < maxScoreObj + 1; i++)
        {
            GameObject e = CreateScore();
            e.SetActive(false);
            ScoreScript eh = e.GetComponent<ScoreScript>();
            scoreList.Add(eh);
        }
        wsSpawn = new WaitForSeconds(createTime);

        for (int i = 0; i < maxCircleObj + 1; i++)
        {
            GameObject e = CreateCircle();
            e.SetActive(false);
            CirclePattern eh = e.GetComponent<CirclePattern>();
            circlePatternList.Add(eh);
        } 
        ciSpawn = new WaitForSeconds(createCircleTime);

        gamaManager = GetComponent<GamaManager>();
    }

    public void ResetScore()
    {
        scoreList.ForEach(x => x.gameObject.SetActive(false));
        scoreCount = 0;
    }
    public void ResetCircle()
    {
        circlePatternList.ForEach(x => x.gameObject.SetActive(false));
        circleCount = 0;
    }

    public void StartCor()
    {
        StartCoroutine(SpawnScore());
        StartCoroutine(SpawnCircle());
    }

    public GameObject CreateScore()
    {
        return Instantiate(scorePrefab,
           transform.position,
           Quaternion.identity,
           transform
           );
    }

    public GameObject CreateCircle()
    {
        return Instantiate(circlePettern,
           transform.position,
           Quaternion.identity,
           transform
           );
    }
    private void Start()
    {
        //StartCoroutine(SpawnCircle());
        //StartCoroutine(SpawnScore());
    }

    private void FixedUpdate()
    {
        if (gamaManager.gameStart)
        {
            startTime = startTime - Time.deltaTime;
        }
        
    }

    private void Update()
    {
        if (startTime <= 0)
        {
            StartCor();
            startTime = curtime;
        }
    }

    IEnumerator SpawnScore()
    {
        while (!gamaManager.gameOver)
        {
            if (scoreCount < maxScoreObj)
            {

                ScoreScript eh = scoreList.Find( x => !x.gameObject.activeSelf);
                // activieSelf, activeInHeirachy
                if (eh == null)
                {
                    GameObject e = CreateScore();
                    eh = e.GetComponent<ScoreScript>();
                    scoreList.Add(eh);
                }

                scoreCount++;

                float randx = UnityEngine.Random.Range(-maxScoreX, maxScoreX);
                float randy = UnityEngine.Random.Range(-maxScoreY, maxScoreY);

                eh.transform.position = new Vector2(randx, randy);
                eh.gameObject.SetActive(true);
            }
            yield return wsSpawn;

        }
    }

    IEnumerator SpawnCircle()
    {
        while (!gamaManager.gameOver)
        {
            if (circleCount < maxCircleObj)
            {
                CirclePattern eh = circlePatternList.Find(x => !x.gameObject.activeSelf);
                // activieSelf, activeInHeirachy
                if (eh == null)
                {
                    GameObject e = CreateCircle();
                    eh = e.GetComponent<CirclePattern>();
                    circlePatternList.Add(eh);
                }

                circleCount++;

                float randy = UnityEngine.Random.Range(-maxScoreY, maxScoreY);

                //Vector3 dir = player.transform.position - eh.transform.position;
                eh.SetPos(new Vector2(spawnPoint.transform.position.x, randy), 180);

                eh.gameObject.SetActive(true);
            }

            yield return ciSpawn;
        }
    }
}
