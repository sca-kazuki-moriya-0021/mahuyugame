using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CornerChildBullet : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnBecameInvisible()
    {
       Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        Destroy(this.gameObject);
    }


}
