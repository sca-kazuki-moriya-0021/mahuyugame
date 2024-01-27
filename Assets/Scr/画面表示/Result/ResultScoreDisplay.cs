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
    [SerializeField, Header("�ړ�����")] 
    private float movetime;
    private bool stop;
    //text�̃^�C��
    private float texttime = 0.5f;

    public bool Stop { get => stop; set => stop = value; }

    //�������Z�b�g���邾��
    public void Set(string s)
    {
        for(int i= 0; i < score.Length; i++)
        score[i].text = s[i].ToString();
        StartCoroutine(SetText());
    }

    //�\������
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
