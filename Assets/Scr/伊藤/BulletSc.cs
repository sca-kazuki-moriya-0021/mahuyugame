using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSc : MonoBehaviour
{
    public float Velocity_0,theta;

    Rigidbody2D rid2d;
    // Start is called before the first frame update
    void Start()
    {
        rid2d=GetComponent<Rigidbody2D>();

        Vector2 bulletV=rid2d.velocity;
        bulletV.x=Velocity_0*Mathf.Cos(theta);
        bulletV.y=Velocity_0*Mathf.Sin(theta);
        rid2d.velocity=bulletV;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}
