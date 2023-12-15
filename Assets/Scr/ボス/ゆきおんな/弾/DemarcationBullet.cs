using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DemarcationBullet : MonoBehaviour
{
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time+= Time.deltaTime;
        if(time > 10f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

