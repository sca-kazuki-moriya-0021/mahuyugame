using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerMoveBullet : MonoBehaviour
{
    //四つ角の場所
    private GameObject cornerObject;

    //スピード
    private float speed = 10f;

    //弾のスピード
    private float bulletSpeed = 1f; 

    //目標地点
    private GameObject[] cornerPosChild = new GameObject[4];

    //動いているかどうか
    private bool moveColFlag;

    //中央値
    private Vector3 originPos;

    //出す弾
    [SerializeField]
    private GameObject childBullet;

    private SnowFairyBulletCon snow;


    //発射タイム
    private float time;

    //初期値
    private Vector3 initializationPos;

    public Vector3 InitializationPos 
    {
        get { return this.initializationPos; }
        set { this.initializationPos = value; }
    }

    public GameObject CornerObject
    {
        get { return this.cornerObject; }
        set { this.cornerObject = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        snow = FindObjectOfType<SnowFairyBulletCon>();

        //目標地点を設定
        for (int i = 0; i < cornerPosChild.Length; i++)
        {
            cornerPosChild[i] = cornerObject.transform.GetChild(i).gameObject;
            //Debug.Log(cornerPosChild[i].gameObject.transform.position);
        }

        //現在地を初期化する
        transform.position = initializationPos;

        //中央座標を獲得する
        originPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > 0.5f)
        {
            ShootBullet();
            time = 0.0f;
        }

        if(snow.BulletDeleteFlag[0] == true)
         Destroy(this.gameObject);

        //移動調整
        if (moveColFlag == false)
        {
            if (cornerPosChild[0].gameObject.transform.position == this.transform.position)
                StartCoroutine(MovePosition(0, 1));
            else if (cornerPosChild[1].gameObject.transform.position == this.transform.position)
                StartCoroutine(MovePosition(1, 2));
            else if (cornerPosChild[2].gameObject.transform.position == this.transform.position)
                StartCoroutine(MovePosition(2, 3));
            else if (cornerPosChild[3].gameObject.transform.position == this.transform.position)
                StartCoroutine(MovePosition(3, 0));
        }
    }

    //移動調整
    private IEnumerator MovePosition(int a, int b)
    {
        float time = 0;
        moveColFlag = true;
        float dir = Mathf.Abs(Vector3.Distance(cornerPosChild[a].transform.position, cornerPosChild[b].transform.position));
     
        while (cornerPosChild[b].transform.position != transform.position)
        {
            time += Time.deltaTime;
            float pos = (time * speed) / dir;
            transform.position = Vector3.Lerp(cornerPosChild[a].transform.position, cornerPosChild[b].transform.position, pos);
            yield return null;
        }
        
        var i = Random.Range(0.5f,1f);
        yield return new WaitForSecondsRealtime(0.2f + i);

        moveColFlag = false;
        StopCoroutine(MovePosition(a, b));
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(childBullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        var dir = originPos - transform.position;
        dir = dir.normalized;
        rb.velocity = dir * bulletSpeed;
    }

}
