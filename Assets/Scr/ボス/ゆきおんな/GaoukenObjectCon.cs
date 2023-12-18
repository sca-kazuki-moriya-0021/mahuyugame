using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GaoukenObjectCon : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    //Way�e�̔��ˊp�x
    [SerializeField]
    private float[] launchWayAngle;

    //�X�s�[�h
    [SerializeField]
    private float bulletSpeed;

    //�p�x�����ϐ�
    private float _theta;
    //��
    float PI = Mathf.PI;

    // Start is called before the first frame update
    void Start()
    {
        //�p�x�����W�A���ɕϊ�
        for (int i = 0; i < launchWayAngle.Length; i++)
        {
            launchWayAngle[i] = launchWayAngle[i] * Mathf.Deg2Rad;
        }

        for (int i = 0; i < 3; i++)
        {
            transform.DOMove(new Vector3(-1 + i,0,0),0.1f);
            //BulletIns();
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BulletIns()
    {
        for(int i = 0; i <launchWayAngle.Length; i++)
        {
            Vector3 dir = new Vector2(Mathf.Cos(launchWayAngle[i]), Mathf.Sin(launchWayAngle[i]));
            dir.z = 0;
            //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
            GameObject bullet_obj = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody2D rigidbody2D = bullet_obj.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = dir * bulletSpeed;
        }
    }

}
