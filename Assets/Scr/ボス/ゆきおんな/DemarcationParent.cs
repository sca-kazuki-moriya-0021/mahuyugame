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
        transform.Translate(transform.position.x +1f,transform.position.y,transform.position.z);
    }
}
