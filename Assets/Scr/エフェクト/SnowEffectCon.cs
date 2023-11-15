using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEffectCon : MonoBehaviour
{
    //’e•Û‘¶—p
    [SerializeField]
    private ParticleSystem particleSystem;

    private AreaManager areaManager;

    // Start is called before the first frame update
    void Start()
    {
        areaManager = FindObjectOfType<AreaManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(areaManager.BossActiveFlag == true)
        {
            var main = particleSystem.main;
            main.simulationSpeed = 3;
        }
    }


}
