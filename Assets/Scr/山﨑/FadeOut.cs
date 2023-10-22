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
    [SerializeField]private Image gameOver;
    [SerializeField]private TotalGM totalGM;
    [SerializeField]private Image playerImage;
    [SerializeField]private GameObject playerPosition;
    [SerializeField]private Light2D worldLight;
    [SerializeField]private Animator fadeOutAnimator;
    [SerializeField] private Image[] EnemyBoss;
    [SerializeField]private float worldLightTime;//�Â����������Ԏw��
    float elapsedTime = 0f;//�o�ߎ���
    bool  worldLightFlag;
    float rate;//����
    float worldLightIntensity;

    //private Vector2 playerPosition;


    // Start is called before the first frame update
    void Start()
    {
         
         StartCoroutine(Test());
         gameOver.enabled = false;
    }

    void Update()
    {
        if (worldLightFlag)
        {
            elapsedTime += Time.deltaTime;  // �o�ߎ��Ԃ̉��Z
            rate = Mathf.Clamp01(elapsedTime / worldLightTime); // �����v�Z   
            worldLight.intensity = Mathf.Lerp(1.0f, 0.2f, rate);
        }

        if (elapsedTime >= worldLightTime)
        {
            worldLightFlag = false;
            return;
        }
    }

    //�����łǂ̃V�[���Ŏ��񂾂��m�F
    private void GameOver()
    {
        Stage_1();
    }

    // Update is called once per frame
    void Stage_1()
    {
        worldLightFlag = true;
        

        fadeOutAnimator.SetTrigger("Stage1");

        stageFadeInOut[0].transform.GetChild(0).DOMove(new Vector2(0f, 0f), 2f).SetDelay(3.0f).OnComplete(() => { 
            playerImage.DOFade(255,0f);
            playerPosition.transform.DOMove(new Vector2(0f, -2f), 2f).OnComplete(() => {
                playerImage.DOFade(0, 3f).SetDelay(3.0f); 
                EnemyBoss[0].transform.DOMove(new Vector2(500,-160),1f);
                });
        });

    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(1);
        playerPosition.transform.position = totalGM.PlayerTransForm;
        GameOver();
        //Debug.Log(totalGM.PlayerTransForm);
    }
}
