using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GamaManager gamaManager;

    public static GameManager instance;

    private ObjectManager objectManager;

    public float startTime;
    public float curtime;

    public GameObject spawnPoint;

    [Header("Pattern")]
    public string[] enemyObjs;

    [Header("score")]
    public GameObject scorePrefab;
    public float createTime = 5.0f;
    public int maxScoreObj = 1;
    public int scoreCount = 0;
    private float maxScoreY = 4.8f;
    private WaitForSeconds wsSpawn;

    public List<ScoreScript> scoreList = new List<ScoreScript>();

    void Awake()
    {
       
        /*
        for (int i = 0; i < maxCircleObj + 4; i++)
        {
            GameObject e = CreateCircle();
            e.SetActive(false);
            CirclePattern eh = e.GetComponent<CirclePattern>();
            circlePatternList.Add(eh);
        } 
        ciSpawn = new WaitForSeconds(createCircleTime);
        */
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

        gamaManager = GetComponent<GamaManager>();
        objectManager = GetComponent<ObjectManager>();

        enemyObjs = new string[] { "enemyCircle", "enemySquare", "enemyBar"}; 
    }

    public void ResetScore()
    {
        scoreList.ForEach(x => x.gameObject.SetActive(false));
        scoreCount = 0;
    }


    public void StartCor()
    {
        StartCoroutine(SpawnScore());
        //StartCoroutine(SpawnCircle());
    }

    public GameObject CreateScore()
    {
        return Instantiate(scorePrefab,
           transform.position,
           Quaternion.identity,
           transform
           );
    }
    /*
    public void ResetCircle()
    {
        circlePatternList.ForEach(x => x.gameObject.SetActive(false));
        circleCount = 0;
    }
    public GameObject CreateCircle()
    {
        return Instantiate(circlePettern,
           transform.position,
           Quaternion.identity,
           transform
           );
    }
    */
    private void Start()
    {
        //StartCoroutine(SpawnCircle());
        
    }

    private void FixedUpdate()
    {
        if (gamaManager.gameStart)
        {
            StartCoroutine(SpawnScore());
            if (startTime >= 0)
            {
                startTime = startTime - Time.deltaTime;
            }
        }

    }

    private void Update()
    {
        if (startTime <= 0)
        {
            if(gamaManager.score > 4)
            {
                SpawnSquareEnemy();
            }

            SpawnCircleEnemy();

            startTime = curtime;
        }
    }

    void SpawnCircleEnemy()
    {
        float randy = UnityEngine.Random.Range(-maxScoreY, maxScoreY);

        GameObject enemy = objectManager.MakeObj(enemyObjs[0]);
        CirclePattern eh = enemy.GetComponent<CirclePattern>();
        eh.SetPos(new Vector2(spawnPoint.transform.position.x, randy), 180);

    }

    void SpawnSquareEnemy()
    {
        float randy = UnityEngine.Random.Range(-maxScoreY, maxScoreY);

        GameObject enemy = objectManager.MakeObj(enemyObjs[1]);
        CirclePattern eh = enemy.GetComponent<CirclePattern>();
        eh.SetPos(new Vector2(spawnPoint.transform.position.x, randy), 180);
    }

    IEnumerator SpawnScore()
    {
        while (!gamaManager.gameOver)
        {
            if (scoreCount < maxScoreObj)
            {

                ScoreScript eh = scoreList.Find(x => !x.gameObject.activeSelf);
                // activieSelf, activeInHeirachy
                if (eh == null)
                {
                    GameObject e = CreateScore();
                    eh = e.GetComponent<ScoreScript>();
                    scoreList.Add(eh);
                }

                scoreCount++;

                float spawnX = spawnPoint.transform.position.x;
                float randx = UnityEngine.Random.Range(-spawnX, spawnX);
                float randy = UnityEngine.Random.Range(-maxScoreY, maxScoreY);

                eh.transform.position = new Vector2(randx, randy);
                eh.gameObject.SetActive(true);
            }
            yield return wsSpawn;

        }
    }
}



//    IEnumerator SpawnCircle()
//    {
//        while (!gamaManager.gameOver)
//        {
//            if (circleCount < maxCircleObj)
//            {
//                CirclePattern eh = circlePatternList.Find(x => !x.gameObject.activeSelf);
//                // activieSelf, activeInHeirachy
//                if (eh == null)
//                {
//                    GameObject e = CreateCircle();
//                    eh = e.GetComponent<CirclePattern>();
//                    circlePatternList.Add(eh);
//                }

//                circleCount++;

//                float randy = UnityEngine.Random.Range(-maxScoreY, maxScoreY);

//                //Vector3 dir = player.transform.position - eh.transform.position;
//                eh.SetPos(new Vector2(spawnPoint.transform.position.x, randy), 180);

//                eh.gameObject.SetActive(true);
//            }

//            yield return ciSpawn;
//        }
//    }
//}
