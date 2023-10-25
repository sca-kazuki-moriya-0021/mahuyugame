using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    private TotalGM gm;

    //プールしておきたい弾の種類
    [SerializeField]
    private GameObject[] pool_Bullets;

    //プールしたオブジェクトを入れるリスト
    List<GameObject> bullet_List = new List<GameObject>();

    private void Start()
    {
        gm = FindObjectOfType<TotalGM>();
    }

    public GameObject poolBullet(Vector2 shotpos)
    {
        //発射したい弾を求める
        GameObject n = null;
        for (int i = 0; i < gm.PlayerWeapon.Length; i++)
        {
            if(gm.PlayerWeapon[i] == true)
            {
                n = pool_Bullets[i];
                break;
            }
        }

        //プールから使うオブジェクトのナンバー。なければｰ1
        int obj_No = bullet_List.FindIndex(b => b.activeSelf == false);

        if (obj_No == -1)
        {
            //リストにないなら、新しく追加
            bullet_List.Add((GameObject)Instantiate(n, shotpos, transform.rotation));

            //リストに追加したのを子オブジェクトに
            obj_No = bullet_List.Count - 1;
            bullet_List[obj_No].transform.parent = gameObject.transform;
        }

        //Listにあればそのオブジェクトをアクティブに
        else bullet_List[obj_No].SetActive(true);

        //使うオブジェクトを返す
        return bullet_List[obj_No];
    }
}
