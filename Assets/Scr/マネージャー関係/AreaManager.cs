using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private PouseCon pouseCon;
    [SerializeField]
    private CountDownCon countDownCon;
    [SerializeField]
    private BossCollder bossCollder;

    private float downTime;

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
            downTime += Time.deltaTime;
            audioSource.volume -= downTime;
            if(audioSource.volume <= 0)
                audioSource.Pause();
      }

      if(countDownCon.CountDownFlag == true)
      {
            if (downFlag == false)
            {

                audioSource.Play();
                StartCoroutine(UpVolume());
                downFlag = true;
            }
      }

      if (bossCollder.BossDeathFlag == true)
      {
           if(stageChangeFlag == false)
           {
              Debug.Log("音量入ったよ");
              StartCoroutine(DownVolume());
              stageChangeFlag = true;
           }
           
      }
    }


    private IEnumerator UpVolume()
    {
        float upTime =0;

        while(audioSource.volume <= 1)
        {
            upTime += Time.unscaledDeltaTime;
            audioSource.volume += upTime;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        StopCoroutine(UpVolume());
    }

    private IEnumerator DownVolume()
    {
        float downTime = 0;
        while(audioSource.volume >= 0)
        {
            downTime += Time.unscaledDeltaTime;
            audioSource.volume -= downTime;
            yield return new WaitForSecondsRealtime(0.12f);
        }
        StopCoroutine(DownVolume());
    }
}
