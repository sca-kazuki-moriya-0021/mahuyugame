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

    private float downTime;

    private bool playFlag = false;

    //ボスオブジェクト
    //[SerializeField]
    //private GameObject bossObject;

    //雑魚オブジェクトの配列
    //[SerializeField]
    //private GameObject[] enemyObeject;

    //中ボスオブジェクト
    //[SerializeField]
    //private GameObject underBossObject

    private TotalGM gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
      if(pouseCon.MenuFlag == true && countDownCon.CountDownFlag == false)
      {
            if(playFlag == true)
            playFlag = false;

            downTime += Time.deltaTime;
            audioSource.volume -= downTime;
            if(audioSource.volume <= 0)
                audioSource.Pause();
      }

      if(countDownCon.CountDownFlag == true)
      {
            if(playFlag == false)
            {
                Debug.Log("音量入ったよ");
                audioSource.Play();
                StartCoroutine(UpVolume());
                playFlag = true;
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
}
