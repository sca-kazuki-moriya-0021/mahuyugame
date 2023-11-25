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
    // Start is called before the first frame update

    void Start()
    {
        //”O‚Ì‚½‚ß
        //backGround.enabled = false;
        totalGM = FindObjectOfType<TotalGM>();;
        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  FadeIn()
    {
        backGround.enabled=true;
        backGround.DOFade(2.55f,1.0f).SetEase(Ease.Linear).SetDelay(1.0f).OnComplete(() =>
        { 
            var scene = totalGM.MyGetScene();
            switch (scene)
            {
                case TotalGM.StageCon.First:
                    SceneManager.LoadScene("SecondStage");
                    break;
                case TotalGM.StageCon.Secound:
                    SceneManager.LoadScene("TheadStage");
                    break;
                case TotalGM.StageCon.Thead:
                    SceneManager.LoadScene("Clear");
                    break;
            }
            
        });
    }

    private void FadeOut()
    {
        Debug.Log("“ü‚Á‚Ä‚é");
        backGround.DOFade(endValue:0f,duration:1.0f);//.SetEase(Ease.Linear);
    }
    
}
