using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCon : MonoBehaviour
{
    //�e�ۑ��p
    [SerializeField]
    private GameObject[] bullets;

    //�X�L���e�ۑ��p
    [SerializeField]
    private GameObject skillBullet;

    //���C���e�̔��ˈʒu
    [SerializeField,Header("���C���e���ˈʒu")]
    private GameObject mainBulletPos;
    private GameObject[] mainBulletPosChird = new GameObject[3] {null,null,null};
    //�T�u�e�̔��ˈʒu
    [SerializeField, Header("�T�u�e���ˈʒu")]
    private GameObject subBulletPos;
    private GameObject[] subBulletPosChird = new GameObject[2]{ null,null};


    //���[�U�[�e�ݒ�p�ϐ�
    [SerializeField, Header("���[�U�[�̑��x")]
    private float laserVelocity;
    [SerializeField,Header("���[�U�[�̔��ˊp�x")]
    private float[] laserAngle;
    [SerializeField, Header("�T�u���[�U�[�̔��ˊp�x")]
    private float[] subLaserAngle;

    //�u�[�������e�ݒ�p�ϐ�
    [SerializeField,Header("�u�[�������̑��x")]
    private float boomerangVelocity;
    //�u�[�������̔��ˊp�x
    //[SerializeField,Header("�u�[�������̔��ˊp�x")]
    //private float[] boomerangAngle;
    [SerializeField,Header("�u�[�������̒��Ԉʒu")]
    private GameObject boomerangPoint;

    float PI = Mathf.PI;

    //���Ԍv��
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

        //�q�I�u�W�F�N�g���擾
        for(int i = 0; i < 3; i++)
        {
            mainBulletPosChird[i] = mainBulletPos.transform.GetChild(i).gameObject;
        }
          
        for(int i = 0; i < 2; i++)
        {
            subBulletPosChird[i] = subBulletPos.transform.GetChild(i).gameObject;
        }
        
        //�p�x�����W�A���ɕϊ�
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

        //��莞�Ԃ��v���C���[������łȂ�������
        if(time > 0.5 && playerCollider.DeathFlag == false && bossCollder.BossDeathFlag == false)
        {
            MainBullet();
            SubBullet();
            time = 0;
        }
        //�X�L���e���p
        if(player.DebuffSkillFlag == true)
        {
            int childCount = this.gameObject.transform.childCount - 1;
            Transform v = this.gameObject.transform.GetChild(childCount);
            GameObject v2 = v.gameObject;
            Instantiate(skillBullet,v2.transform.position,Quaternion.identity);
            player.DebuffSkillFlag = false;
        }
    }

    //���C���̔��ˈʒu
    private void MainBullet()
    {
        //�I�����ꂽ0�Ԗڂ̒e�𔭎˂���
        if (gm.PlayerWeapon[0] == true)
           Weapon0(mainBulletPosChird);
        //�I�����ꂽ1�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerWeapon[1] == true)
           Weapon1(mainBulletPosChird);
        //�I�����ꂽ2�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerWeapon[2] == true)
           Weapon2(mainBulletPosChird);
        //�I�����ꂽ3�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerWeapon[3] == true)
           Weapon3(mainBulletPosChird);
    }
    //�T�u�̔��ˈʒu
    private void SubBullet()
    {
        if (gm.PlayerSubWeapon[0] == true)
            Weapon0(subBulletPosChird);
        //�I�����ꂽ1�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerSubWeapon[1] == true)
            Weapon1(subBulletPosChird);
        //�I�����ꂽ2�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerSubWeapon[2] == true)
            Weapon2(subBulletPosChird);
        //�I�����ꂽ3�Ԗڂ̒e�𔭎˂���
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
            //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
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
            //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
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
