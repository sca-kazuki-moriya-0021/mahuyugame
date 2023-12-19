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
        while (count <1000)
        {
            var i = Random.Range(-9.0f,9.1f);
            Instantiate(bullet,new Vector3(transform.position.x+i,transform.position.y),Quaternion.identity);
            count++;
            var a =  Random.Range(0.2f,0.5f);
            yield return new WaitForSeconds(a);
        }

        StopCoroutine(Shoot());
    }
}
