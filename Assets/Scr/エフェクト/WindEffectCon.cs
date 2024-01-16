using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectCon : MonoBehaviour
{
    [SerializeField]
     private ParticleSystem[] particleSystems;

    private BossCollder bossCollder;

    // Start is called before the first frame update
    void Start()
    {
        bossCollder = FindObjectOfType<BossCollder>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossCollder.BossDeathFlag == true)
        {
            for(int i = 0; i < particleSystems.Length; i++)
                Destroy(particleSystems[i]);
        }
    }
}
