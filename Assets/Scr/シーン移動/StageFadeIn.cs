using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StageFadeIn : MonoBehaviour
{
    [SerializeField]
    private Image backGround;

    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FadeIn()
    {
        backGround.DOFade(endValue: 0f, duration: 1.0f);
    }
}
