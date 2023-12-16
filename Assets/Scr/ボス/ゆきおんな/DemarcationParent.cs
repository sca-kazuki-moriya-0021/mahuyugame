using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using DG.Tweening;

public class DemarcationParent : MonoBehaviour
{
    [Tooltip("��]���x(deg/sec)"), SerializeField] private float rotateSpeed;
    [Tooltip("�~�̊g�呬�x(uu/sec)"), SerializeField] private float radiusSpeed;
    [Tooltip("�J�n���a"), SerializeField] private float startRaduis;
    [Tooltip("�ő唼�a"), SerializeField] private float maxRaduis;
    [Tooltip("�e��Prefab"), SerializeField] private GameObject bulletPrefab;
    [Tooltip("�e�̐�"), SerializeField] private int bulletCount;

    private float sign;

    private List<GameObject> bullets;
    private float nowRadius;            //  ���݂̔��a
    private float bulletDelta;          //  �e�̊Ԋu
    private float radiusDelta;          //  ���a�̈ړ����x(uu/sec)

    public float Sign
    {
        get { return this.sign; }
        set { this.sign = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        nowRadius = startRaduis;

        //  Delta�̐ݒ�(�e�̊Ԋu�E���a�̑��x(�t���[���P��))
        bulletDelta = (360.0f / bulletCount);
        radiusDelta = (maxRaduis - nowRadius) / radiusSpeed;

        //  �e�̐���
        bullets = new List<GameObject>();
        for (var i = 0; i < bulletCount; i++)
        {
            var pos = CalcBulletPositon(nowRadius, bulletDelta * i);
            var bullet = Instantiate(bulletPrefab, pos, quaternion.identity, transform);
            bullets.Add(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  �~�̔��a���L����
        nowRadius += (radiusDelta * Time.deltaTime);
        if (nowRadius > maxRaduis)
        {
            nowRadius = maxRaduis;
        }

        //  �e�̈ʒu���X�V����(���[�J�����W�̕ύX)
        for (var i = 0; i < bullets.Count; i++)
        {
            var pos = CalcBulletPositon(nowRadius, bulletDelta * i);
            bullets[i].transform.localPosition = pos;
        }

        //  �e����]������
        transform.Rotate(new Vector3(0.0f, 0.0f, 360.0f * sign *(Time.deltaTime / rotateSpeed)));
    }

    //  �e�̈ʒu���v�Z����
    private Vector2 CalcBulletPositon(float radius, float angle)
    {
        var direction = Quaternion.Euler(0.0f, 0.0f, angle) * Vector2.up;
        direction *= radius;

        return new Vector2(direction.x, direction.y);
    }
}
