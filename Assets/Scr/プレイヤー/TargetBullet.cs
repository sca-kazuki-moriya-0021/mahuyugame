using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var accleration = Vector3.zero;
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach(GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistansce = diff.sqrMagnitude;
            if(curDistansce < distance)
            {
                closest = go;
                distance = curDistansce;
            }
        }
        return closest;
    }

}
