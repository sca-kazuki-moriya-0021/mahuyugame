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

    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
    }

    public void CreatePool(int maxCount)
    {
        Debug.Log(gm);

        bullet_List = new List<GameObject>();
        GameObject obj = null;
        for (int x = 0; x < gm.PlayerWeapon.Length; x++)
        { 
            if(gm.PlayerWeapon[x] == true)
            {
                obj = pool_Bullets[x];
                break;
            }
        }
        for (int i = 0; i < maxCount; i++)
        {
            GameObject ins = Instantiate(obj);
            ins.SetActive(false);
            ins.transform.parent = this.gameObject.transform;
            bullet_List.Add(ins);
        }
    }

    public GameObject GetObject(Vector2 position)
    {
        for(int i = 0; i < bullet_List.Count; i++)
        {
            if(bullet_List[i].activeSelf == false)
            {
                GameObject ins = bullet_List[i];
                ins.transform.position = position;
                ins.SetActive(true);
                return ins;
            }
        }


        GameObject Obj = null;
        for (int x = 0; x < gm.PlayerWeapon.Length; x++)
        {
            if (gm.PlayerWeapon[x] == true)
                Obj = pool_Bullets[x];
            break;
        }
        GameObject newObj = Instantiate(Obj,position,Quaternion.identity);
        newObj.SetActive(false);
        newObj.transform.parent = gameObject.transform;
        bullet_List.Add(newObj);
        Debug.Log("�����Ă����");
        return newObj;
    }
}
