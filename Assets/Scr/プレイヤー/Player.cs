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

    //��ʓ�
    [SerializeField]
    private GameObject screenWithin;
    private GameObject[] screenWithinChird =new GameObject[2] {null,null};


    //�g���悤
    //private PlayerBulletPool pBulletPool;
    private TotalGM gm;
    private SkillDisplay_Stage skillDisplay;
    private PouseCon pouseCon;

    //�ᑬ�{�^����������Ă��邩�ǂ���
    private bool decelerationFlag = false;

    //�X�L���g�������Ɏg�p����t���O
    private bool[] skillAtkFlag = new bool[]{false,false };

    //�ړ���~�p�t���O
    private bool bossMoveStopFlag;
    //���܏����p�̃t���O
    private bool bulletSeverFlag;
    //�v���C���[�L�����̃o�t���g�p���̃t���O
    private bool pBaffSkillFlag = false;
    private float pBaffSkillTime;

    //�{�^���������ꂽ�Ƃ��Ɏg���t���O
    private bool jKey;
    private bool buttonPish = false;

    //�g�����͒m���
    private IEnumerator coroutine;
 
    //private Image[] pImage;

    public bool[] SkillAtkFlag
    {
        get { return this.skillAtkFlag; }
        set { this.skillAtkFlag = value; }
    }

    public bool BussMoveStopFlag
    {
        get { return this.bossMoveStopFlag; }
        set { this.bossMoveStopFlag = value; }
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

    public IEnumerator Coroutine
    {
        get { return this.coroutine; }
        set { this.coroutine = value; }
    }

    private void OnEnable()
    {
        gm = FindObjectOfType<TotalGM>();
        skillDisplay = FindObjectOfType<SkillDisplay_Stage>();
        pouseCon = FindObjectOfType<PouseCon>();
        //pBulletPool = FindObjectOfType<PlayerBulletPool>();
 
        gm.PlayerWeapon[1] = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //pBulletPool.CreatePool(10);
        for(int i = 0;i < screenWithinChird.Length;i++)
        {
            screenWithinChird[i] = screenWithin.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        //�X�L�����s
        if (buttonPish && (skillAtkFlag[0] || skillAtkFlag[1]))
        {
            buttonPish = false;
            //coroutine = SkillAtk();
            StartCoroutine(SkillAtk());
        }
        //�o�t�X�L����
        if(pBaffSkillFlag == true)
        {
            pBaffSkillTime += Time.deltaTime;
            if(pBaffSkillTime > 10)
            {
                pBaffSkillTime = 0;
                pBaffSkillFlag = false;
            }
        }
        //�ړ��p�֐�
        InputSystemMove();
    }

    //WASD�ړ��̓��͎擾
    public void OnMove(InputAction.CallbackContext context)
    {
        ///Debug.Log(context);
        inputV = context.ReadValue<Vector2>();
    }

    //�X�L��1�����g���K�[
    public void OnFirstSkill(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if(skillAtkFlag[0] == false && skillDisplay.SkillCoolFlag[0] == false)
        {
            jKey = true;
            skillAtkFlag[0] = true;
            buttonPish = true;
        }
    }

    //�X�L��2�����g���K�[
    public void OnSecondSkill(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (skillAtkFlag[1] == false && skillDisplay.SkillCoolFlag[1] ==false)
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
            case InputActionPhase.Performed:
                // �{�^���������ꂽ���̏���
                decelerationFlag = true;
                break;

            case InputActionPhase.Canceled:
                // �{�^���������ꂽ���̏���
                decelerationFlag = false;
                break;
        }
    }

    //�ړ��p�֐�
    public void InputSystemMove()
    {  

        if (screenWithinChird[0].transform.position.x  <= transform.position.x &&
            screenWithinChird[1].transform.position.x  >= transform.position.x &&
            screenWithinChird[0].transform.position.y >= transform.position.y &&
            screenWithinChird[1].transform.position.y <= transform.position.y)
        {
            //�ᑬ�ړ�
            if (decelerationFlag == true) 
               transform.Translate(inputV.x * Time.deltaTime * 2f, inputV.y * Time.deltaTime * 2f, 0);
            else
               transform.Translate(inputV.x * Time.deltaTime * 5f, inputV.y * Time.deltaTime * 5f, 0);
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
        if(jKey)
        {
            jKey = false;
            //�L�����p�X�Ȃ�A�A�j���[�V�����ŃX�L���J�b�g�C���N��
            for (int i = 0; i < gm.PlayerSkill.Length -1; i++)
            {
                if (gm.PlayerSkill[i] == true)
                {
                    if(i == 0)
                    {
                        Debug.Log("������");
                        bossMoveStopFlag = true;
                    }
                    else if (i == 1)
                    {
                        Debug.Log("haita");
                        bulletSeverFlag =true;
                        yield return new WaitForSeconds(1f);
                        bulletSeverFlag = false;
                    }
                    else if(i == 2)
                    {

                    }
                    yield return new WaitForSeconds(3.0f);
                    skillDisplay.SkillCoolFlag[0] = true;
                    break;
                }
            }
        }
        else
        {
            //�L�����p�X�Ȃ�A�A�j���[�V�����ŃX�L���J�b�g�C���N��
            for (int i = 3; i > 0; i--)
            {
                if (gm.PlayerSkill[i] == true)
                {
                    //pImage[x].enabled = true;
                    if (i == 3)
                    {
                        pBaffSkillFlag = true;
                    }
                    else if(i == 2)
                    {
                        
                    }
                    else if(i == 1)
                    {
                        Debug.Log(i);
                        bulletSeverFlag = true;
                        yield return new WaitForSeconds(1f);
                        bulletSeverFlag = false;
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
