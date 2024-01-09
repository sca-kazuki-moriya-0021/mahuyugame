using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using Spine.Unity;
using Spine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //�����擾�p
    private Vector2 inputV;
    //�ړ������p
    [SerializeField]
    private GameObject screenWithin;
    private GameObject[] screenWithinChird =new GameObject[2] {null,null};

    //�X�N���v�g�擾�p
    private TotalGM gm;
    private SkillDisplay_Stage skillDisplay;
    private PouseCon pouseCon;
    private PlayerSkillCutInCon skillCutinCon;
    private PlayerCollider playerCollider;
    private BossCollder bossCollder;

    //�X�L��������
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClips;

    [SerializeField]
    private GameObject buffEffect;

    [SerializeField]
    private Rigidbody2D rb;

    private float time;
    //�ᑬ�{�^����������Ă��邩�ǂ���
    private bool decelerationFlag = false;
    //�X�L���g�������Ɏg�p����t���O
    private bool[] skillAtkFlag = new bool[]{false,false};
    //�o���A�p�t���O
    private bool�@barrierFlag;
    private float  pBarrierTime;
    //���܏����p�̃t���O
    private bool bulletSeverFlag;
    //�v���C���[�L�����̃o�t�g�p���̃t���O
    private bool pBaffSkillFlag = false;
    private float pBaffSkillTime;
    //�f�o�t���ˎ��̃t���O
    private bool debuffSkillFlag;
    //�X�L���{�^���������ꂽ�Ƃ��Ɏg���t���O
    private bool jKey;
    private bool buttonPish = false;

    //�g�����͒m���
    private IEnumerator coroutine;
 
    public bool[] SkillAtkFlag
    {
        get { return this.skillAtkFlag; }
        set { this.skillAtkFlag = value; }
    }

    public bool PBaffSkillFlag
    {
        get { return this.pBaffSkillFlag; }
        set { this.pBaffSkillFlag = value; }
    }

    public bool BulletSeverFlag
    {
        get { return this.bulletSeverFlag; }
        set { this.bulletSeverFlag = value; }
    }

    public bool DebuffSkillFlag
    {
        get { return this.debuffSkillFlag; }
        set { this.debuffSkillFlag = value; }
    }

    public IEnumerator Coroutine
    {
        get { return this.coroutine; }
        set { this.coroutine = value; }
    }

    public bool BarrierFlag { get => barrierFlag; set => barrierFlag = value; }

    private void OnEnable()
    {
        gm = FindObjectOfType<TotalGM>();
        skillDisplay = FindObjectOfType<SkillDisplay_Stage>();
        pouseCon = FindObjectOfType<PouseCon>();
        skillCutinCon = FindObjectOfType<PlayerSkillCutInCon>();
        playerCollider = FindObjectOfType<PlayerCollider>();
        bossCollder = FindObjectOfType<BossCollder>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //�ړ������̍��W���m
        for(int i = 0;i < screenWithinChird.Length;i++)
        {
            screenWithinChird[i] = screenWithin.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        //if(pouseCon.MenuFlag == false)
        {
            //�X�L�����s
            if (buttonPish && (skillAtkFlag[0] || skillAtkFlag[1]))
            {
                buttonPish = false;
                StartCoroutine(SkillAtk());
            }
            //�o�t�X�L����
            if (pBaffSkillFlag == true)
            {
                pBaffSkillTime += Time.deltaTime;
                if (pBaffSkillTime > 7)
                {
                    Destroy(buffEffect);
                    pBaffSkillTime = 0;
                    pBaffSkillFlag = false;
                }
            }
            //�o���A���̌����
            if(barrierFlag == true)
            {
                pBarrierTime += Time.deltaTime;
                if (pBarrierTime > 5)
                {
                    //Destroy(buffEffect);
                    pBarrierTime = 0;
                    barrierFlag = false;
                }
            }
            //�ړ��p�֐�
            if (playerCollider.DeathFlag == false)
                InputSystemMove();

            //�Q�[���I�[�o�[�ɂȂ������������`���ė�����
            else if (playerCollider.DeathFlag == true)
            {
                rb.velocity = Vector3.zero;
                time += Time.deltaTime *0.1f;
                var endpos = new Vector3(transform.position.x - 5f, transform.position.y - 10f);
                var midpos = new Vector3(transform.position.x - 3f, transform.position.y - 5f);
                Vector3 a = Vector3.Lerp(transform.position, midpos, time);
                Vector3 b = Vector3.Lerp(midpos, endpos, time);
                rb.velocity = Vector3.Lerp(a, b, time);
            }
        }
    }

    //WASD�ړ��̓��͎擾
    public void OnMove(InputAction.CallbackContext context)
    {
        inputV = context.ReadValue<Vector2>();
    }

    //�|�[�Y����
    public void OnPouse(InputAction.CallbackContext context)
    {
        pouseCon.Pouse();
    }

    //�X�L��1�����g���K�[
    public void OnFirstSkill(InputAction.CallbackContext context)
    {
        if(skillAtkFlag[0] == false && skillDisplay.SkillCoolFlag[0] == false && skillCutinCon.CutInFlag == false && bossCollder.BossDeathFlag == false)
        {
            jKey = true;
            skillAtkFlag[0] = true;
            buttonPish = true;
        }
    }

    //�X�L��2�����g���K�[
    public void OnSecondSkill(InputAction.CallbackContext context)
    {
        if (skillAtkFlag[1] == false && skillDisplay.SkillCoolFlag[1] == false && skillCutinCon.CutInFlag == false && bossCollder.BossDeathFlag == false)
        {
            skillAtkFlag[1] = true;
            buttonPish = true;   
        }
    }

    //�ᑬ�ړ�
    public void OnDeceleration(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //�{�^���������ꂽ��True�E�����ꂽ��False
            case InputActionPhase.Performed:
                decelerationFlag = true;
                break;

            case InputActionPhase.Canceled:
                decelerationFlag = false;
                break;
        }
    }

    //�ړ��p�֐�
    public void InputSystemMove()
    {  �@//�ړ��ł��鎞
        if (screenWithinChird[0].transform.position.x  <= transform.position.x &&
            screenWithinChird[1].transform.position.x  >= transform.position.x &&
            screenWithinChird[0].transform.position.y >= transform.position.y &&
            screenWithinChird[1].transform.position.y <= transform.position.y)
        {
            //�ᑬ�ړ�
            if (decelerationFlag == true) 
               rb.velocity = inputV * 3f;
            else
               rb.velocity = inputV * 5f;
        }
        else if(screenWithinChird[0].transform.position.x > transform.position.x)
        {
            Vector3 a = transform.position;
            a.x = screenWithinChird[0].transform.position.x;
            transform.position = a;
        }
        else if (screenWithinChird[0].transform.position.y < transform.position.y)
        {
            Vector3 a = transform.position;
            a.y = screenWithinChird[0].transform.position.y;
            transform.position = a;
        }
        else if (screenWithinChird[1].transform.position.x < transform.position.x)
        {
            Vector3 a = transform.position;
            a.x = screenWithinChird[1].transform.position.x;
            transform.position = a;
        }
        else if (screenWithinChird[1].transform.position.y > transform.position.y)
        {
            Vector3 a = transform.position;
            a.y = screenWithinChird[1].transform.position.y;
            transform.position = a;
        }
    }

    //�X�L�������{��
    private IEnumerator SkillAtk()
    {
         audioSource.PlayOneShot(audioClips);
        //�X�L��1���g��ꂽ��
        if(jKey)
        {
            jKey = false;
            //�X�L�������ƃX�L���J�b�g�C������
            for (int i = 0; i < gm.PlayerSkill.Length -1; i++)
            {
                if (gm.PlayerSkill[i] == true)
                {
                    switch (i)
                    {
                        case 0:
                            skillCutinCon.PlayerCutInDisplay(i);
                            barrierFlag = true;
                            break;
                        case 1:
                            skillCutinCon.PlayerCutInDisplay(i);
                            bulletSeverFlag = true;
                            yield return new WaitForSeconds(1f);
                            bulletSeverFlag = false;
                            break;
                        case 2:
                            skillCutinCon.PlayerCutInDisplay(i);
                            debuffSkillFlag = true;
                            break;
                    }
                    yield return new WaitForSeconds(3.0f);
                    skillDisplay.SkillCoolFlag[0] = true;
                    break;
                }
            }
        }
        else
        {
            //�X�L�������ƃX�L���J�b�g�C������
            for (int i = gm.PlayerSkill.Length -1; i > 0; i--)
            {
                if (gm.PlayerSkill[i] == true)
                {
                    switch (i)
                    {
                        case 3:
                            skillCutinCon.PlayerCutInDisplay(i);
                            Instantiate(buffEffect,transform.position,Quaternion.identity,transform);
                            pBaffSkillFlag = true;
                            break;
                        case 2:
                            skillCutinCon.PlayerCutInDisplay(i);
                            debuffSkillFlag = true;
                            break;
                        case 1:
                            skillCutinCon.PlayerCutInDisplay(i);
                            bulletSeverFlag = true;
                            yield return new WaitForSeconds(1f);
                            bulletSeverFlag = false;
                            break;
                    }
                    yield return new WaitForSeconds(3.0f);
                    skillDisplay.SkillCoolFlag[1] = true;
                    break;
                }
            }
        }
       StopCoroutine(SkillAtk());
    }

}
