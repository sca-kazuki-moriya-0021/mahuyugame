using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCutInCon : MonoBehaviour
{
    [SerializeField, Header("�X�L���J�b�g�C���̃C���X�g")]
    private Sprite[] skillSprites;

    [SerializeField, Header("�擾�������摜")]
    private Image getImage;

    [SerializeField,Header("�X�L���J�b�g�C���̕���")]
    private string[] texts;

    [SerializeField,Header("�擾����������")]
    private Text getText;

    [SerializeField,Header("�X�L���J�b�g�C���̃A�j���[�V����")]
    private Animator[] skillAnimator;

    private Canvas myCanvas;

    private bool cutInFlag;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = this.GetComponent<Canvas>();

        myCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(cutInFlag == true)
        {
            time += Time.unscaledDeltaTime;
            if (time >= 3f)
            {
                time = 0;
                Time.timeScale = 1f;
                myCanvas.enabled = false;
            }
        }
    }
    
    public void PlayerCutInDisplay(int i)
    {
       for(int x = 0;x< skillAnimator.Length; x++)
       {
            Debug.Log("��������");
            skillAnimator[x].updateMode = AnimatorUpdateMode.UnscaledTime;
       }
       myCanvas.enabled = true;
       time = 0;
       Time.timeScale = 0f;
       getImage.sprite = skillSprites[i];
       getText.text = texts[i];
       cutInFlag = true;
    }
}
