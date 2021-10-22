using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyCirclePrefab;
    public GameObject enemySquarePrefab;
    public GameObject enemyBarPrefab;
    public GameObject scorePrefab;

    GameObject[] enemyCircle;
    GameObject[] enemySquare;
    GameObject[] enemyBar;

    GameObject[] score;

    GameObject[] targetPool;

    private void Awake()
    {
        enemyCircle = new GameObject[40];
        enemySquare = new GameObject[10];
        enemyBar = new GameObject[10];

        score = new GameObject[1];

        Generate();
    }

    void Generate()
    {
        for(int i = 0; i < enemyCircle.Length; i++)
        {
            enemyCircle[i] = Instantiate(enemyCirclePrefab);
            enemyCircle[i].SetActive(false);
        }
        for (int i = 0; i < enemySquare.Length; i++)
        {
            enemySquare[i] = Instantiate(enemySquarePrefab);
            enemySquare[i].SetActive(false);
        }
        for (int i = 0; i < enemyBar.Length; i++)
        {
            enemyBar[i] = Instantiate(enemyBarPrefab);
            enemyBar[i].SetActive(false);
        }
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = Instantiate(scorePrefab);
            score[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "enemyCircle":
                targetPool = enemyCircle;
                break;
            case "enemySquare":
                targetPool = enemySquare;
                break;
            case "enemyBar":
                targetPool = enemyBar;
                break;
            case "score":
                targetPool = score; 
                break;
            default:
                break;  
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }
}
