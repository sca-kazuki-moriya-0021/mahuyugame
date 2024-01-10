using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamRealityChildBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private float speed = 2;
    
    //角度入れる変数
    private float _theta;
    //π
    float PI = Mathf.PI;

    //Way弾の発射角度
    private float launchWayAngle = 360;
    //発射するWay弾の数
    private int waySpilt = 8;

    private int waySpritCount=0;

    private float time = 0;

    private bool shootFlag = false;

    public int ChildWaySpiltCount { get => waySpritCount; set => waySpritCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        time += Time.deltaTime;
        if (time > 3 && shootFlag == false)
        {
            shootFlag = true;
            for (int i = 0; i < waySpritCount + waySpilt; i++)
            {
                //n-way弾の端から端までの角度
                float AngleRange = PI * (launchWayAngle / 180);
                if (AngleRange > 1) _theta = (AngleRange / (waySpilt)) * i + 0.5f * (PI - AngleRange);
                else _theta = 0.5f * PI;

                GameObject b = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                var bulletv = new Vector2(speed * Mathf.Cos(_theta), speed * Mathf.Sin(_theta));
                rb.velocity = bulletv;
            }
        }

        if(time > 5)
        {
            Destroy(this.gameObject);
        }
    }
}
