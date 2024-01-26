using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerMoveBullet : MonoBehaviour
{
    //�l�p�̏ꏊ
    private GameObject cornerObject;

    //�X�s�[�h
    private float speed = 10f;

    //�e�̃X�s�[�h
    private float bulletSpeed = 2f; 

    //�ڕW�n�_
    private GameObject[] cornerPosChild = new GameObject[4];

    //�����Ă��邩�ǂ���
    private bool moveColFlag;

    //�����l
    private Vector3 originPos;

    //�o���e
    [SerializeField]
    private GameObject childBullet;

    //���˃^�C��
    private float time;

    //�����Ƃ��Ɏg���p�̃^�C��
    private float deleteTime;

    //�����l
    private Vector3 initializationPos;

    public Vector3 InitializationPos 
    {
        get { return this.initializationPos; }
        set { this.initializationPos = value; }
    }

    public GameObject CornerObject
    {
        get { return this.cornerObject; }
        set { this.cornerObject = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

        //�ڕW�n�_��ݒ�
        for (int i = 0; i < cornerPosChild.Length; i++)
        {
            cornerPosChild[i] = cornerObject.transform.GetChild(i).gameObject;
            //Debug.Log(cornerPosChild[i].gameObject.transform.position);
        }

        //���ݒn������������
        transform.position = initializationPos;

        //�������W���l������
        originPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        deleteTime += Time.deltaTime;

        //�e����
        if(time > 0.3f)
        {
            ShootBullet();
            time = 0.0f;
        }

        if(deleteTime > 12.0f)
        {
            Destroy(this.gameObject);
        }

        //�ړ�����
        if (moveColFlag == false) { 
            if (cornerPosChild[0].gameObject.transform.position == this.transform.position)
                StartCoroutine(MovePosition(0, 1));
            else if (cornerPosChild[1].gameObject.transform.position == this.transform.position)
                StartCoroutine(MovePosition(1, 2));
            else if (cornerPosChild[2].gameObject.transform.position == this.transform.position)
                StartCoroutine(MovePosition(2, 3));
            else if (cornerPosChild[3].gameObject.transform.position == this.transform.position)
                StartCoroutine(MovePosition(3, 0));
        }
    }

    //�ړ�����
    private IEnumerator MovePosition(int a, int b)
    {
        moveColFlag = true;
        float time = 0;
        float dir = Mathf.Abs(Vector3.Distance(cornerPosChild[a].transform.position, cornerPosChild[b].transform.position));
     
        while (cornerPosChild[b].transform.position != transform.position)
        {
            time += Time.deltaTime;
            float pos = (time * speed) / dir;
            transform.position = Vector3.Lerp(cornerPosChild[a].transform.position, cornerPosChild[b].transform.position, pos);
            yield return null;
        }
        
        yield return new WaitForSecondsRealtime(0.2f);

        moveColFlag = false;
        StopCoroutine(MovePosition(a, b));
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(childBullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        var dir = originPos - transform.position;
        dir = dir.normalized;
        rb.velocity = dir * bulletSpeed;
    }

}
