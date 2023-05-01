using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Button RestartButton, QuitButton;
    public Canvas DeathCanvas, HeartCanvas;
    public Sprite Heart, DamagedHeart;

    public bool Dead = false;

    // Start is called before the first frame update
    void Start()
    {
        DeathCanvas.enabled = false;
        HeartCanvas.enabled = true;
        QuitButton.onClick.AddListener(OnQuitClicked);
        RestartButton.onClick.AddListener(onRestartClicked);
    }

    public void onRestartClicked()
    {
        Debug.Log("Clicked!");
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void ChangeHealth(int NewHealth)
    {
        if (Dead == false)
        {
            if (NewHealth == 3)
            {
                HeartCanvas.transform.Find("3").GetComponent<Image>().sprite = Heart;
                HeartCanvas.transform.Find("2").GetComponent<Image>().sprite = Heart;
                HeartCanvas.transform.Find("1").GetComponent<Image>().sprite = Heart;
            }
            if (NewHealth == 2)
            {
                HeartCanvas.transform.Find("3").GetComponent<Image>().sprite = Heart;
                HeartCanvas.transform.Find("2").GetComponent<Image>().sprite = Heart;
                HeartCanvas.transform.Find("1").GetComponent<Image>().sprite = DamagedHeart;
            }
            if (NewHealth == 1)
            {
                HeartCanvas.transform.Find("3").GetComponent<Image>().sprite = Heart;
                HeartCanvas.transform.Find("2").GetComponent<Image>().sprite = DamagedHeart;
                HeartCanvas.transform.Find("1").GetComponent<Image>().sprite = DamagedHeart;
            }
            if (NewHealth == 0)
            {
                Dead = true;
                HeartCanvas.enabled = false;
                DeathCanvas.enabled = true;
            }
        }
        
    }
}
