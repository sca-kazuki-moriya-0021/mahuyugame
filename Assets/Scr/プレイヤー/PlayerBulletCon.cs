using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerBulletPool;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerBulletCon : MonoBehaviour
{
    //弾保存用
    [SerializeField]
    private GameObject[] bullets;
    //弾の発射位置
    private GameObject[] bulletChilds = new GameObject[]{null,null,null};

    //レーザー反射のスクリプト
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float[] angle;

    float PI = Mathf.PI;

    private float time;

    private TotalGM gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();

        //自分の子オブジェクトを取得
        int childCount = this.gameObject.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
           Transform childTransform = this.gameObject.transform.GetChild(i);
           bulletChilds[i] = childTransform.gameObject;
           Debug.Log(bulletChilds[i]);
        }

        //角度をラジアンに変換
        for(int i = 0; i < angle.Length; i++)
        {
            angle[i] = angle[i] * Mathf.Deg2Rad;

        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 5)
        {
            if (gm.PlayerWeapon[0] == true)
            {
                for (int i = 0; i < bulletChilds.Length; i++)
                {
                    Instantiate(bullets[0], bulletChilds[i].transform);
                }
            }

            if (gm.PlayerWeapon[1] == true)
            {
                for (int i = 0; i < angle.Length; i++)
                {
                   Vector2 dir = new Vector2(Mathf.Cos(angle[i]),Mathf.Sin(angle[i]));
                    //弾インスタンスを取得し、初速と発射角度を与える
                    GameObject bullet_obj = (GameObject)Instantiate(bullets[1],bulletChilds[i].transform.position , transform.rotation);
                    LaserBullet bullet_sc = bullet_obj.GetComponent<LaserBullet>();
                    bullet_sc.Velocity = velocity;
                    bullet_sc.Angle = dir;
                }
            }

            if(gm.PlayerWeapon[2] == true)
            {
                for (int i = 0; i < bulletChilds.Length; i++)
                {
                    Instantiate(bullets[2], bulletChilds[i].transform);
                }
            }

            if(gm.PlayerWeapon[3] == true)
            {

            }

            time = 0;
        }
    }
}
