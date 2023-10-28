using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    GameObject skill_1Prefab;
    [SerializeField]
    GameObject bulletSpawn;
    [SerializeField]
    GameObject skill_2Prefab;
    private float angle;
    private Vector3 startPos;
    private float elapsedTime = 0.0f;

    public float speed = 2.0f; // �ړ����x
    public float amplitudeX = 3.0f; // X���̐U��
    public float amplitudeY = 1.0f; // Y���̐U��
    public float skill1Duration = 2.0f; // �X�L��1�̏I������
    public float danmakuDuration = 2.0f; // �e���̎�������
    public float skill2Duration = 5.0f; // �X�L��2�̎�������

    private bool skillFrag = false;
    int count = 0;
    void Start()
    {
        //GameObject Spawn = GameObject.Find("BulletSpawn");
        startPos = transform.position;
        
    }

    void Update()
    {
        StartCoroutine(StartSkill());
        elapsedTime += Time.deltaTime;
        angle += Time.deltaTime * speed;
        float x = startPos.x + Mathf.Sin(angle * 2) * amplitudeX;
        float y = startPos.y + Mathf.Sin(angle) * amplitudeY;
        // Z���̈ʒu�͌Œ�i2D��ԂɌŒ�j
        transform.position = new Vector3(x, y, 0);
        if(elapsedTime >= danmakuDuration)
        {
            Debug.Log(skillFrag);
            skillFrag = true;
            elapsedTime = 0;
            count++;
        }else if (elapsedTime >= skill1Duration)
        {
            skillFrag = true;
        }
    }

    private IEnumerator StartSkill()
    {
        if (skillFrag == true)
        {
            Debug.Log(count);
            Instantiate(skill_1Prefab, this.transform.position, Quaternion.identity);
            skillFrag = false;
            yield return new WaitForSeconds(1.0f);
        }
         if (skillFrag == true && count == 1)
         {
            Instantiate(skill_2Prefab, this.transform.position, Quaternion.identity);
            skillFrag = false;
            yield return new WaitForSeconds(20.0f);
        }
    }
        //�X�^�[�g���ʏ�e�����X�L��(���[�v)
       
}
