using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class PlayerPointLightCon : MonoBehaviour
{
    [SerializeField]
    private Light2D light2D;

    [SerializeField]
    private CircleCollider2D collider2D;

    [SerializeField]
    private SnowFairyBulletCon bulletCon;

    [SerializeField]
    private float coolTime;

    private bool test = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletCon.BlizzardFlag == true && test == false)
        {
            test = true;
            StartCoroutine(BlizzardLight());
        }

    }

    private IEnumerator BlizzardLight()
    {
        while (true)
        {
            collider2D.enabled = false;
            light2D.intensity = 0f;
            yield return new WaitForSeconds(coolTime);
            collider2D.enabled = true;
            light2D.intensity = 1f;
            yield return new WaitForSeconds(coolTime);
        }
    }
}
