using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    static GameObject mp;
    AudioSource MpPlayer;
    public AudioClip mainTheme;
    public AudioClip loopedTheme;


    // Start is called before the first frame update
    void Start()
    {
        if (mp == null)
        {
            mp = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }
        MpPlayer = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);

        if (mainTheme != null)
        {
            MpPlayer.volume = PlayerPrefHandler.GetMusic();
            MpPlayer.clip = mainTheme;
            MpPlayer.loop = false;
            MpPlayer.Play();
            StartCoroutine(WaitForTrackTOend());
        }
    }

    

    IEnumerator WaitForTrackTOend()
    {
        while (MpPlayer.isPlaying)
        {

            yield return new WaitForSeconds(0.01f);

        }
        MpPlayer.clip = loopedTheme;
        MpPlayer.loop = true;
        MpPlayer.Play();

    }

    public void UpdateVol()
    {
        MpPlayer.volume = PlayerPrefHandler.GetMusic();
    }

}
