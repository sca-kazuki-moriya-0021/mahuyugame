using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ScoreText : MonoBehaviour
{

    [SerializeField]
    private TMP_Text scoreText;
    private float baseScore = 1000;
    private float time = 0;

    private TotalGM gm;
    private TotalGM.StageCon stage;

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
        time += Time.deltaTime;
        if(time > 1)
        {
            switch (stage)
            {
                case TotalGM.StageCon.First:
                    ScoreCount(0);
                    break;

                case TotalGM.StageCon.Secound:
                    ScoreCount(1);
                    break;

                case TotalGM.StageCon.Thead:
                    ScoreCount(2);
                    break;
            }
            time = 0;
        }
    }

    private void ScoreCount(int i)
    {
        gm.NowScore[i] = gm.PlayerHp[0] * baseScore - (gm.GameOverCount * baseScore);
    }
}
