using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private Text[] score;
    [SerializeField, Header("目標地点")]
    private Vector2[] postion;
    [SerializeField, Header("移動時間")] 
    private float movetime;
    private bool stop;
    //textのタイム
    private float texttime = 0.5f;

    public bool Stop { get => stop; set => stop = value; }

    //文字をセットするだけ
    public void Set(string s)
    {
        for(int i= 0; i < score.Length; i++)
        score[i].text = s[i].ToString();
        StartCoroutine(SetText());
    }

    //表示する
    private IEnumerator SetText()
    {
        for (int i = 0; i < score.Length; i++)
        {
            score[i].DOFade(1f, texttime);
            score[i].transform.DOLocalMove(postion[i], movetime);
            yield return new WaitForSecondsRealtime(texttime);
        }
        stop=true;
        StopCoroutine(SetText());
    }
}
