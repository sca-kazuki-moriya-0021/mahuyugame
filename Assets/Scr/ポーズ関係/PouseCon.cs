using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PouseCon : MonoBehaviour
{
    //効果音
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private Button button;

    [SerializeField]
    private EventSystem ev = EventSystem.current;

    [SerializeField,Tooltip("自分のキャンパス")]
    private Canvas myCanvas;
    private CountDownCon countDownCon;

    private AudioSource audioSource;

    private bool menuFlag = false;

    private TotalGM totalGM;

    private GameObject selectedObj;

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
        countDownCon = FindObjectOfType<CountDownCon>();
        myCanvas.enabled = false;
        //button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            myCanvas.enabled = true;
            button.Select();
            
        }
    }

    private void FixedUpdate()
    {
        if (selectedObj == null && myCanvas.enabled == true)
        {
            
            button.Select();
            selectedObj = ev.currentSelectedGameObject;
        }
        else
        {
            selectedObj = ev.currentSelectedGameObject;
            //アウトラインをここで入れる
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
        countDownCon.CountDownFlag = true;
        myCanvas.enabled = false;
        //Debug.Log("おとなるよー");
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
