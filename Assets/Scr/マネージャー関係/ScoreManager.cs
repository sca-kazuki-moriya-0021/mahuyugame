using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    private float score;


    private float time;

    private TotalGM gm;
    private TotalGM.StageCon stage;

    private bool bossDidFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();

        stage = gm.MyGetScene();
    }

    // Update is called once per frame
    void Update()
    {

        if (bossDidFlag == false)
        {
            time += Time.deltaTime;
            if (time > 60)
            {
                time = 0;
            }
            scoreText.text = score.ToString();

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
