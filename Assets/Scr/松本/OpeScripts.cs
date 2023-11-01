using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeScripts : MonoBehaviour
{
    [SerializeField]
    GameObject Panel = null;
    [SerializeField]
    private AudioClip soundE;
    //Œø‰Ê‰¹—p
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Opetrue()
    {
        if (Panel.activeSelf)
        {
            Panel.SetActive(false);
            audioSource.PlayOneShot(soundE);
        }
        else
        {
            Panel.SetActive(true);
            audioSource.PlayOneShot(soundE);
        }
    }
}
