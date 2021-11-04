using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarPattern : MonoBehaviour
{
    private PlayerShadowMove playerShadow;

    float dist;
    public float speed = 1f;
    float time;
    float curtime;
    Vector2 startPos;
    Vector2 targetPos;

    public float lifeTime = 3.0f;
    WaitForSeconds ws;

    private ObjectManager objectManager;
    private GameManager gameManager;

    public float realBarCount;
    void Start()
    {
        playerShadow = FindObjectOfType<PlayerShadowMove>();

        objectManager = FindObjectOfType<ObjectManager>();

        gameManager = FindObjectOfType<GameManager>();

        ws = new WaitForSeconds(lifeTime);

    }

    void Update()
    {
        if (curtime < time)
        {
            curtime += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, targetPos, curtime / time);
        }

        SetTargetPos(playerShadow.transform.position);
        this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, 0);

        StartCoroutine(SetActiveFalse());

        if(GamaManager.instance.gameOver)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SetTargetPos(Vector2 targetPos)
    {
        dist = Vector2.Distance(transform.position, targetPos);
        time = dist / speed;
        curtime = 0f;
        startPos = transform.position;
        this.targetPos = targetPos;
    }

    public void SetPos(Vector3 pos, float angle = 0f)
    {
        transform.position = pos;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void SpawnBarEnemy()
    {
        GameObject enemy = objectManager.MakeObj(gameManager.enemyObjs[3]);
        RealBarPattern bh = enemy.GetComponent<RealBarPattern>();
        bh.SetPos(new Vector2(this.transform.position.x, 0), 0);
    }

    IEnumerator SetActiveFalse()
    {
        yield return ws;
        SpawnBarEnemy();
        this.gameObject.SetActive(false);
    }
}
