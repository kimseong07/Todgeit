using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBarPattern : MonoBehaviour
{
    public float lifeTime = 3.0f;
    WaitForSeconds ws;

    BarPattern barpattern;

    private void Start()
    {
        barpattern = FindObjectOfType<BarPattern>();
        ws = new WaitForSeconds(lifeTime);
    }

    private void Update()
    {
        StartCoroutine(SetActiveFalse());
    }

    public void SetPos(Vector3 pos, float angle = 0f)
    {
        transform.position = pos;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator SetActiveFalse()
    {
        yield return ws;
        this.gameObject.SetActive(false);

    }
}
