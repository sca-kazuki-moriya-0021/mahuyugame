using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyBulletCon : MonoBehaviour
{
    //�e�I�u�W�F�N�g
    [SerializeField]
    private GameObject[] bullets;
    //�e�̃X�s�[�h
    [SerializeField]
    private float[] bulletSpeed;
    //�e�̔��ˊp�x
    [SerializeField, Range(0, 360)]
    private float[] launchAngle;
    //�e�̔��ˊ��o
    [SerializeField]
    private float fireTime;

    private float time = 0;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > fireTime)
        {
            ShootBulletWithCustomDirection(count);
            ShootBulletWithCustomDirection(-count);
            count += 1;
            time = 0;
        }

        if(time > 120)
        {

        }


    }

    private void ShootBulletWithCustomDirection(int i)
    {
        // �e�𐶐����āA�����ʒu��ݒ�
        GameObject bullet = Instantiate(bullets[0], transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(i),Mathf.Sin(i));
        //Debug.Log(count);

        // �e�̌������J�X�^�}�C�Y���邽�߂ɁA�e�̊p�x��ύX
       // bullet.transform.rotation = Quaternion.Euler(0, 0, launchAngle[0] + i);

        // �e�̑��x�x�N�g����ݒ肵�āA�w�肳�ꂽ�p�x�ɔ�΂�
        //var bulletDirection = Quaternion.Euler(0, 0, launchAngle[0] + i);
        rb.velocity = dir * bulletSpeed[0];
    }
}
