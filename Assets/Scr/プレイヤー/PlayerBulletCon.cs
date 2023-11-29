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

    //�e�̔��ˈʒu
    private GameObject[] bulletChilds = new GameObject[]{null,null,null};

    //���[�U�[�e�ݒ�p�ϐ�
    [SerializeField, Header("���[�U�[�̑��x")]
    private float laserVelocity;
    [SerializeField,Header("���[�U�[�̔��ˊp�x")]
    private float[] laserAngle;

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


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
        player = FindObjectOfType<Player>();
        playerCollider = FindObjectOfType<PlayerCollider>();

        //�����̎q�I�u�W�F�N�g���擾
        int childCount = this.gameObject.transform.childCount;
        for(int i = 0; i < childCount-1; i++)
        {
           Transform childTransform = this.gameObject.transform.GetChild(i);
           bulletChilds[i] = childTransform.gameObject;
        }

        //�p�x�����W�A���ɕϊ�
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

        //��莞�Ԃ��v���C���[������łȂ�������
        if(time > 0.5 && playerCollider.DeathFlag == false)
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
            GameObject bullet_obj =(GameObject)Instantiate(skillBullet,v2.transform.position,Quaternion.identity);
            PlayerSkillBulletCon bullet_sc = bullet_obj.GetComponent<PlayerSkillBulletCon>();
            player.DebuffSkillFlag = false;
        }
    }

    private void MainBullet()
    {
        //�I�����ꂽ0�Ԗڂ̒e�𔭎˂���
        if (gm.PlayerWeapon[0] == true)
           Weapon0();
        //�I�����ꂽ1�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerWeapon[1] == true)
           Weapon1();
        //�I�����ꂽ2�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerWeapon[2] == true)
           Weapon2();
        //�I�����ꂽ3�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerWeapon[3] == true)
           Weapon3();
    }

    private void SubBullet()
    {
        if (gm.PlayerSubWeapon[0] == true)
            Weapon0();
        //�I�����ꂽ1�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerSubWeapon[1] == true)
            Weapon1();
        //�I�����ꂽ2�Ԗڂ̒e�𔭎˂���
        else if (gm.PlayerSubWeapon[2] == true)
            Weapon2();
        //�I�����ꂽ3�Ԗڂ̒e�𔭎˂���
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
            //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
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
            //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
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
