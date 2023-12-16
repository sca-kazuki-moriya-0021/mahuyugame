using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    //�e�ۑ��p
    [SerializeField]
    private GameObject[] bullets;

    //�e�̔��ˈʒu
    [SerializeField]
    private GameObject bulletpos;
    private GameObject[] bulletChilds = new GameObject[2] { null,  null };

    //���[�U�[�e�ݒ�p�ϐ�
    [SerializeField, Header("���[�U�[�̑��x")]
    private float laserVelocity;
    [SerializeField, Header("���[�U�[�̔��ˊp�x")]
    private float[] laserAngle;

    float PI = Mathf.PI;
    // Start is called before the first frame update
    void Start()
    {
        //�����̎q�I�u�W�F�N�g���擾
        for (int i = 0; i < 2; i++)
        { 
            bulletChilds[i] = bulletpos.transform.GetChild(i).gameObject;
        }
        //�p�x�����W�A���ɕϊ�
        for (int i = 0; i < laserAngle.Length; i++)
        {
            laserAngle[i] = laserAngle[i] * Mathf.Deg2Rad;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void a()
    {
        for (int i = 0; i < laserAngle.Length; i++)
        {
            Vector3 dir = new Vector2(Mathf.Cos(laserAngle[i]), Mathf.Sin(laserAngle[i]));
            dir.z = 0;
            GameObject bullet_obj = (GameObject)Instantiate(bullets[0], bulletChilds[i].transform.position, transform.rotation);
            LaserBulletItou bullet_sc = bullet_obj.GetComponent<LaserBulletItou>();
            bullet_sc.Velocity = laserVelocity * 1.5f;
            bullet_sc.Angle = dir;
            Debug.Log(laserAngle[i]);
        }
    }
    public void b()
    {
        Vector3 dir = new Vector2(Mathf.Cos(laserAngle[0]), Mathf.Sin(laserAngle[0]));
        dir.z = 0;
        GameObject bullet_obj = (GameObject)Instantiate(bullets[1], bulletChilds[0].transform.position, transform.rotation);
        LaserBulletKussetu bullet_sc = bullet_obj.GetComponent<LaserBulletKussetu>();
        bullet_sc.Velocity = laserVelocity * 1.5f;
        bullet_sc.Angle = dir;
    }
}
