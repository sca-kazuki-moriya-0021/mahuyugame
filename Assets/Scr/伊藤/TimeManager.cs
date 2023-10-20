using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private TotalGM totalGM;
    float[]StargeTime={0.0f,0.0f,0.0f};
    float[] StargeCleaTime={0.0f,0.0f,0.0f };
    string scenNameNow;
    [SerializeField]private Text timerText;
    //[SerializeField]private Text timerText02;
    //[SerializeField] private Text timerText03;
    private int minute;
    
    private void Awake()
    {
        totalGM = FindObjectOfType<TotalGM>();
        minute = 0;
        //DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        var scene = totalGM.MyGetScene();
        timerText = GetComponent<Text>();
        Debug.Log(timerText);
        switch (scene)
        {
            case TotalGM.StageCon.First:
                StartCoroutine("TimeStarge01");
                minute -= ((int)totalGM.NowTime[0]);
                break;
            case TotalGM.StageCon.Secound:
                StartCoroutine("TimeStarge02");
                minute-= ((int)totalGM.NowTime[1]);
                break;
            case TotalGM.StageCon.Thead:
                StartCoroutine("TimeStarge03");
                minute -= ((int)totalGM.NowTime[2]);
                break;
            case TotalGM.StageCon.Clear:
                StartCoroutine("TotalTime");
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TimeStarge01()
    {
        Debug.Log("Yes");
        //StargeTime[0] += Time.deltaTime;
        totalGM.NowTime[0] += Time.deltaTime;
        Debug.Log(totalGM.NowTime[0]);
        timerText.text = minute.ToString("00") + ":" + ((int)totalGM.NowTime[0]).ToString("00");
        if (totalGM.NowTime[0] == 15.0f)
        {
            totalGM.NowTime[0] = StargeCleaTime[0];
        }
        else if (StargeCleaTime[0] > totalGM.LastTime[0])
        {
            totalGM.LastTime[0] = StargeCleaTime[0];
        }
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator TimeStarge02()
    {
        
        yield return new WaitForSeconds(1.0f);
        StargeTime[1] += Time.deltaTime;
        totalGM.NowTime[1] = StargeTime[1];
        timerText.text = minute.ToString("00") + ":" + ((int)totalGM.NowTime[1]).ToString("00");
        if (totalGM.NowTime[1] == 15.0f)
        {
            totalGM.NowTime[1] = StargeCleaTime[1];
            Debug.Log(StargeCleaTime[1]);
        }
        else if (StargeCleaTime[1] > totalGM.LastTime[1])
        {
            totalGM.LastTime[1] = StargeCleaTime[1];
        }
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator TimeStarge03()
    {
        yield return new WaitForSeconds(1.0f);
        StargeTime[2] += Time.deltaTime;
        totalGM.NowTime[2] = StargeTime[2];
        timerText.text = minute.ToString("00") + ":" + ((int)totalGM.NowTime[2]).ToString("00");
        if (totalGM.NowTime[2] == 15.0f)
        {
            totalGM.NowTime[2] = StargeCleaTime[2];
            Debug.Log(StargeCleaTime[2]);
        }
        else if (StargeCleaTime[2] > totalGM.LastTime[2])
        {
            totalGM.LastTime[2] = StargeCleaTime[2];
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("TotalTime");
    }
    IEnumerable TotalTime()
    {
        yield return new WaitForSeconds(1.0f);
        StargeCleaTime.GetEnumerator();
    }
}