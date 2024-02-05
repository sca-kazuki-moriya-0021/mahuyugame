using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class TotalGM : MonoBehaviour
{
    public static TotalGM instance;

    //�n�C�X�R�A�ƍ��̃X�R�A
    private float[] nowScore = new float[]{0,0,0};
    private float[] highScore = new float[] { 0, 0, 0 };

    //�v���C���[���p���p
    //�X�L���͂���Ȃ��\�����邯�ǈꉞ
    private int[] playerHp = new int[]{3,0};
    private bool[] playerWeapon = {false,false,false,false};
    private bool[] playerSubWeapon = {false,false,false,false};
    private bool[] playerSkill = new bool[]{false,false,false,false};
    private bool[] playerSubSkill = new bool[]{false,false,false,false};

    private float[] skillCoolTimeCount = new float[] { 20, 20, 10, 15, 0, 0, 0, 0 };

    //�Q�[���I�[�o�[�ɂȂ�����
    private int gameOverCount = 0;
    //���ʂɂ������ǂ����̃t���O
    private bool backSideFlag = false;
    //�v���C���[�����񂾎��̍��W�擾
    private Vector2 playerTransform;
    //�X�L���J�b�g�C�����邩�ǂ���
    private bool cutinWhetherFlag = true;
    //���C�g�_�ł��邩�ǂ���
    private bool lightBlinkingFlag = true;


    #region//�X�e�[�W�Ǘ�

    //�X�e�[�W�Ǘ�
    public enum StageCon
    {
        No,
        Title = 0,
        SkillSelect,
        First,
        Secound,
        Thead,
        GameOver,
        Clear,
    }

    //���݁E�O�E�N���A���ɖ߂鎞�Ɏg���ϐ�
    private StageCon scene;
    private StageCon backScene;
    private StageCon clearBackScene;

    private Dictionary<string, StageCon> sceneDic = new Dictionary<string, StageCon>()
    {
        {"Title",StageCon.Title },
        {"SkillSelect" ,StageCon.SkillSelect},
        {"Stage",StageCon.First },
        {"SecondStage",StageCon.Secound },
        {"TheadStage",StageCon.Secound },
        {"GameOver",StageCon.GameOver },
        {"Clear",StageCon.Clear },
    };

    #endregion

    #region//�Q�b�^�[���Z�b�^�[
    public StageCon Scene {
        get { return this.scene; }
    }

    public StageCon BackScene {
        get { return this.backScene; }
        set { this.backScene = value; }
    }

    public StageCon ClearBackScene {
        get { return this.clearBackScene; }
        set { this.clearBackScene = value; }
    }

    public Dictionary<string, StageCon> SceneDic {
        get { return this.sceneDic; }
        set { this.sceneDic = value; }
    }

    public int[] PlayerHp {
        get { return this.playerHp; }
        set { this.playerHp = value; }
    }

    public float[] SkillCoolTimeCount
    {
        get { return this.skillCoolTimeCount; }
        set { this.skillCoolTimeCount = value; }
    }

    public bool[] PlayerSkill {
        get { return this.playerSkill; }
        set { this.playerSkill = value; }
    }

    public bool[] PlayerSubSkill
    {
        get { return this.playerSubSkill; }
        set { this.playerSubSkill = value; }
    }

    public bool[] PlayerWeapon
    {
        get { return this.playerWeapon; }
        set { this.playerWeapon = value; }
    }

    public bool[] PlayerSubWeapon
    {
        get { return this.playerSubWeapon; }
        set { this.playerSubWeapon = value; }
    }

    public float[] NowScore {
        get { return this.nowScore; }
        set { this.nowScore = value; }
    }

    public float[] HighScore{
        get { return this.highScore; }
        set { this.highScore = value; }
    }

    public int GameOverCount
    {
        get { return this.gameOverCount; }
        set { this.gameOverCount = value; }
    }

    public Vector2 PlayerTransForm
    {
        get { return this.playerTransform; }
        set { this.playerTransform = value; }
    }

    public bool BackSideFlag
    {
        get { return this.backSideFlag; }
        set { this.backSideFlag = value; }
    }

    public bool CutinWhetherFlag { get => cutinWhetherFlag; set => cutinWhetherFlag = value; }
    public bool LightBlinkingFlag { get => lightBlinkingFlag; set => lightBlinkingFlag = value; }
    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���݂̃X�e�[�W��Ԃ�
    public StageCon MyGetScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        scene = sceneDic[sceneName];
        return scene;
    }

    // enum�̃V�[���Ŏw�肵���V�[�������[�h����
    public void MyLoadScene(StageCon scene)
    {
        SceneManager.LoadScene(sceneDic.FirstOrDefault(x => x.Value == scene).Key);
    }

    // ���݂̃V�[�����ēx���[�h����
    public void ReloadCurrentScene()
    {
        StageCon scene = MyGetScene();
        MyLoadScene(scene);
    }

    //�O�̃V�[���ɖ߂鎞�Ɏg��
    public void ReloadClearScene()
    {
        MyLoadScene(backScene);
    }

    //�N���A����X�e�[�W�ɖ߂鎞�Ɏg��
    //�g��Ȃ��Ǝv�����ǎc���Ă���
    public void ClearBack()
    {
        MyLoadScene(clearBackScene);
    }
}
