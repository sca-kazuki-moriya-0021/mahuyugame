using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillClip : MonoBehaviour
{
    [SerializeField]private RawImage rawImage;
    [SerializeField]private EventSystem ev = EventSystem.current;
    private GameObject selectedSkill;//１F前に選択しているスキル
    private GameObject nowSelectSkill;//今選択しているスキル

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
