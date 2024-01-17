using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class SkillEffect : MonoBehaviour
{
    [SerializeField]private Material[] IconColor;
    float amount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < IconColor.Length ; i++)
        {
            IconColor[i].SetFloat("Vector1_9edd6251fe5e4b9c9734d767243022ec", amount);
        }
    }

    public void Amount_0()
    {
        DOTween.To(
            ()=> amount,
            num => amount = num,
            0,
            5
            );
        
    }

    public void Amount_1()
    {
        Debug.Log("‚Í‚¢‚¢‚½");
        DOTween.To(
            () => amount,
            num => amount = num,
            1,
            5
            );

    }
}
