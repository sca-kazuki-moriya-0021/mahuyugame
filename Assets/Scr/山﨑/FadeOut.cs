using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//using UnityEngine.Rendering.Universal;//Light2D���g���̂ɕK�v
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
    //[SerializeField] private Image[] EnemyBoss;
    


    //private Vector2 playerPosition;


    // Start is called before the first frame update
    void Start()
    {
         GameOver();
    }

    void Update()
    {
        
    }

    //�����łǂ̃V�[���Ŏ��񂾂��m�F
    private void GameOver()
    {
        fadeOutAnimator.SetTrigger("Stage1");
    }
}
