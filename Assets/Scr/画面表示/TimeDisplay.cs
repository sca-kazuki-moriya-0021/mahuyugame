using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField]
    private Text timerText;
    
    private TotalGM gm;
    private TotalGM.StageCon stage;

    private bool bossDidFlag = false;

    private float time;

    private int minute;

    public bool BossDidFlag
    {
        get { return this.bossDidFlag; }
        set { this.bossDidFlag = value; }
    }

    private void Awake()
    {
        gm = FindObjectOfType<TotalGM>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        stage = gm.MyGetScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossDidFlag == false)
        {
            time += Time.deltaTime;
            if (time > 60)
            {
                time = 0;
                minute++;
            }
            timerText.text = minute.ToString("00") + ":" + ((int)time).ToString("00");

            switch (stage)
            {
                case TotalGM.StageCon.First:
                    gm.NowTime[0] += Time.deltaTime;
                    break;

                case TotalGM.StageCon.Secound:
                    gm.NowTime[1] += Time.deltaTime;
                    break;

                case TotalGM.StageCon.Thead:
                    gm.NowTime[2] += Time.deltaTime;
                    break;
            }
        }
    }
}
