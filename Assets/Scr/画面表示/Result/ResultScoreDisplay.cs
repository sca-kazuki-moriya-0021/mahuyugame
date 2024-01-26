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

    //textのタイム
    private float texttime = 0.5f;

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
            //score[i].transform.DOLocalMove(postion[i], 1.5f);
            yield return new WaitForSecondsRealtime(texttime);
        }
    }
}
