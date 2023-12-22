using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpSilder : MonoBehaviour
{
    private BossCollder bossCollder; 

    private int maxHp =0;
    private int hp = 0;

    [SerializeField]
    private Slider bossHpSilder;

    // Start is called before the first frame update
    void Start()
    {
        bossCollder = FindObjectOfType<BossCollder>();
        maxHp = bossCollder.BossHp;
        hp = maxHp;
        bossHpSilder.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hp);
        bossHpSilder.value = (float)bossCollder.BossHp / (float)maxHp;
    }
}
