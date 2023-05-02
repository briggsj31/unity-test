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
        gameObject.transform.Find("Click").gameObject.GetComponent<AudioSource>().Play(0);
        Debug.Log("Clicked!");
        SceneManager.LoadScene("StreetLevel", LoadSceneMode.Single);
    }

    public void OnQuitClicked()
    {
        gameObject.transform.Find("Click").gameObject.GetComponent<AudioSource>().Play(0);
        Application.Quit();
    }

    public void OnCreditsClicked()
    {
        gameObject.transform.Find("Click").gameObject.GetComponent<AudioSource>().Play(0);
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
