using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpSilder : MonoBehaviour
{
    [SerializeField]
    private BossCollder bossCollder; 

    private double maxHp =0;
    private double hp = 0;

    [SerializeField]
    private Slider bossHpSilder;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = bossCollder.BossHp;
        hp = maxHp;
        bossHpSilder.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        bossHpSilder.value = (float)bossCollder.BossHp / (float)maxHp;
    }
}
