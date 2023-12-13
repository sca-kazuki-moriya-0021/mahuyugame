using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerMoveBullet : MonoBehaviour
{
    //四つ角の場所
    private GameObject cornerObject;

    //スピード
    private float speed = 10f;

    //目標地点
    private GameObject[] cornerPosChild = new GameObject[4];

    //動いているかどうか
    private bool moveColFlag;

    private Vector3 originPos;

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
        //目標地点を設定
        for (int i = 0; i < cornerPosChild.Length; i++)
        {
            cornerPosChild[i] = cornerObject.transform.GetChild(i).gameObject;
            //Debug.Log(cornerPosChild[i].gameObject.transform.position);
        }

        //現在地を初期化する
        transform.position = initializationPos;
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

        //移動調整
        if (moveColFlag == false)
        {
            Debug.Log("mituka");
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
        Debug.Log("コルーチン入ったよ");
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

        yield return new WaitForSecondsRealtime(2f);

        moveColFlag = false;
        StopCoroutine(MovePosition(a, b));
    }

    private void ShootBullet()
    {
        //GameObject bullet = Instantiate(bullets[number], transform.position, Quaternion.identity);
    }
}
