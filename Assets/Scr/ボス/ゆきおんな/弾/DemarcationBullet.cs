using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DemarcationBullet : MonoBehaviour
{
    //äpìx
    private Vector2 bulletVelocity;

    //íÜêS
    private Vector2 center;

    private Rigidbody2D rigidbody2D;

    public Vector2 BulletVelocity {
        get { return this.bulletVelocity; }
        set { this.bulletVelocity = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        center = transform.parent.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnBecameInvisible()
    {
        //Destroy(this.gameObject);
    }

}

