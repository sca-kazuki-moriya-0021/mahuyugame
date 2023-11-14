using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : MonoBehaviour
{
    private float velocity;
    private Vector3 reverseAngle;
    private Vector3 angle;

    private Rigidbody2D rb2d;


    public float Velocity { get => velocity; set => velocity = value; }
    public Vector3 Angle { get => angle; set => angle = value; }
    public Vector3 ReverseAngle {get => reverseAngle; set => reverseAngle = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
     
        Vector3 bulletV = rb2d.velocity;
        bulletV = velocity * angle;
        bulletV.z = 0;
        rb2d.velocity = bulletV;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
