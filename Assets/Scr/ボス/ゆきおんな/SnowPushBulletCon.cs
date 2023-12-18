using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnowPushBulletCon : MonoBehaviour
{
    //�p�x�����ϐ�
    private float _theta;
    //��
    float PI = Mathf.PI;

    //�f�B�}�[�P�C�V�����p�̐e�I�u�W�F�N�g
    [SerializeField]
    private GameObject demarcationParentObject;

    [SerializeField]
    private GameObject gaoukenObject;

    private List<GameObject> objects = new List<GameObject>();

    //�p�x�����߂Ĕ��˂���
    public void ShootBulletWithCustomDirection(int i, GameObject obj, float speed)
    {
        // �e�𐶐����āA�����ʒu��ݒ�
        GameObject bullet = Instantiate(obj, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
        rb.velocity = dir * speed;
    }

    //Way�e
    public void ShootWayBullet(int spilt, float angle, GameObject obj, float speed)
    {
        for (int i = 0; i < spilt; i++)
        {
            //n-way�e�̒[����[�܂ł̊p�x
            float AngleRange = PI * (angle / 180);
            if (AngleRange > 1) _theta = (AngleRange / (spilt)) * i + 0.5f * (PI - AngleRange);
            else _theta = 0.5f * PI;

            GameObject bullet = Instantiate(obj, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            var bulletv = new Vector2(speed * Mathf.Cos(_theta), speed * Mathf.Sin(_theta));
            rb.velocity = bulletv;
        }
    }

    //��]�e
    public void ShootBarrier(float angle, GameObject obj, float speed)
    {
        angle = angle * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(obj, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = dir * speed;
    }

    //�l���ړ��e����
    //�N�����x���[�g���b�v
    public void ShootCornerMove(GameObject cornerObj ,GameObject childObj, GameObject bulletObj,float speed)
    {
        GameObject bullet = Instantiate(bulletObj, childObj.transform.position, Quaternion.identity);
        CornerMoveBullet cornerMoveBullet = bullet.GetComponent<CornerMoveBullet>();
        cornerMoveBullet.CornerObject = cornerObj;
        cornerMoveBullet.InitializationPos = childObj.transform.position;
    }

    //�f�B�}�[�P�C�V����
    public void ShootDemarcation(float sign)
    {
        GameObject parent = Instantiate(demarcationParentObject,transform.position,quaternion.identity);
        objects.Add(parent);
        DemarcationParent parent_cs = parent.GetComponent<DemarcationParent>();
        parent_cs.Sign = sign;
    }

    //�f�B�}�[�P�C�V�����ŏo�Ă����I�u�W�F�N�g�����ϐ�
    public void DestoryDemarcation()
    {
        Debug.Log("������");
        for(int i = 0; i < objects.Count; i++)
        {
            var g = objects[i];
            Destroy(g.gameObject);
        }
        objects.Clear();
    }

    //�쉤���u��S�\���̕񂢁v
    public void GaoukenShoot()
    {
        Instantiate(gaoukenObject,transform.position,quaternion.identity);
    }
}
