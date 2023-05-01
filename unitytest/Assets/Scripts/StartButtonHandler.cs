using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButtonHandler : MonoBehaviour
{

    public Button StartButton, QuitButton, CreditsButton;

    private bool Toggle = false;

    public Image CreditsImage;

    // Start is called before the first frame update
    void Start()
    {
        CreditsImage.enabled = false;
        StartButton.onClick.AddListener(OnStartClicked);
        QuitButton.onClick.AddListener(OnQuitClicked);
        CreditsButton.onClick.AddListener(OnCreditsClicked);
    }


    public void OnStartClicked()
    {
        Debug.Log("Clicked!");
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnCreditsClicked()
    {
        if (Toggle == false)
        {
            Toggle = true;
        }
        else
        {
            Toggle = false;
        }

        CreditsImage.enabled = Toggle;
    }

}
