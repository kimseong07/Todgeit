using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    PlayerMove playerSprite;
    public SpriteRenderer backGround;
    public Image fillSprite;

    public float coolTime;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("다수의 칼라매니저가 실행중입니다");
        }
        instance = this;

        playerSprite = FindObjectOfType<PlayerMove>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WhiteColor()
    {
        playerSprite.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
        //backGround.color = new Color(1, 1, 1, 1f);
    }

    public void BlackColor()
    {
        fillSprite.gameObject.SetActive(false);
        playerSprite.GetComponent<SpriteRenderer>().color = new Color(1, 0.9f, 0.2f, 1f);
        //backGround.color = new Color(0.9f, 1, 0.6f, 1f);
    }

    public IEnumerator CoolTime(float cool)
    {
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            fillSprite.gameObject.SetActive(true);
            fillSprite.fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }
    }
}
