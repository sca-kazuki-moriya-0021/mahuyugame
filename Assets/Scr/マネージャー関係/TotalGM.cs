using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class TotalGM : MonoBehaviour
{
    public static TotalGM instance;

    //ハイスコアと今のスコア
    private float[] nowScore = new float[]{0,0,0};
    private float[] highScore = new float[] { 0, 0, 0 };

    //プレイヤー引継ぎ用
    //スキルはいらない可能性あるけど一応
    private int[] playerHp = new int[]{3,0};
    private bool[] playerWeapon = {false,false,false,false};
    private bool[] playerSubWeapon = {false,false,false,false};
    private bool[] playerSkill = new bool[]{false,false,false,false};
    private bool[] playerSubSkill = new bool[]{false,false,false,false};

    private float[] skillCoolTimeCount = new float[] { 20, 20, 10, 15, 0, 0, 0, 0 };

    //ゲームオーバーになった回数
    private int gameOverCount = 0;
    //裏面にいくかどうかのフラグ
    private bool backSideFlag = false;
    //プレイヤーが死んだ時の座標取得
    private Vector2 playerTransform;
    //スキルカットインするかどうか
    private bool cutinWhetherFlag = true;
    //ライト点滅するかどうか
    private bool lightBlinkingFlag = true;


    #region//ステージ管理

    //ステージ管理
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

    //現在・前・クリア時に戻る時に使う変数
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

    #region//ゲッター＆セッター
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

    //現在のステージを返す
    public StageCon MyGetScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        scene = sceneDic[sceneName];
        return scene;
    }

    // enumのシーンで指定したシーンをロードする
    public void MyLoadScene(StageCon scene)
    {
        SceneManager.LoadScene(sceneDic.FirstOrDefault(x => x.Value == scene).Key);
    }

    // 現在のシーンを再度ロードする
    public void ReloadCurrentScene()
    {
        StageCon scene = MyGetScene();
        MyLoadScene(scene);
    }

    //前のシーンに戻る時に使う
    public void ReloadClearScene()
    {
        MyLoadScene(backScene);
    }

    //クリアからステージに戻る時に使う
    //使わないと思うけど残しておく
    public void ClearBack()
    {
        MyLoadScene(clearBackScene);
    }
}
