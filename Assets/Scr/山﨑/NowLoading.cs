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

    private bool fadeInFlag = false;

    private BossCollder bossCollder;

    public bool FadeInFlag
    {
        get { return this.fadeInFlag; }
        set { this.fadeInFlag = value; }
    }

    // Start is called before the first frame update

    void Start()
    {
        //”O‚Ì‚½‚ß
        //backGround.enabled = false;
        totalGM = FindObjectOfType<TotalGM>();
        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        //Œ»Ý‚ÌTween‚ð•Û‘¶‚·‚é—p•Ï”
        Tween tween = null;
        fadeInFlag = true;
        backGround.enabled=true;
        //Œ»Ý‚ÌTween“à—e‚ð‘ã“ü
        backGround.DOFade(2.55f,1.0f).SetEase(Ease.Linear).SetDelay(1.0f).OnComplete(() =>
        { 
            bossCollder.BossDeathFlag = false;
            var scene = totalGM.MyGetScene();
            Debug.Log("“ü‚Á‚½");
            switch (scene)
            {
                case TotalGM.StageCon.First:
                    fadeInFlag = false;
                    //tween?.Kill();
                    SceneManager.LoadScene("SecondStage");
                    break;
                case TotalGM.StageCon.Secound:
                    fadeInFlag = false;
                    //tween?.Kill();
                    SceneManager.LoadScene("TheadStage");
                    break;
                case TotalGM.StageCon.Thead:
                    fadeInFlag = false;
                    //tween?.Kill();
                    SceneManager.LoadScene("Clear");
                    break;
            }
            
        });
    }

    private void FadeOut()
    {
        backGround.DOFade(endValue:0f,duration:1.0f);//.SetEase(Ease.Linear);
    }
    
}
