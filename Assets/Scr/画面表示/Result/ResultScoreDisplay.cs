using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private Text[] score;
  
    public void Set(string s)
    {
        for(int i= 0; i < score.Length; i++)
        {
            score[i].text = s[i].ToString();
        }
    }
}
