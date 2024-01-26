using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GaoukenObjectCon : MonoBehaviour
{
    //�e�̃v���n�u
    [SerializeField]
    private GameObject bullet;

    //Way�e�̔��ˊp�x
    [SerializeField]
    private float[] launchWayAngle;

    //�X�s�[�h
    [SerializeField]
    private float bulletSpeed;

    //�����̈ʒu�ۑ��p
    private Vector3 pos;

    //�O�������W�ۑ��p
    private List<Vector3> oldPos = new List<Vector3>();

    //Tween�ۑ��p
    private Tween tween = null;

    //���ʎq
    private int identifier;

    public int Identifier
    {
        get { return this.identifier; }
        set { this.identifier = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //�p�x�����W�A���ɕϊ�
        for (int i = 0; i < launchWayAngle.Length; i++)
        {
            launchWayAngle[i] = launchWayAngle[i] * Mathf.Deg2Rad;
        }

        pos = transform.position;

        //Tween�œ������A����Tween��ۑ�����
        switch (identifier)
        {
            //�ォ�牺
            case 0:
                tween = this.transform.DOMove(new Vector3(0, -5, 0), 3f);
                break;
            case 1:
                tween = this.transform.DOMove(new Vector3(0,2.6f,0), 3f);
                break;
            case 2:
                tween = this.transform.DOMove(new Vector3(9,-5,0),3f);
                break;
            case 3:
                tween = this.transform.DOMove(new Vector3(-9,-5,0), 3f);
                break;
            case 4:
                tween = this.transform.DOMove(new Vector3(-9, 3, 0),3f);
                break;
            case 5:
                tween = this.transform.DOMove(new Vector3(9, 3, 0),3f);
                break;
        }
        //�Đ�
        tween.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //�O�ɕۑ��������W�ƍ��̍��W���r���āA1���傫���Ƃ���List�ɂ��̍��W��ۑ�
        var t = transform.position;
        if(Vector3.Distance(t,pos) >= 1)
        {
            oldPos.Add(t);
            pos = t;
        }

        //Tween���I�������R���[�`������
        tween.OnComplete(() => {StartCoroutine(BulletIns());});

        //  �e����]������
        transform.Rotate(new Vector3(0.0f, 0.0f, 360.0f * (Time.deltaTime / 2f)));
    }

    //�ۑ��������W����e���o��
    private IEnumerator BulletIns()
    {
       for (int x = 0; x < oldPos.Count; x++)
       {
          for (int i = 0; i < launchWayAngle.Length; i++)
          {
            Vector3 dir = new Vector2(Mathf.Cos(launchWayAngle[i]), Mathf.Sin(launchWayAngle[i]));
            dir.z = 0;
            GameObject bullet_obj = (GameObject)Instantiate(bullet, oldPos[x], Quaternion.identity);
            GaoukenBullet gaoukenBullet = bullet_obj.GetComponent<GaoukenBullet>();
            gaoukenBullet.Dir = dir;
            gaoukenBullet.Speed = 1.5f;
          }
          yield return new WaitForSeconds(0.2f);
       }
        Destroy(this.gameObject);
        StopCoroutine(BulletIns());
    }
}
