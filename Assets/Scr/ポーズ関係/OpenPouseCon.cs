using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPouseCon : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Escape"))
        {
            Time.timeScale = 0f;
            canvas.enabled = true;
        }
    }
}
