using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static PlayerBulletPool;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
    [SerializeField,Header("�u�[�������̔��ˊp�x")]
    private float[] boomerangAngle;
    //�u�[�������̋A���ė���p�x
    private float[] boomerangReveseAngle;

    float PI = Mathf.PI;

    //���Ԍv��
    private float time;

    private TotalGM gm;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
        player = FindObjectOfType<Player>();

        //�����̎q�I�u�W�F�N�g���擾
        int childCount = this.gameObject.transform.childCount;
        for(int i = 0; i < childCount-1; i++)
        {
           Transform childTransform = this.gameObject.transform.GetChild(i);
           bulletChilds[i] = childTransform.gameObject;
           //Debug.Log(bulletChilds[i]);
        }

        //�p�x�����W�A���ɕϊ�
        for(int i = 0; i < laserAngle.Length; i++)
        {
            laserAngle[i] = laserAngle[i] * Mathf.Deg2Rad;
        }

        for(int i = 0; i < boomerangAngle.Length; i++)
        {
            boomerangAngle[i] = boomerangAngle[i] * Mathf.Deg2Rad;
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
                for (int i = 0; i < 1; /*laserAngle.Length*/ i++)
                {
                    Vector3 dir = new Vector2(Mathf.Cos(laserAngle[i]),Mathf.Sin(laserAngle[i]));
                    dir.z = 0;
                    //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
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
                for (int i = 0; i < boomerangAngle.Length; i++)
                {
                    if(boomerangAngle[i] > 0 || boomerangAngle[i] < 0)
                    {
                        boomerangReveseAngle[i] = boomerangAngle[i] * -1;
                    }
                    else if(boomerangAngle[i] == 0)
                    {
                        boomerangReveseAngle[i] = boomerangAngle[i];
                    }

                    Vector3 dir = new Vector2(Mathf.Cos(boomerangAngle[i]), Mathf.Sin(boomerangAngle[i]));
                    dir.z = 0;
                    Vector3 reveseDir = new Vector2(Mathf.Cos(boomerangReveseAngle[i]),Mathf.Sin(boomerangReveseAngle[i]));
                    reveseDir.z = 0;
                    //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
                    GameObject bullet_obj = (GameObject)Instantiate(bullets[3], bulletChilds[i].transform.position, transform.rotation);
                    BoomerangBullet bullet_sc = bullet_obj.GetComponent<BoomerangBullet>();
                    if (player.PBaffSkillFlag == true)
                        bullet_sc.Velocity = boomerangVelocity * 1.5f;
                    else
                        bullet_sc.Velocity = boomerangVelocity;
                    bullet_sc.Angle = dir;
                    bullet_sc.ReverseAngle = reveseDir;
                }
            }

            time = 0;
        }

        if(player.DebuffSkillFlag == true)
        {
            int childCount = this.gameObject.transform.childCount - 1;
            Transform v = this.gameObject.transform.GetChild(childCount);
            GameObject v2 = v.gameObject;
            Instantiate(skillBullet,v2.transform.position,Quaternion.identity);
            player.DebuffSkillFlag = false;
        }
    }
}
