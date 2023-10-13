using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownCon : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Canvas myCanvas;
    [SerializeField]
    private List<string> texts = new List<string>();
    private float time = 6.0f;

    private PouseCon pouseCon;

    private bool countDownFlag = false;

    public bool CountDownFlag
    {
        get { return this.countDownFlag; }
        set { this.countDownFlag = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).gameObject.GetComponent<Text>();
        myCanvas = this.GetComponent<Canvas>();
        pouseCon = FindObjectOfType<PouseCon>();
        myCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(countDownFlag == true)
        {
            myCanvas.enabled = true;
            while (true)
            {
                text.text = string.Format("Time:{0:00.00}", time);
                time -= Time.realtimeSinceStartup;
                Debug.Log("time");
                if (time <= 0.0f)
                {
                    Time.timeScale = 1f;
                    time = 0f;
                    myCanvas.enabled = false;
                    break;
                }
            }
        }
    }
}
