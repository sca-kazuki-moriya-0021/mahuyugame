using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearText : MonoBehaviour
{
    [SerializeField]
    private Text[] nowTime;
    [SerializeField]
    private Text[] bestTime;
    private TotalGM gm;
    private float minute;

    private void Awake()
    {
        gm = FindObjectOfType<TotalGM>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < nowTime.Length; i++)
        {
           var m = gm.NowTime[i] % 60;
           minute = (gm.NowTime[i] - m) /60;
           nowTime[i].text=minute.ToString("00")+":"+m.ToString("00");
            if (gm.NowTime[i] < gm.LastTime[i])
            {
                gm.LastTime[i]=gm.NowTime[i];
                bestTime[i].text=nowTime[i].text;
            }
            else
            {
                var mg = gm.LastTime[i] % 60;
                minute = (gm.LastTime[i] - mg) / 60;
                bestTime[i].text = minute.ToString("00") + ":" + mg.ToString("00");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
