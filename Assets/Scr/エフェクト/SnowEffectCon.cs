using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEffectCon : MonoBehaviour
{
    //’e•Û‘¶—p
    [SerializeField]
    private ParticleSystem particleSystem;

    [SerializeField]
    private SnowFairyBulletCon snow;

    [SerializeField]
    private GameObject blizzard;

    private bool stopFlag;

    // Start is called before the first frame update
    void Start()
    {
        var main = particleSystem.main;
        main.simulationSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
       if(snow.BlizzardFlag == true)
            particleSystem.loop = false;

       if(stopFlag == true)
       {
           stopFlag = false;
           if(blizzard.activeSelf == false)
           blizzard.SetActive(true);
       }
    }

    private void OnParticleSystemStopped()
    {
        stopFlag = true;
    }
}
