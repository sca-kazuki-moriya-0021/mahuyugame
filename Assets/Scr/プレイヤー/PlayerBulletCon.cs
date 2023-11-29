using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private PlayerCollider playerCollider;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
        player = FindObjectOfType<Player>();
        playerCollider = FindObjectOfType<PlayerCollider>();

        //自分の子オブジェクトを取得
        int childCount = this.gameObject.transform.childCount;
        for(int i = 0; i < childCount-1; i++)
        {
           Transform childTransform = this.gameObject.transform.GetChild(i);
           bulletChilds[i] = childTransform.gameObject;
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
        if (player.PBaffSkillFlag == true)
            time += 0.0001f;

        //一定時間かつプレイヤーが死んでなかったら
        if(time > 0.5 && playerCollider.DeathFlag == false)
        {
            MainBullet();
            SubBullet();
            time = 0;
        }
        //スキル弾幕用
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

    private void MainBullet()
    {
        //選択された0番目の弾を発射する
        if (gm.PlayerWeapon[0] == true)
           Weapon0();
        //選択された1番目の弾を発射する
        else if (gm.PlayerWeapon[1] == true)
           Weapon1();
        //選択された2番目の弾を発射する
        else if (gm.PlayerWeapon[2] == true)
           Weapon2();
        //選択された3番目の弾を発射する
        else if (gm.PlayerWeapon[3] == true)
           Weapon3();
    }

    private void SubBullet()
    {
        if (gm.PlayerSubWeapon[0] == true)
            Weapon0();
        //選択された1番目の弾を発射する
        else if (gm.PlayerSubWeapon[1] == true)
            Weapon1();
        //選択された2番目の弾を発射する
        else if (gm.PlayerSubWeapon[2] == true)
            Weapon2();
        //選択された3番目の弾を発射する
        else if (gm.PlayerSubWeapon[3] == true)
            Weapon3();
    }

    private void Weapon0()
    {
        for (int i = 0; i < bulletChilds.Length; i++)
        {
            Instantiate(bullets[0], bulletChilds[i].transform.position, Quaternion.identity);
        }
    }

    private void Weapon1()
    {
        for (int i = 0; i < laserAngle.Length; i++)
        {
            Vector3 dir = new Vector2(Mathf.Cos(laserAngle[i]), Mathf.Sin(laserAngle[i]));
            dir.z = 0;
            //弾インスタンスを取得し、初速と発射角度を与える
            GameObject bullet_obj = (GameObject)Instantiate(bullets[1], bulletChilds[i].transform.position, transform.rotation);
            LaserBullet bullet_sc = bullet_obj.GetComponent<LaserBullet>();
            if (player.PBaffSkillFlag == true)
                bullet_sc.Velocity = laserVelocity * 1.5f;
            else
                bullet_sc.Velocity = laserVelocity;
            bullet_sc.Angle = dir;
        }
    }

    private void Weapon2()
    {
        for (int i = 0; i < bulletChilds.Length; i++)
        {
            Instantiate(bullets[2], bulletChilds[i].transform.position, Quaternion.identity);
        }
    }

    private void Weapon3()
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
}
