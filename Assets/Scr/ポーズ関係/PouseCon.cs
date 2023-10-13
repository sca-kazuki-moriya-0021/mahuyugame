using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PouseCon : MonoBehaviour
{
    //効果音
    [SerializeField]
    private AudioClip soundE;

    [SerializeField,Tooltip("自分のキャンパス")]
    private Canvas myCanvas;
    [SerializeField,Header("カウントダウンキャンパス")]
    private Canvas countDownCanvas;

    private AudioSource audioSource;

    private bool menuFlag = false;

    private TotalGM totalGM;

    public bool MenuFlag
    {
        get { return this.menuFlag; }
        set { this.menuFlag = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalGM = FindObjectOfType<TotalGM>();
        audioSource = GetComponent<AudioSource>();
        myCanvas = this.GetComponent<Canvas>();
        countDownCanvas = GetComponent<Canvas>();
        myCanvas.enabled = false;
        countDownCanvas.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            myCanvas.enabled = true;
        }
    }

    //ゲーム終了
    public void GameEnd()
    {
        audioSource.PlayOneShot(soundE);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //エディタ上の動作
#else
            Application.Quit();
            //エディタ以外の操作
#endif

    }

    //ゲームに戻る
    public void BackStage()
    {
        audioSource.PlayOneShot(soundE);
        Time.timeScale = 1f;
        myCanvas.enabled = false;
        countDownCanvas.enabled = false;
    }


    //ステージリロード
    public void StageReload()
    {
        audioSource.PlayOneShot(soundE);

        myCanvas.enabled = true;
        //if (timeGM.TimeFlag == false)
        {
            Time.timeScale = 1f;
        }

        totalGM.BackScene = totalGM.MyGetScene();
        totalGM.ReloadCurrentScene();

    }
}
