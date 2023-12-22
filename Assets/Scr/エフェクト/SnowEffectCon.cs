using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEffectCon : MonoBehaviour
{
    //’e•Û‘¶—p
    [SerializeField]
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        var main = particleSystem.main;
        main.simulationSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
