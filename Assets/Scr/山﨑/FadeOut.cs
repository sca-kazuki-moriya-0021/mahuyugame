using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//using UnityEngine.Rendering.Universal;//Light2Dを使うのに必要
using UnityEngine.Experimental.Rendering.Universal;

public class FadeOut : MonoBehaviour
{
    [SerializeField]private GameObject[] stageFadeInOut;
    [SerializeField]private Image[] backGround;
    [SerializeField]private Image[] gameOver;
    [SerializeField]private TotalGM totalGM;
    [SerializeField]private Image[] playerImage;
    [SerializeField]private GameObject playerPosition;
    [SerializeField]private Light2D light2D;
    [SerializeField] private Light2D WorldLight2D;
    [SerializeField]private Animator fadeOutAnimator;
    [SerializeField] private Image[] EnemyBoss;
    


    //private Vector2 playerPosition;


    // Start is called before the first frame update
    void Start()
    {
         light2D.enabled = true;
         StartCoroutine(Test());
         
    }

    void Update()
    {
        
    }

    //ここでどのシーンで死んだか確認
    private void GameOver()
    {
        //playerImage[0].transform.position = playerPosition.transform.position;
        playerImage[2].transform.position = playerPosition.transform.position;//totalGM.PlayerTransForm;
        //Stage_1();
        Stage_3();
    }

    // Update is called once per frame
    void Stage_1()
    {
        
        

        //fadeOutAnimator.SetTrigger("Stage1");

        backGround[0].transform.DOMove(new Vector2(0f, 0f), 2f).SetDelay(3.0f).OnComplete(() => { 
            
            IntensityChg();
            playerImage[0].DOFade(2.55f, 2f).SetDelay(1.0f);
            playerImage[0].transform.DOMove(new Vector2(0f, -2f), 2f).SetDelay(1.0f).OnComplete(() => {
                playerImage[0].DOFade(0, 3f).SetDelay(3.0f);
                
                EnemyBoss[0].rectTransform.DOMove(new Vector2(5f,-1.6f),1f);
                });
        });

    }

    private void Stage_3()
    {
        var brackColor = new Color(0f, 0f, 0f);
        var whiteColor = new Color(255f, 255f, 255f);
        gameOver[2].DOFade(2.55f,1.0f).OnComplete(() => { 
            playerImage[2].enabled = true;
            playerImage[2].DOColor(brackColor, 1.0f).OnComplete(() => {
                WorldLight2D.enabled=false;
                IntensityChg();
                backGround[2].DOColor(whiteColor, 2.0f);
                playerImage[2].transform.DOMove(new Vector2(0f, 0f), 2f).OnComplete(() => {
                    
                    playerImage[2].DOFade(255, 2f).SetDelay(1.0f).OnComplete(() => {
                        fadeOutAnimator.SetTrigger("Stage3");
                    });
                });           
            });
        });
        
    }

    //光の明るさ調整
    private void IntensityChg()
    {
        DOTween.To(
            () => light2D.intensity,
            num => light2D.intensity = num,
            0.6f,
            3.0f);
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(1);
        
        GameOver();
        //Debug.Log(totalGM.PlayerTransForm);
    }
}
