using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSFX : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<AudioSource>() == null){
            gameObject.AddComponent<AudioSource>();

        }
        audioSource = GetComponent<AudioSource>();
        if (GetComponent<AudioSource>() != null)
        {

            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            if(clip == null)
                clip = (AudioClip)Resources.Load("SFXs/ClickButton");
            audioSource.clip = clip;
        }
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && audioSource != null)
        {
            audioSource.volume = PlayerPrefHandler.GetSFX()/1.1f;
            audioSource.Play();
        }
    }

    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.volume = PlayerPrefHandler.GetSFX()/1.1f;
            audioSource.Play();
        }
    }

    public void UpdateClip(AudioClip clip)
    {
        this.clip = clip;
        if(audioSource == null)
        {
            gameObject.AddComponent<AudioSource>();
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.clip = clip;
    }
}
