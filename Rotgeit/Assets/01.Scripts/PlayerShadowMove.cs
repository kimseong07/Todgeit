using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowMove : MonoBehaviour
{
    public Transform player;

    float dist;
    public float speed = 1f;
    float time;
    float curtime;
    Vector2 startPos;
    Vector2 targetPos;

    void Start()
    {
        
    }

    void Update()
    {
        if (curtime < time)
        {
            curtime += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, targetPos, curtime / time);
        }

        SetTargetPos(player.position);
    }

    public void SetTargetPos(Vector2 targetPos)
    {
        dist = Vector2.Distance(transform.position, targetPos);
        time = dist / speed;
        curtime = 0f;
        startPos = transform.position;
        this.targetPos = targetPos;
    }
}
