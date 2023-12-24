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
    private float countTime;

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
            text.enabled = true;
            myCanvas.enabled = true;
            float s = 3f;
            countTime += Time.unscaledDeltaTime;
            s -= countTime;
            text.text  = s.ToString("f2");
            if(s < 0f)
            {
              Debug.Log("asi");
              text.enabled = false;
              myCanvas.enabled = false;
              Time.timeScale = 1f;
              countDownFlag = false;
              pouseCon.MenuFlag = false;
              countTime = 0f;
            }
            
        }
    }
}
