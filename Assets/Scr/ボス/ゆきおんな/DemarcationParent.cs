using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DemarcationParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.DORotate(Vector3.forward * 180f, 1f);
    }
}
