using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyCirclePrefab;
    public GameObject enemySquarePrefab;
    public GameObject enemyBarPrefab;

    GameObject[] enemyCircle;
    GameObject[] enemySquare;
    GameObject[] enemyBar;

    GameObject[] targetPool;

    private void Awake()
    {
        enemyCircle = new GameObject[40];
        enemySquare = new GameObject[30];
        enemyBar = new GameObject[5];

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
