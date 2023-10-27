using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    private TotalGM gm;

    //プールしておきたい弾の種類
    [SerializeField]
    private GameObject[] pool_Bullets;

    private bool[] bulletFlag = new bool[] {false,false,false,false}; 


    //プールしたオブジェクトを入れるリスト
    List<GameObject> bullet_List = new List<GameObject>();

    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
       
        for(int i = 0; i < gm.PlayerWeapon.Length; i++){
            if (gm.PlayerWeapon[i] == true)
            {
                bulletFlag[i] = true;
            }
        }
    }

    private void Update()
    {
        
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
        for (int i = 0; i < bullet_List.Count; i++)
        {
            GameObject ins = null;
            for (int x = 0; x < gm.PlayerWeapon.Length; x++)
            {
                if (gm.PlayerWeapon[x] == true)
                {
                    ins = pool_Bullets[x];
                }
            }
             bullet_List[i] = ins;
             Debug.Log(bullet_List[i]);
            if (bullet_List[i].activeSelf == false)
            {
                Debug.Log("武器切り替え");
                bullet_List[i].transform.position = position;
                bullet_List[i].SetActive(true);
                return ins;
            }
        }

        GameObject Obj = null;
        for (int x = 0; x < gm.PlayerWeapon.Length; x++)
        {
            if (gm.PlayerWeapon[x] == true)
            {
                Obj = pool_Bullets[x];
                break;
            }
        }
        GameObject newObj = Instantiate(Obj,position,Quaternion.identity);
        newObj.SetActive(false);
        newObj.transform.parent = gameObject.transform;
        bullet_List.Add(newObj);
        Debug.Log("増えているよ");
        return newObj;
    }
}
