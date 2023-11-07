using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerBulletPool;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerBulletCon : MonoBehaviour
{
    //�e�ۑ��p
    [SerializeField]
    private GameObject[] bullets;
    //�e�̔��ˈʒu
    private GameObject[] bulletChilds = new GameObject[]{null,null,null};

    //���[�U�[���˂̃X�N���v�g
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float[] angle;

    float PI = Mathf.PI;

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
        for(int i = 0; i < childCount; i++)
        {
           Transform childTransform = this.gameObject.transform.GetChild(i);
           bulletChilds[i] = childTransform.gameObject;
           Debug.Log(bulletChilds[i]);
        }

        //�p�x�����W�A���ɕϊ�
        for(int i = 0; i < angle.Length; i++)
        {
            angle[i] = angle[i] * Mathf.Deg2Rad;

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
                    Instantiate(bullets[0], bulletChilds[i].transform);
                }
            }

            if (gm.PlayerWeapon[1] == true)
            {
                for (int i = 0; i < angle.Length; i++)
                {
                    Vector3 dir = new Vector2(Mathf.Cos(angle[i]),Mathf.Sin(angle[i]));
                    dir.z = 0;
                    //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
                    GameObject bullet_obj = (GameObject)Instantiate(bullets[1],bulletChilds[i].transform.position , transform.rotation);
                    LaserBullet bullet_sc = bullet_obj.GetComponent<LaserBullet>();
                    if (player.PBaffSkillFlag == true)
                        bullet_sc.Velocity = velocity * 1.5f;
                    else
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
