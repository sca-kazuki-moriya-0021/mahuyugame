using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float degree;
    [SerializeField]
    private float angle_split;

    private float theta;
    float PI = Mathf.PI;

    private float time;
   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 5f)
        {
            for (int i = 0; i <= (angle_split - 1); i++)
            {
                //n-way弾の端から端までの角度
                float AngleRange = PI * (degree / 180);

                //弾インスタンスに渡す角度の計算
                if (angle_split > 1) theta = (AngleRange / (angle_split - 1)) * i - 0.5f * AngleRange;
                else theta = 0;

                //弾インスタンスを取得し、初速と発射角度を与える
                GameObject bullet_obj = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
                LaserBullet bullet_sc = bullet_obj.GetComponent<LaserBullet>();
                bullet_sc.Theta = theta;
                bullet_sc.Velocity = velocity;
            }
            time = 0f;
        }
      
    }

   
}
