using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private float offset = 14.3f;
    private int duplicates = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Background" && duplicates < 20)
        {
            duplicates += 1;
            GameObject Clone = Instantiate(gameObject, gameObject.transform.localPosition + new Vector3(-10 + (offset * duplicates), gameObject.transform.localPosition.y, 0), gameObject.transform.rotation);
            Clone.name = "Background_Clone";
        }
        
    }
}
