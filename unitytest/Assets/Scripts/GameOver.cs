using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button ResetButton;
    // Start is called before the first frame update
    void Start()
    {
        ResetButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        gameObject.transform.Find("Click").gameObject.GetComponent<AudioSource>().Play(0);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
