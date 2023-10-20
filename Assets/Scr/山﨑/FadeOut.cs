using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField]private GameObject[] stageFadeOut;
    [SerializeField]private Image gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        gameOver.enabled = true;
        stageFadeOut[0].transform.GetChild(0).DOMove(new Vector3(0f, 0f, 0f), 2f);
    }

    //ここでGameOver検知
    public void TestButton()
    {
        GameOver();
    }

    
}
