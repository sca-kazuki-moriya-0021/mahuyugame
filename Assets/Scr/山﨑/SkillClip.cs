using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class SkillClip : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private EventSystem ev = EventSystem.current;
    [SerializeField] private VideoPlayer videoPlayer;//Video���i�[
    [SerializeField] private VideoClip[] skillClip;
    [SerializeField] private GameObject[] skill;
    [SerializeField] private Animator skillAnimator;
    //[SerializeField] Button button;
    private TotalGM totalGM;
    private GameObject selectedSkill;//�PF�O�ɑI�����Ă���X�L��
    private GameObject nowSelectSkill;//���I�����Ă���X�L��
    private string selectingOBJ;
    private bool backSelect;
    private bool videoPlay;
    private bool buttonPush;
    private float time = 0.0f;
    private IEnumerator reset;
    bool One;
    bool duringVideoPlayback;//�r�f�I���Đ��������肷��


    public bool ButtonPush
    {
        get { return buttonPush; }
        set { buttonPush = value; }
    }

    void Start()
    {
        //button.Select();
        //skillAnimator.keepAnimatorControllerStateOnDisable = false;
        nowSelectSkill = ev.currentSelectedGameObject;
        //selectedSkill = nowSelectSkill;
        //sprite = null;
        totalGM = FindObjectOfType<TotalGM>();
        //Debug.Log(totalGM.PlayerSkill[1]);
        //�o�O�΍�///////////////
        this.rawImage.enabled = false;
        reset = Video();
        //videoPlayer.time = 0;
        //videoPlayer.frame = 0;
        videoPlayer.Stop();
        //////////////////////////

        //PushButton();
    }

    // Update is called once per frame
    void Update()
    {       
        nowSelectSkill = ev.currentSelectedGameObject;
        if (!One)
        {
            selectedSkill = nowSelectSkill;
            One = true;
        }
        PushButton();
    }

    //1f�O�ƈႤ��
    private void LateUpdate()
    {
        if (selectedSkill != nowSelectSkill)
        {
            AnimationCheck();
            duringVideoPlayback = false;
            time = 0.0f;
            selectedSkill = nowSelectSkill;
            backSelect = true;
            this.rawImage.enabled = false;
            //�o�O�΍�///////////////
            videoPlay = false;
            videoPlayer.time = 0;
            videoPlayer.frame = 0;
            videoPlayer.Stop();
            if (reset != null)
                StopCoroutine(reset);
            reset = null;
            /////////////////////////
        }
        else
        {
            backSelect = false;
            time += Time.deltaTime;

            if (time >= 5 && !videoPlay)
            {
                //�o�O�΍�///////////////
                videoPlayer.enabled = true;
                //videoPlayer.time = 0;
                //videoPlayer.frame = 0;
                //videoPlayer.Stop();
                //reset = Video();
                /////////////////////////
                StartCoroutine(reset);
                time = 0.0f;
            }
        }

    }

    private void SelectSkillClip()
    {

        selectingOBJ = selectedSkill.name;
        switch (selectingOBJ)
        {
            case "Skill0":
                videoPlayer.clip = skillClip[0];
                skillAnimator.SetTrigger("Skill0");
                //videoPlayer.Play();
                break;
            case "Skill1":
                videoPlayer.clip = skillClip[1];
                skillAnimator.SetTrigger("Skill1");
                //videoPlayer.Play();
                break;
            case "Skill2":
                videoPlayer.clip = skillClip[2];
                skillAnimator.SetTrigger("Skill2");
                //videoPlayer.Play();
                break;
            case "Skill3":
                videoPlayer.clip = skillClip[3];
                skillAnimator.SetTrigger("Skill3");
                //videoPlayer.Play();
                break;
        }
    }

    private void PushButton()
    {
        if (buttonPush)
        {
            Debug.Log("�͂�����");
            time = 0;
            //�o�O�΍�///////////////
            videoPlay = false;
            videoPlayer.time = 0;
            videoPlayer.frame = 0;
            videoPlayer.Stop();
            if (reset != null)
            {
                StopCoroutine(reset);
                AnimationCheck();
                duringVideoPlayback = false;
            }
            reset = null;
            /////////////////////////

            this.rawImage.enabled = false;
            buttonPush = false;
        }
    }

    private void AnimationCheck()
    {

        if (duringVideoPlayback)
        {
            skillAnimator.SetTrigger("Select");

        }
    }


    //�r�f�I�̍Đ�����
    private IEnumerator Video()
    {

            SelectSkillClip();
            duringVideoPlayback = true;
            videoPlay = true;
            //skillAnimator.enabled = true;

            yield return new WaitForSeconds(0.8f);
            videoPlayer.Play();

            yield return new WaitForSeconds(0.2f);

            this.rawImage.enabled = true;
            yield return new WaitForSeconds(7.0f);
        
    }

}
