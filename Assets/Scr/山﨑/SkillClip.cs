using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class SkillClip : MonoBehaviour
{
    [SerializeField]private RawImage rawImage;
    [SerializeField]private EventSystem ev = EventSystem.current;
    [SerializeField]private VideoPlayer videoPlayer;//Videoを格納
    [SerializeField]private VideoClip[] skillClip;
    [SerializeField] private GameObject[] skill;
    
    private GameObject selectedSkill;//１F前に選択しているスキル
    private GameObject nowSelectSkill;//今選択しているスキル
    private string selectingOBJ;
    private bool backSelect;
    private bool videoPlay;
    private float time;
    private IEnumerator reset;
    void Start()
    {
        nowSelectSkill = ev.currentSelectedGameObject;
        //sprite = null;

        //バグ対策///////////////
        this.rawImage.enabled = false;
        reset = Video();
        videoPlayer.time = 0;
        videoPlayer.frame = 0;
        videoPlayer.Stop(); 
        //////////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        nowSelectSkill = ev.currentSelectedGameObject;      
    }

    //1f前と違う時
    private void LateUpdate()
    {
        if (selectedSkill != nowSelectSkill)
        {
            time = 0.0f;
            selectedSkill = nowSelectSkill;
            backSelect = true;    
            this.rawImage.enabled = false;
            //バグ対策///////////////
            videoPlay = false;
            videoPlayer.time = 0;
            videoPlayer.frame = 0;
            videoPlayer.Stop();
            if(reset!=null)
            StopCoroutine(reset);
            reset = null;
            /////////////////////////
        }
        else
        {
            backSelect = false;
            time += Time.deltaTime;
            if(time >= 5 && !videoPlay)
            {
                //バグ対策///////////////
                videoPlayer.enabled = true;
                videoPlayer.time = 0;
                videoPlayer.frame = 0;
                videoPlayer.Stop();               
                reset = Video();
                /////////////////////////
                StartCoroutine(reset);  
                time=0.0f;               
            }
        }
    }

    private void SelectSkillClip()
    {        
        selectingOBJ = selectedSkill.name;
        switch(selectingOBJ)
        {
            case "Skill0":
                videoPlayer.clip = skillClip[0];
                videoPlayer.Play();
                break;
            case "Skill1":
                videoPlayer.clip = skillClip[1];
                videoPlayer.Play();
                break;
            case "Skill2":
                videoPlayer.clip = skillClip[2];
                videoPlayer.Play();
                break;
            case "Skill3":
                videoPlayer.clip = skillClip[3];
                videoPlayer.Play();
                break;
        }
    }


    //ビデオの再生処理
    private IEnumerator Video()
    {
        
        while (!backSelect)
        {
            videoPlay = true;
            SelectSkillClip();
            yield return new WaitForSeconds(0.2f);
            this.rawImage.enabled = true;
            yield return new WaitForSeconds(7.0f);    
        }
    }

}
