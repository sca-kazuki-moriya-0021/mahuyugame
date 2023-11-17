using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static PlayerBulletPool;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerBulletCon : MonoBehaviour
{
    //弾保存用
    [SerializeField]
    private GameObject[] bullets;

    //スキル弾保存用
    [SerializeField]
    private GameObject skillBullet;

    //弾の発射位置
    private GameObject[] bulletChilds = new GameObject[]{null,null,null};

    //レーザー弾設定用変数
    [SerializeField, Header("レーザーの速度")]
    private float laserVelocity;
    [SerializeField,Header("レーザーの発射角度")]
    private float[] laserAngle;

    //ブーメラン弾設定用変数
    [SerializeField,Header("ブーメランの速度")]
    private float boomerangVelocity;
    //ブーメランの発射角度
    //[SerializeField,Header("ブーメランの発射角度")]
    //private float[] boomerangAngle;
    [SerializeField,Header("ブーメランの中間位置")]
    private GameObject boomerangPoint;

    float PI = Mathf.PI;

    //時間計測
    private float time;

    private TotalGM gm;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
        player = FindObjectOfType<Player>();

        //自分の子オブジェクトを取得
        int childCount = this.gameObject.transform.childCount;
        for(int i = 0; i < childCount-1; i++)
        {
           Transform childTransform = this.gameObject.transform.GetChild(i);
           bulletChilds[i] = childTransform.gameObject;
           //Debug.Log(bulletChilds[i]);
        }

        //角度をラジアンに変換
        for(int i = 0; i < laserAngle.Length; i++)
        {
            laserAngle[i] = laserAngle[i] * Mathf.Deg2Rad;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //Debug.Log(time);
        if (player.PBaffSkillFlag == true)
            time += 0.01f;

        if(time > (6 - gm.PlayerLevel[0]))
        {
            if (gm.PlayerWeapon[0] == true)
            {
                for (int i = 0; i < bulletChilds.Length; i++)
                {
                    Instantiate(bullets[0], bulletChilds[i].transform.position, Quaternion.identity);
                }
            }
            else if (gm.PlayerWeapon[1] == true)
            {
                for (int i = 0; i < laserAngle.Length; i++)
                {
                    Vector3 dir = new Vector2(Mathf.Cos(laserAngle[i]),Mathf.Sin(laserAngle[i]));
                    dir.z = 0;
                    //弾インスタンスを取得し、初速と発射角度を与える
                    GameObject bullet_obj = (GameObject)Instantiate(bullets[1],bulletChilds[i].transform.position , transform.rotation);
                    LaserBullet bullet_sc = bullet_obj.GetComponent<LaserBullet>();
                    if (player.PBaffSkillFlag == true)
                        bullet_sc.Velocity = laserVelocity * 1.5f;
                    else
                        bullet_sc.Velocity = laserVelocity;
                    bullet_sc.Angle = dir;
                }
            }
            else if(gm.PlayerWeapon[2] == true)
            {
                for (int i = 0; i < bulletChilds.Length; i++)
                {
                    Instantiate(bullets[2], bulletChilds[i].transform.position, Quaternion.identity);
                }
            }

            else if(gm.PlayerWeapon[3] == true)
            {
                for (int i = 0; i < bulletChilds.Length; i++)
                {
                    //弾インスタンスを取得し、初速と発射角度を与える
                    var dir = boomerangPoint.transform.position - bulletChilds[i].transform.position / 2;
                    GameObject bullet_obj = (GameObject)Instantiate(bullets[3], bulletChilds[i].transform.position, transform.rotation);
                    BoomerangBullet bullet_sc = bullet_obj.GetComponent<BoomerangBullet>();
                    if (player.PBaffSkillFlag == true)
                        bullet_sc.Velocity = boomerangVelocity * 1.5f;
                    else
                        bullet_sc.Velocity = boomerangVelocity;
                    bullet_sc.Number = i;
                    bullet_sc.Angle = dir;
                    bullet_sc.EndPosition = boomerangPoint.transform.position;
                }
            }

            time = 0;
        }

        if(player.DebuffSkillFlag == true)
        {
            int childCount = this.gameObject.transform.childCount - 1;
            Transform v = this.gameObject.transform.GetChild(childCount);
            GameObject v2 = v.gameObject;
            GameObject bullet_obj =(GameObject)Instantiate(skillBullet,v2.transform.position,Quaternion.identity);
            PlayerSkillBulletCon bullet_sc = bullet_obj.GetComponent<PlayerSkillBulletCon>();
            player.DebuffSkillFlag = false;
        }
    }
}
