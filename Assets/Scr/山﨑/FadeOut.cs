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
    [SerializeField]private Image gameOver;
    [SerializeField]private TotalGM totalGM;
    [SerializeField]private Image playerImage;
    [SerializeField]private GameObject playerPosition;
    [SerializeField]private Light2D spotLight;
    [SerializeField]private Animator fadeOutAnimator;
    [SerializeField] private Image[] EnemyBoss;
    bool  worldLightFlag;


    //private Vector2 playerPosition;


    // Start is called before the first frame update
    void Start()
    {
         
         StartCoroutine(Test());
         gameOver.enabled = false;
    }

    void Update()
    {
        
    }

    //ここでどのシーンで死んだか確認
    private void GameOver()
    {
        Stage_1();
    }

    // Update is called once per frame
    void Stage_1()
    {
        
        

        fadeOutAnimator.SetTrigger("Stage1");

        stageFadeInOut[0].transform.GetChild(0).DOMove(new Vector2(0f, 0f), 2f).SetDelay(3.0f).OnComplete(() => { 
            
            IntensityChg();
            playerImage.DOFade(255, 2f).SetDelay(1.0f);
            playerPosition.transform.DOMove(new Vector2(0f, -2f), 2f).SetDelay(1.0f).OnComplete(() => {
                playerImage.DOFade(0, 3f).SetDelay(3.0f);
                
                EnemyBoss[0].rectTransform.DOMove(new Vector2(5f,-1.6f),1f);
                });
        });

    }

    private void IntensityChg()
    {
        DOTween.To(
            () => spotLight.intensity,
            num => spotLight.intensity = num,
            0.6f,
            3.0f);
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(1);
        playerPosition.transform.position = totalGM.PlayerTransForm;
        GameOver();
        //Debug.Log(totalGM.PlayerTransForm);
    }
}
