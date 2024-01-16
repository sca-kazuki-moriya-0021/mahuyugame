using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StageFadeIn : MonoBehaviour
{
    [SerializeField]
    private Image backGround;

    private bool fadeInFlag = true;

    public bool FadeInFlag { get => fadeInFlag; set => fadeInFlag = value; }

    // Start is called before the first frame update
    void Awake()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FadeIn()
    {
        backGround.DOFade(endValue: 0f, duration: 1.5f).OnComplete(() => {
            fadeInFlag = false;
        });
    }
}
