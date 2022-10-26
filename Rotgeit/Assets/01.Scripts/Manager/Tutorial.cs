using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public Button start;
    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });
    }
    public void TruePanel(CanvasGroup canvG)
    {
        canvG.alpha = 1;
        canvG.interactable = true;
        canvG.blocksRaycasts = true;
    }

    public void Back(CanvasGroup canvG)
    {
        canvG.alpha = 0;
        canvG.interactable = false;
        canvG.blocksRaycasts = false;
    }
}
