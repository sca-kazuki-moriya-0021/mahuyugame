using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuraObjectCon : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {


    }

    private IEnumerator Shoot()
    {
        var count = 0;
        while (count <120)
        {
            var i = Random.Range(-5.0f,5.1f);
            Instantiate(bullet,new Vector3(transform.position.x+i,transform.position.y),Quaternion.identity);
            count++;
            yield return null;
        }

        StopCoroutine(Shoot());
    }
}
