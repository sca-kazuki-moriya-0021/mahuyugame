using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class BarrageEffect : MonoBehaviour
{
    [SerializeField] private Material[] IconColor;
    float amount = 0;
    [SerializeField]
    [Tooltip("球体のパーティクル")]
    private ParticleSystem[] particle;
    [SerializeField]
    [Tooltip("下を円状に回る")]
    private ParticleSystem[] particle1;
    [SerializeField]
    [Tooltip("球体のパーティクル")]
    private ParticleSystem particle2;
    [SerializeField]
    [Tooltip("下を円状に回る")]
    private ParticleSystem particle3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < IconColor.Length; i++)
        {
            IconColor[i].SetFloat("Vector1_9edd6251fe5e4b9c9734d767243022ec", amount);
        }
    }

    public void Amount_0()
    {
        for (int i = 0; i < IconColor.Length; i++)
        {
            particle[i].Play();
            particle1[i].Play();
        }
        particle2.Play();
        particle3.Play();
        amount =1;
        DOTween.To(
            () => amount,
            num => amount = num,
            0,
            3
            );

    }

    public void Amount_1()
    {
        for (int i = 0; i < IconColor.Length; i++)
        {
            particle1[i].Play();
        }
        particle2.Play();
        particle3.Play();
        amount =0;
        DOTween.To(
            () => amount,
            num => amount = num,
            1,
            3
            );

    }
}
