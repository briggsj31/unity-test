using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CreditsButtonHandler : MonoBehaviour
{

    private bool Toggle = false;
    public Image CreditsImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (Toggle == false)
            {
                Toggle = true;
            }
            else {
                Toggle = false;
            }

            CreditsImage.enabled = Toggle;

        }
    }
}
