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

    //メイン弾の発射位置
    [SerializeField,Header("メイン弾発射位置")]
    private GameObject mainBulletPos;
    private GameObject[] mainBulletPosChird = new GameObject[3] {null,null,null};
    //サブ弾の発射位置
    [SerializeField, Header("サブ弾発射位置")]
    private GameObject subBulletPos;
    private GameObject[] subBulletPosChird = new GameObject[2]{ null,null};


    //レーザー弾設定用変数
    [SerializeField, Header("レーザーの速度")]
    private float laserVelocity;
    [SerializeField,Header("レーザーの発射角度")]
    private float[] laserAngle;
    [SerializeField, Header("サブレーザーの発射角度")]
    private float[] subLaserAngle;

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
    private BossCollder bossCollder;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
        player = FindObjectOfType<Player>();
        playerCollider = FindObjectOfType<PlayerCollider>();
        bossCollder = FindObjectOfType<BossCollder>();

        //子オブジェクトを取得
        for(int i = 0; i < 3; i++)
        {
            mainBulletPosChird[i] = mainBulletPos.transform.GetChild(i).gameObject;
        }
          
        for(int i = 0; i < 2; i++)
        {
            subBulletPosChird[i] = subBulletPos.transform.GetChild(i).gameObject;
        }
        
        //角度をラジアンに変換
        for(int i = 0; i < laserAngle.Length; i++)
        {
            laserAngle[i] = laserAngle[i] * Mathf.Deg2Rad;
        }

        for(int i = 0; i< subLaserAngle.Length; i++)
        {
            subLaserAngle[i] = subLaserAngle[i] * Mathf.Deg2Rad;
        }

        gm.PlayerWeapon[0] = true;
        gm.PlayerSubWeapon[1] = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (player.PBaffSkillFlag == true)
            time += 0.0001f;

        //一定時間かつプレイヤーが死んでなかったら
        if(time > 0.5 && playerCollider.DeathFlag == false && bossCollder.BossDeathFlag == false)
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
            Instantiate(skillBullet,v2.transform.position,Quaternion.identity);
            player.DebuffSkillFlag = false;
        }
    }

    //メインの発射位置
    private void MainBullet()
    {
        //選択された0番目の弾を発射する
        if (gm.PlayerWeapon[0] == true)
           Weapon0(mainBulletPosChird);
        //選択された1番目の弾を発射する
        else if (gm.PlayerWeapon[1] == true)
           Weapon1(mainBulletPosChird);
        //選択された2番目の弾を発射する
        else if (gm.PlayerWeapon[2] == true)
           Weapon2(mainBulletPosChird);
        //選択された3番目の弾を発射する
        else if (gm.PlayerWeapon[3] == true)
           Weapon3(mainBulletPosChird);
    }
    //サブの発射位置
    private void SubBullet()
    {
        if (gm.PlayerSubWeapon[0] == true)
            Weapon0(subBulletPosChird);
        //選択された1番目の弾を発射する
        else if (gm.PlayerSubWeapon[1] == true)
            Weapon1(subBulletPosChird);
        //選択された2番目の弾を発射する
        else if (gm.PlayerSubWeapon[2] == true)
            Weapon2(subBulletPosChird);
        //選択された3番目の弾を発射する
        else if (gm.PlayerSubWeapon[3] == true)
            Weapon3(subBulletPosChird);
    }

    private void Weapon0(GameObject[]bulletPos)
    {
        for (int i = 0; i < bulletPos.Length; i++)
           Instantiate(bullets[0],bulletPos[i].transform.position, Quaternion.identity);
    }

    private void Weapon1(GameObject[] bulletPos)
    {
        GameObject[] weapen = bulletPos;
        float[] angle;
        if(weapen == mainBulletPosChird)
            angle = laserAngle;
        else
            angle = subLaserAngle;

        for (int i = 0; i < angle.Length; i++)
        {
            Vector3 dir = new Vector2(Mathf.Cos(angle[i]), Mathf.Sin(angle[i]));
            dir.z = 0;
            //弾インスタンスを取得し、初速と発射角度を与える
            GameObject bullet_obj = (GameObject)Instantiate(bullets[1], bulletPos[i].transform.position, transform.rotation);
            LaserBullet bullet_sc = bullet_obj.GetComponent<LaserBullet>();
            if (player.PBaffSkillFlag == true)
                bullet_sc.Velocity = laserVelocity * 1.5f;
            else
                bullet_sc.Velocity = laserVelocity;
            bullet_sc.Angle = dir;
        }
    }

    private void Weapon2(GameObject[] bulletPos)
    {
        for (int i = 0; i < bulletPos.Length; i++)
            Instantiate(bullets[2], bulletPos[i].transform.position, Quaternion.identity);
    }

    private void Weapon3(GameObject[] bulletPos)
    {
        for (int i = 0; i < bulletPos.Length; i++)
        {
            //弾インスタンスを取得し、初速と発射角度を与える
            var dir = boomerangPoint.transform.position - bulletPos[i].transform.position / 2;
            GameObject bullet_obj = (GameObject)Instantiate(bullets[3], bulletPos[i].transform.position, transform.rotation);
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
