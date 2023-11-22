using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearText : MonoBehaviour
{
    [SerializeField]
    private Text[] nowScoreText;
    [SerializeField]
    private Text[] highScoreText;
    private TotalGM gm;

    private void Awake()
    {
        gm = FindObjectOfType<TotalGM>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < nowScoreText.Length; i++)
        {
            nowScoreText[i].text = gm.NowScore[i].ToString();
            if (gm.NowScore[i] > gm.HighScore[i])
                gm.HighScore[i] = gm.NowScore[i];
            highScoreText[i].text=gm.HighScore[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
