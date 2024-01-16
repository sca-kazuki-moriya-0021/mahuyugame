using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NowLoading : MonoBehaviour
{
    [SerializeField]
    private Image backGround;
    private TotalGM totalGM;

    //ボス保存用
    [SerializeField]
    private GameObject bossObject;

    private bool fadeInFlag = false;

    private bool fadeOutFlag = true;

    private BossCollder bossCollder;

    public bool FadeInFlag
    {
        get { return this.fadeInFlag; }
        set { this.fadeInFlag = value; }
    }

    public bool FadeOutFlag
    {
        get { return this.fadeOutFlag; }
        set { this.fadeOutFlag = value; }
    }

    // Start is called before the first frame update

    void Start()
    {
        //念のため
        //backGround.enabled = false;
        bossCollder = FindObjectOfType<BossCollder>();
        totalGM = FindObjectOfType<TotalGM>();
        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FadeIn()
    {
        //現在のTweenを保存する用変数
        fadeInFlag = true;
        backGround.enabled=true;
        //現在のTween内容を代入
        
        backGround.DOFade(2.55f,1.0f).SetEase(Ease.Linear).SetDelay(1.0f).OnComplete(() =>
        {
            var scene = totalGM.MyGetScene();
            switch (scene)
            {
                case TotalGM.StageCon.First:
                    CleaningUp();
                    SceneManager.LoadScene("Clear");
                    break;
                case TotalGM.StageCon.Secound:
                    CleaningUp();
                    SceneManager.LoadScene("Clear");
                    break;
                case TotalGM.StageCon.Thead:
                    CleaningUp();
                    SceneManager.LoadScene("Clear");
                    break;
            }
        });
    }

    private void FadeOut()
    {
        backGround.DOFade(endValue:0f,duration:1.5f).OnComplete(() =>{
            fadeOutFlag = false;
        });
    }

    //ステージ移行時
    private void CleaningUp()
    {
        fadeInFlag = false;
        bossCollder.BossDeathFlag = false;
        Destroy(bossObject);
    }

}
