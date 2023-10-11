using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class TotalGM : MonoBehaviour
{
    //�O��̃^�C���ƍ��̃^�C��
    private float[] nowTime;
    private float[] lastTime;
    
    #region//�X�e�[�W�Ǘ�

    //�X�e�[�W�Ǘ�
    public enum StageCon
    {
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

    public float[] NowTime {
        get { return this.nowTime; }
        set { this.nowTime = value; }
    }

    public float[] LastTime {
        get { return this.lastTime; }
        set { this.lastTime = value; }
    }

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
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
