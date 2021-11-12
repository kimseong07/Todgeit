using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GamaManager gamaManager;

    public static GameManager instance;

    private ObjectManager objectManager;

    public float circleTime;
    public float squareTime;
    public float circleCurTime;
    public float squareCurTime;

    public bool isRight = false;

    private float randPatTime = 2f;

    public GameObject spawnPoint;
    public GameObject spawnPointLeft;
    public GameObject player;

    [Header("Pattern")]
    public string[] enemyObjs;
    public float barCount;

    [Header("score")]
    public GameObject scorePrefab;
    public float createTime = 5.0f;
    public int maxScoreObj = 1;
    public int scoreCount = 0;
    private float maxScoreY = 4.8f;
    private WaitForSeconds wsSpawn;

    public List<ScoreScript> scoreList = new List<ScoreScript>();

    private GameObject square;

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

        enemyObjs = new string[] { "enemyCircle", "enemySquare", "enemyBar", "enemyRealBar"};

        isRight = true;
    }

    public void ResetScore()
    {
        scoreList.ForEach(x => x.gameObject.SetActive(false));
        scoreCount = 0;
        barCount = 0;
        randPatTime = 2f;

        ObjectManager.instance.ResetEnemy();
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

            Timer();
        }
    }

    private void Update()
    {
        if (isRight)
        {
            PatternRight();
        }
        else
        {
            PatternLeft();
        }

        if(gamaManager.score >= 3 && randPatTime <= 0)
        {
            StartPattern();
        }

        if(gamaManager.score % 5 == 0 && barCount < 1 && gamaManager.score != 0)
        {
            SpawnBarEnemy();
        }
        if(gamaManager.score % 6 == 0 && gamaManager.score != 0)
        {
            barCount = 0;
        }

        if(gamaManager.score % 2 == 1)
        {
            isRight = false;
        }
        else
        {
            isRight = true;
        }
    }

    void PatternRight()
    {
        if (circleTime <= 0)
        {
            SpawnCircleEnemy();

            circleTime = circleCurTime;
        }

        if (gamaManager.score >= 3 && squareTime <= 0)
        {
            SpawnSquareEnemy();

            squareTime = squareCurTime;
        }
    }
    void PatternLeft()
    {
        if (circleTime <= 0)
        {
            SpawnCircleEnemyL();

            circleTime = circleCurTime;
        }

        if (gamaManager.score >= 3 && squareTime <= 0)
        {
            SpawnSquareEnemyL();

            squareTime = squareCurTime;
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

        square = objectManager.MakeObj(enemyObjs[1]);
        CirclePattern eh = square.GetComponent<CirclePattern>();
        eh.SetPos(new Vector2(spawnPoint.transform.position.x, randy), 180);
    }

    void SpawnCircleEnemyL()
    {
        float randy = UnityEngine.Random.Range(-maxScoreY, maxScoreY);

        GameObject enemy = objectManager.MakeObj(enemyObjs[0]);
        CirclePattern eh = enemy.GetComponent<CirclePattern>();
        eh.SetPos(new Vector2(spawnPointLeft.transform.position.x, randy), 0);
    }

    void SpawnSquareEnemyL()
    {
        float randy = UnityEngine.Random.Range(-maxScoreY, maxScoreY);

        square = objectManager.MakeObj(enemyObjs[1]);
        CirclePattern eh = square.GetComponent<CirclePattern>();
        eh.SetPos(new Vector2(spawnPointLeft.transform.position.x, randy), 0);
    }

    void SpawnBarEnemy()
    {
        GameObject enemy = objectManager.MakeObj(enemyObjs[2]);
        BarPattern bh = enemy.GetComponent<BarPattern>();
        bh.SetPos(new Vector2(player.transform.position.x, 0), 0);
        barCount++;
    }



    void StartPattern()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject ptobj = objectManager.MakeObj(enemyObjs[0]);
            CirclePattern pt = ptobj.GetComponent<CirclePattern>();
            switch (i)
            {
                case 0:
                    pt.SetPos(new Vector2(square.transform.position.x, square.transform.position.y), 60);
                    break;
                case 1:
                    pt.SetPos(new Vector2(square.transform.position.x, square.transform.position.y), 120);
                    break;
                case 2:
                    pt.SetPos(new Vector2(square.transform.position.x, square.transform.position.y), 180);
                    break;
                case 3:
                    pt.SetPos(new Vector2(square.transform.position.x, square.transform.position.y), 240);
                    break;
                case 4:
                    pt.SetPos(new Vector2(square.transform.position.x, square.transform.position.y), 300);
                    break;
                case 5:
                    pt.SetPos(new Vector2(square.transform.position.x, square.transform.position.y), 360);
                    break;
            }

            square.SetActive(false);
            randPatTime = UnityEngine.Random.Range(1.5f, 3.0f);
        }
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

    public void Timer()
    {
        if (circleTime >= 0)
        {
            circleTime = circleTime - Time.deltaTime;
        }

        if (squareTime >= 0)
        {
            squareTime = squareTime - Time.deltaTime;
        }

        if (randPatTime >= 0 && gamaManager.score >= 1)
        {
            randPatTime = randPatTime - Time.deltaTime;
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
