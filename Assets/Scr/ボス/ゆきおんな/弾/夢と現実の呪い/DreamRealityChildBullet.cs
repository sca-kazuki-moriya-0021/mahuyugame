using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamRealityChildBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private float speed = 2;
    
    //Šp“x“ü‚ê‚é•Ï”
    private float _theta;
    //ƒÎ
    float PI = Mathf.PI;

    //Way’e‚Ì”­ŽËŠp“x
    private float launchWayAngle = 360;
    //”­ŽË‚·‚éWay’e‚Ì”
    private int waySpilt = 10;

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
        Debug.Log(waySpritCount);
        time += Time.deltaTime;
        time += Time.deltaTime;
        if (time > 2 && shootFlag == false)
        {
            shootFlag = true;
            for (int i = 0; i < waySpritCount + waySpilt; i++)
            {
                //n-way’e‚Ì’[‚©‚ç’[‚Ü‚Å‚ÌŠp“x
                float AngleRange = PI * (launchWayAngle / 180);
                if (AngleRange > 1) _theta = (AngleRange / (waySpilt + waySpritCount)) * i + 0.5f * (PI - AngleRange);
                else _theta = 0.5f * PI;

                GameObject b = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                var bulletv = new Vector2(speed * Mathf.Cos(_theta), speed * Mathf.Sin(_theta));
                rb.velocity = bulletv;
            }
            Destroy(this.gameObject);
        }
    }
}
