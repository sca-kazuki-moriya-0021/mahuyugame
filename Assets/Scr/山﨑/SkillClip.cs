using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillClip : MonoBehaviour
{
    [SerializeField]private RawImage rawImage;
    [SerializeField]private EventSystem ev = EventSystem.current;
    private GameObject selectedSkill;//�PF�O�ɑI�����Ă���X�L��
    private GameObject nowSelectSkill;//���I�����Ă���X�L��

    // Start is called before the first frame update
    void Start()
    {
        this.rawImage.enabled =false;
        nowSelectSkill = ev.currentSelectedGameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    
}
