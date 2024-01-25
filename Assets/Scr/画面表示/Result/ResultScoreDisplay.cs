using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private Text[] score;
    [SerializeField, Header("�ڕW�n�_")]
    private Vector2[] postion;
    [SerializeField, Header("�e�L�X�g�\������܂ł̎���")] 
    private float[] texttime;
    public void Set(string s)
    {
        for(int i= 0; i < score.Length; i++)
        {
            //score[i].text = s[i].ToString();
            score[i].DOText(s[i].ToString(), texttime[i]).SetEase(Ease.Linear);
            //score[i].transform.DOLocalMove(postion[i], 1.5f);
        }
    }
}
