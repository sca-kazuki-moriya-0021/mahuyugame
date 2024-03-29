using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AreaManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private PouseCon pouseCon;
    [SerializeField]
    private CountDownCon countDownCon;
    [SerializeField]
    private BossCollder bossCollder;

    private bool playFlag = false;
    private bool downFlag = false;
    private bool stageChangeFlag = false;


    //ボスオブジェクト
    //[SerializeField]
    //private GameObject bossObject;

    //雑魚オブジェクトの配列
    //[SerializeField]
    //private GameObject[] enemyObeject;

    //中ボスオブジェクト
    //[SerializeField]
    //private GameObject underBossObject



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {

      if((pouseCon.MenuFlag == true && countDownCon.CountDownFlag == false))
      {
            downFlag = false;
            if(playFlag == false)
            {
                playFlag = true;
                this.audioSource.DOFade(endValue:0f,duration:1f).SetUpdate(true).OnComplete(() =>
                { 
                    audioSource.Pause();
                });
            }
      }

      if(countDownCon.CountDownFlag == true && pouseCon.MenuFlag == true)
      {
            if (downFlag == false)
            {
                Debug.Log("入ってる");
                audioSource.Play();
                playFlag = false;
                downFlag = true;
                this.audioSource.DOFade(endValue: 1f, duration: 3f).SetUpdate(true);
            }
      }

      if (bossCollder.BossDeathFlag == true)
      {
           if(stageChangeFlag == false)
           {
              StartCoroutine(DownVolume());
              stageChangeFlag = true;
           }
      }
    }


    /*private IEnumerator PouseVolume()
    {
        float downTime = 0;
        while (audioSource.volume >= 0)
        {
            downTime += Time.unscaledDeltaTime;
            audioSource.volume -= downTime;
            yield return new WaitForSecondsRealtime(0.01f);
        }

        StopCoroutine(PouseVolume());
    }*/

    private IEnumerator DownVolume()
    {
        float downTime = 0;
        while(audioSource.volume >= 0)
        {
            downTime += Time.unscaledDeltaTime;
            audioSource.volume -= downTime;
            yield return new WaitForSecondsRealtime(0.12f);
        }
        audioSource.Pause();
        StopCoroutine(DownVolume());
    }
}
