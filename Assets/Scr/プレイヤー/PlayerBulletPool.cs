using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    private TotalGM gm;

    //�v�[�����Ă��������e�̎��
    [SerializeField]
    private GameObject[] pool_Bullets;

    //�v�[�������I�u�W�F�N�g�����郊�X�g
    List<GameObject> bullet_List = new List<GameObject>();

    private void Start()
    {
        gm = FindObjectOfType<TotalGM>();
    }

    public GameObject poolBullet(Vector2 shotpos)
    {
        //���˂������e�����߂�
        GameObject n = null;
        for (int i = 0; i < gm.PlayerWeapon.Length; i++)
        {
            if(gm.PlayerWeapon[i] == true)
            {
                n = pool_Bullets[i];
                break;
            }
        }

        //�v�[������g���I�u�W�F�N�g�̃i���o�[�B�Ȃ���ΰ1
        int obj_No = bullet_List.FindIndex(b => b.activeSelf == false);

        if (obj_No == -1)
        {
            //���X�g�ɂȂ��Ȃ�A�V�����ǉ�
            bullet_List.Add((GameObject)Instantiate(n, shotpos, transform.rotation));

            //���X�g�ɒǉ������̂��q�I�u�W�F�N�g��
            obj_No = bullet_List.Count - 1;
            bullet_List[obj_No].transform.parent = gameObject.transform;
        }

        //List�ɂ���΂��̃I�u�W�F�N�g���A�N�e�B�u��
        else bullet_List[obj_No].SetActive(true);

        //�g���I�u�W�F�N�g��Ԃ�
        return bullet_List[obj_No];
    }
}
