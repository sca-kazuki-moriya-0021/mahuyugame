using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    [SerializeField] private GameObject[] skillPrefabs; // �X�L���̒e���̃v���n�u�z��
    [SerializeField] private float skillSwitchInterval = 30.0f; // �X�L���؂�ւ��̊Ԋu�i�b�j
    [SerializeField] private GameObject normalBulletPrefab;

    private float skillSwitchTimer = 0.0f; //�X�L���o�ߎ���
    private int currentSkillIndex = 0; // ���݂̃X�L���̃C���f�b�N�X
    private float skillTimerCount = 0f;// �X�L�������^�C�~���O
    private GameObject skillInstance;
    private GameObject normalPrefab;

    private BossMove bossMove;
    // Start is called before the first frame update
    void Start()
    {
        bossMove = FindObjectOfType<BossMove>();
        normalPrefab = Instantiate(normalBulletPrefab, transform.position, Quaternion.identity);
        normalPrefab.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        skillTimerCount += Time.deltaTime;

        if (skillTimerCount >= 5f)
        {
           bossMove.BossAttack1 = true;
        }

        if(skillTimerCount >= 10f)
        {
            bossMove.BossAttack2 = true;
        }

        skillSwitchTimer += Time.deltaTime;
        // �X�L���؂�ւ��̃^�C�~���O���Ǘ�
        if (skillSwitchTimer >= skillSwitchInterval)
        {
            SwitchSkill();
            skillSwitchTimer = 0.0f;
        }
    }

    // �e����؂�ւ��郁�\�b�h
    private void SwitchSkill()
    {
        // ���݂̒e����j��
        DestroyCurrentSkill();

        // ���̒e���ɐ؂�ւ�
        currentSkillIndex = (currentSkillIndex + 1) % skillPrefabs.Length;

        // �V�����e���𐶐�
        InstantiateSkill();
    }

    // ���݂̒e���𐶐�
    private void InstantiateSkill()
    {
        GameObject skillPrefab = skillPrefabs[currentSkillIndex];
        // �X�L���̒e���Ə������������Ɏ���
        skillInstance = Instantiate(skillPrefab, transform.position, Quaternion.identity);
        skillInstance.transform.SetParent(transform);
    }

    // ���݂̒e����j��
    private void DestroyCurrentSkill()
    {
        if (skillInstance != null)
        {
            Destroy(skillInstance);
        }
    }
}
