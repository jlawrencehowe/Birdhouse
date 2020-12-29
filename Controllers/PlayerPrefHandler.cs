using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefHandler
{
    #region PlayerPrefs keys

    public const string MUSIC_VOL = "music";
    public const string SFX_VOL = "sfx";
    public const string SKIP_TUT = "tutorial";
    public const string SEND_NOTI = "notification";

    #endregion

    
    public static void SetMusic(float musicVol)
    {
        PlayerPrefs.SetFloat(MUSIC_VOL, musicVol);
    }

    public static float GetMusic()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOL, 50);
    }

    public static void SetSFX(float sfxVol)
    {
        PlayerPrefs.SetFloat(SFX_VOL, sfxVol);
    }

    public static float GetSFX()
    {
        return PlayerPrefs.GetFloat(SFX_VOL, 50);
    }

    public static void SetTutorial(bool setTut)
    {
        if (setTut)
        {
            PlayerPrefs.SetInt(SKIP_TUT, 1);
        }
        else
        {
            PlayerPrefs.SetInt(SKIP_TUT, 0);
        }
    }

    public static bool GetTutorial()
    {

        if (PlayerPrefs.GetInt(SKIP_TUT, 0) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static void SetNotification(bool isNotification)
    {
        if (isNotification)
            PlayerPrefs.SetInt(SEND_NOTI, 1);
        else
            PlayerPrefs.SetInt(SEND_NOTI, 0);
    }

    public static bool GetNotification()
    {
        if(PlayerPrefs.GetInt(SEND_NOTI, 1) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
