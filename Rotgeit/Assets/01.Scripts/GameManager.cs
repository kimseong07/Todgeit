using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GamaManager gamaManager;

    public static GameManager instance;

    public GameObject scorePrefab;
    public float createTime = 5.0f;
    public int maxScoreObj = 5;
    private int scoreCount = 0;

    private List<ScoreScript> scoreList = new List<ScoreScript>();
    private WaitForSeconds wsSpawn;

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

        gamaManager = GetComponent<GamaManager>();
    }

    public GameObject CreateScore()
    {
        return Instantiate(scorePrefab,
           transform.position,
           Quaternion.identity,
           transform
           );
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (!gamaManager.gameOver)
        {
            if (scoreCount < maxScoreObj)
            {
               
                ScoreScript eh = scoreList.Find(
                                    x => !x.gameObject.activeSelf);
                // activieSelf, activeInHeirachy
                if (eh == null)
                {
                    GameObject e = CreateScore();
                    eh = e.GetComponent<ScoreScript>();
                    scoreList.Add(eh);
                }
                scoreCount++;
                eh.transform.position = new Vector2(0,0);
                eh.gameObject.SetActive(true);

                Action handler = null;
                handler = () =>
                {
                    scoreCount--;
                };

            }
            yield return wsSpawn;
        }
    }
}
