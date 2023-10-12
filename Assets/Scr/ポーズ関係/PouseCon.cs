using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PouseCon : MonoBehaviour
{
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private Canvas myCanvas;

    private AudioSource audioSource;

    private TotalGM totalGM;

    // Start is called before the first frame update
    void Start()
    {
        totalGM = FindObjectOfType<TotalGM>();
        audioSource = GetComponent<AudioSource>();
        myCanvas = this.GetComponent<Canvas>();
        myCanvas.enabled = false;
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
