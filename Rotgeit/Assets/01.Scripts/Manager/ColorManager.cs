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
        playerSprite.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1f);
        //backGround.color = new Color(1, 1, 1, 1f);
    }

    public void BlackColor()
    {
        playerSprite.GetComponent<SpriteRenderer>().color = new Color(1, 0.9f, 0.2f, 1f);
        //backGround.color = new Color(0.9f, 1, 0.6f, 1f);
    }

    public void CoolTimeColor()
    {
        //fillSprite.fillAmount = Mathf.Lerp();
    }
}
