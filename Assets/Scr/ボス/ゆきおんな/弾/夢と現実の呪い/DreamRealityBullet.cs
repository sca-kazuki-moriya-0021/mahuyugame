using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamRealityBullet : MonoBehaviour
{
    //角度入れる変数
    private float _theta;
    //π
    float PI = Mathf.PI;

    //Way弾の発射角度
    private float launchWayAngle = 360;
    //発射するWay弾の数
    private int waySpilt = 8;

    //子のWay弾の数を増やすよう
    private int waySpiltCount = 0;

    [SerializeField]
    private GameObject bullet;

    private float speed = 1;

    private bool childShootFlag;
    private bool shootFlag = false;

    private float time = 0f;

    public int WaySpiltCount { get => waySpiltCount; set => waySpiltCount = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 3 && childShootFlag == false)
        {
            childShootFlag = true;
            for (int i = 0; i < waySpilt; i++)
            {
                //n-way弾の端から端までの角度
                float AngleRange = PI * (launchWayAngle / 180);
                if (AngleRange > 1) _theta = (AngleRange / (waySpilt)) * i + 0.5f * (PI - AngleRange);
                else _theta = 0.5f * PI;

                GameObject b = Instantiate(bullet, transform.position, transform.rotation);
                DreamRealityChildBullet bullet_cs = b.GetComponent<DreamRealityChildBullet>();
                bullet_cs.ChildWaySpiltCount = waySpiltCount;
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                var bulletv = new Vector2(speed * Mathf.Cos(_theta), speed * Mathf.Sin(_theta));
                rb.velocity = bulletv;
            }
        }

        if (time > 6f && shootFlag == false)
        {
            shootFlag = true;
            Instantiate(bullet, transform.position, transform.rotation);
        }


        if(time > 8f)
        {
            Destroy(this.gameObject);
        }
    }
}
