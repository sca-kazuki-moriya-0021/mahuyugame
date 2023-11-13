using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;

    private bool bossPhaseFlag = false;

    public bool BossPhaseFlag
    {
        get { return this.bossPhaseFlag; }
        set { this.bossPhaseFlag = value; }
    }

    private void Awake()
    {
      
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
        if(bossPhaseFlag == true)
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
            bossPhaseFlag = false;
        }
    }
}
