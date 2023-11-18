using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    private bool bossActiveFlag;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;

    private bool bossPhaseFlag = false;


    public bool BossActiveFlag
    {
        get { return this.bossActiveFlag; }
        set { this.bossActiveFlag = value; }
    }

    public bool BossPhaseFlag
    {
        get { return this.bossPhaseFlag; }
        set { this.bossPhaseFlag = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossPhaseFlag == true)
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
            bossPhaseFlag = false;
        }
    }
}
