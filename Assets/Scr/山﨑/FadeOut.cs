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
    [SerializeField]private Light2D worldLight;
    [SerializeField]private float worldLightTime;//暗くしたい時間指定
    float elapsedTime = 0f;//経過時間
    bool  worldLightFlag;
    float rate;//割合
    float worldLightIntensity;

    //private Vector2 playerPosition;


    // Start is called before the first frame update
    void Start()
    {
         
         StartCoroutine(Test());
    }

    void Update()
    {
        if (worldLightFlag)
        {
            elapsedTime += Time.deltaTime;  // 経過時間の加算
            rate = Mathf.Clamp01(elapsedTime / worldLightTime); // 割合計算   
            worldLight.intensity = Mathf.Lerp(1.0f, 0.2f, rate);
        }

        if (elapsedTime >= worldLightTime)
        {
            worldLightFlag = false;
            return;
        }
    }

    //ここでどのシーンで死んだか確認
    private void GameOver()
    {
        Stage_1();
    }

    // Update is called once per frame
    void Stage_1()
    {
        worldLightFlag = true;
        gameOver.enabled = true;
        stageFadeInOut[0].transform.GetChild(0).DOMove(new Vector2(0f, 0f), 2f).SetDelay(3.0f).OnComplete(() => { 
            playerImage.DOFade(255,0f);
            playerPosition.transform.DOMove(new Vector2(0f, -2f), 2f).OnComplete(() => {
                playerImage.DOFade(0, 3f).SetDelay(3.0f); });
        });

    }

    //ここでGameOver検知
    public void TestButton()
    {
        
        
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(5);
        playerPosition.transform.position = totalGM.PlayerTransForm;
        GameOver();
        //Debug.Log(totalGM.PlayerTransForm);
    }
}
