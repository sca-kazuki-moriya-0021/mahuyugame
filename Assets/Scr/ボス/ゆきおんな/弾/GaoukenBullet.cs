using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaoukenBullet : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private Vector3 dir;

    private float speed = 2f;

    private float time = 0f;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    private float count = 1;

    public Vector3 Dir
    {
        get { return this.dir; }
        set { this.dir = value; }
    }

    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D.velocity = dir * speed;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
       if(time > 3f)
       {
            count++;
            time = 0;
       }

       if(count % 2 == 0)
          rigidbody2D.velocity = dir * speed *0.5f;
       else
         rigidbody2D.velocity = dir * speed;

       if(player.BulletSeverFlag == true)
            Destroy(this.gameObject);

    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
