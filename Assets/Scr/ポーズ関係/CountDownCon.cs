using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownCon : MonoBehaviour
{
    private Canvas myCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        myCanvas = this.GetComponent<Canvas>();
        myCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(myCanvas.enabled == true)
        {
            float time = 4;
            time -= Time.deltaTime;
        }
    }
}
