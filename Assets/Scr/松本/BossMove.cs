using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // �ړ����x
    [SerializeField] private float amplitudeX = 3.0f; // X���̐U��
    [SerializeField] private float amplitudeY = 1.0f; // Y���̐U��
    [SerializeField] private GameObject[] skillPrefabs; // �X�L���̒e���̃v���n�u�z��
    [SerializeField] private float skillSwitchInterval = 30.0f; // �X�L���؂�ւ��̊Ԋu�i�b�j
    [SerializeField] private GameObject normalBulletPrefab;
    private float skillSwitchTimer = 0.0f; //�X�L���o�ߎ���
    private int currentSkillIndex = 0; // ���݂̃X�L���̃C���f�b�N�X
    private GameObject skillInstance;
    private GameObject normalPrefab;
    private float angle;
    private Vector3 startPos;

    //�v���C���[�擾
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        startPos = transform.position;
        // �ʏ�e�����������������ɒǉ�
        normalPrefab = Instantiate(normalBulletPrefab, transform.position, Quaternion.identity);
        normalPrefab.transform.SetParent(transform);
    }

    void Update()
    {
        //�v���C���[�̈ړ���~�X�L�����������Ă��Ȃ��������͓���
        if(player.BussMoveStopFlag == false)
        {
            angle += Time.deltaTime * speed;
            float x = startPos.x + Mathf.Sin(angle * 2) * amplitudeX;
            float y = startPos.y + Mathf.Sin(angle) * amplitudeY;
            // Z���̈ʒu�͌Œ�i2D��ԂɌŒ�j
            transform.position = new Vector3(x, y, 0);
        }
        skillSwitchTimer += Time.deltaTime;
        // �X�L���؂�ւ��̃^�C�~���O���Ǘ�
        if (skillSwitchTimer >= skillSwitchInterval)
        {
            SwitchSkill();
            skillSwitchTimer = 0.0f;
        }
    }

    // �X�L����؂�ւ��郁�\�b�h
    private void SwitchSkill()
    {
        // ���݂̃X�L����j��
        DestroyCurrentSkill();

        // ���̃X�L���ɐ؂�ւ�
        currentSkillIndex = (currentSkillIndex + 1) % skillPrefabs.Length;

        // �V�����X�L���𐶐�
        InstantiateSkill();
    }

    // ���݂̃X�L���𐶐�
    private void InstantiateSkill()
    {
        GameObject skillPrefab = skillPrefabs[currentSkillIndex];
        // �X�L���̐����Ə������������Ɏ���
        skillInstance = Instantiate(skillPrefab, transform.position, Quaternion.identity);
        skillInstance.transform.SetParent(transform);
    }

    // ���݂̃X�L����j��
    private void DestroyCurrentSkill()
    {
        if (skillInstance != null)
        {
            Destroy(skillInstance);
        }
    }

}
