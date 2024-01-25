using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    //[SerializeField] private TMP_Text tmptext;
    [SerializeField] private Text[] _text;
    [SerializeField] private float[] score;
    [SerializeField,Header("目標地点")] private Vector2[] posion; 
    [SerializeField,Header("テキスト表示するまでの時間")] private float texttime;
    [SerializeField,Header("移動時間")] private float movetime;
    // Start is called before the first frame update
    void Start()
    {
        //_text[0].DOText(score[0].ToString(),5);
        for (int i = 0; i < _text.Length; i++)
        {
            _text[i].DOText(score[i].ToString(), texttime).SetEase(Ease.Linear);
            _text[i].transform.DOLocalMove(posion[i],movetime);
            Debug.Log("a");
        }
    }
}
