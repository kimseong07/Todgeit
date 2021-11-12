using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public GameObject enemyCirclePrefab;
    public GameObject enemySquarePrefab;
    public GameObject enemyBarPrefab;
    public GameObject enemyRealBarPrefab;

    GameObject[] enemyCircle;
    GameObject[] enemySquare;
    GameObject[] enemyBar;
    GameObject[] enemyRealBar;

    public GameObject[] targetPool;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 게임매니저가 실행중입니다.");
        }
        instance = this;

        enemyCircle = new GameObject[40];
        enemySquare = new GameObject[30];
        enemyBar = new GameObject[5];
        enemyRealBar = new GameObject[5];

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
        for (int i = 0; i < enemyRealBar.Length; i++)
        {
            enemyRealBar[i] = Instantiate(enemyRealBarPrefab);
            enemyRealBar[i].SetActive(false);
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
            case "enemyRealBar":
                targetPool = enemyRealBar;
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

    public void ResetEnemy()
    {
        for (int i = 0; i < targetPool.Length; i++)
        {
            if(targetPool[i].activeSelf)
            {
                targetPool[i].gameObject.SetActive(false);
            }
        }
    }
}
