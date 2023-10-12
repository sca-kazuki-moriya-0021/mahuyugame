using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferStage : MonoBehaviour
{
    //効果音用
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip SE;
    
    private TotalGM totalGM;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalGM = FindObjectOfType<TotalGM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //ゲーム終了
    public void GameEnd()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //エディタ上の動作
#else
            Application.Quit();
            //エディタ以外の操作
#endif

    }

    //スキルセレクト画面に行くとき
    public void SkillSelect()
    {
        audioSource.PlayOneShot(SE);
        SceneManager.LoadScene("SkillSelect", LoadSceneMode.Single);
    }

    //ステージリロード
    public void StageReload()
    {
        audioSource.PlayOneShot(SE);

        //if (timeGM.TimeFlag == false)
        {
            Time.timeScale = 1f;
        }

        totalGM.BackScene = totalGM.MyGetScene();
        totalGM.ReloadCurrentScene();

    }

    //ゲームオーバーからステージに戻る時に使う関数
    //事前にプレイヤーとかでBackSceneに値を投げておいてこれを発動する感じ
    public void ReloadStage()
    {
        audioSource.PlayOneShot(SE);
        totalGM.ReloadClearScene();
    }

    //クリア画面にいく時
    //多分使わない
    public void Clear()
    {
        audioSource.PlayOneShot(SE);
        SceneManager.LoadScene("Clear", LoadSceneMode.Single);
    }

    //ステージ1に移動
    public void Stage()
    {
        audioSource.PlayOneShot(SE);
        SceneManager.LoadScene("Stage", LoadSceneMode.Single);
    }

    /*public void ResetStage()
    {
        audioSource.PlayOneShot(SE);
        SceneManager.LoadScene("Stage", LoadSceneMode.Single);
    }*/
}
