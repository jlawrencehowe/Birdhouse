using System;
using UnityEngine;
using TMPro;
//using UnityEditor.PackageManager.Requests;

public class RequestTimer : MonoBehaviour
{
    public TextMeshPro timerText;
    public GameObject completeButton;
    public SpriteRenderer sr;
    public Renderer requestRenderer;

    public void Start()
    {
        completeButton.SetActive(false);
    }

    public void SetTime(float currentTime, float maxTime)
    {

        /*
        if (time > 0)
        {
            int min = (int)(time / 60);
            int sec = (int)(time % 60);
            timerText.text = min.ToString("D2") + ":" + sec.ToString("D2");
        }
        else
        {
            timerText.text = "Complete!";
            gameObject.GetComponent<ListenerScript>().SetDeactivateAll(false);
            completeButton.SetActive(true);
        }
        */


        if(currentTime <= 0)
        {
            gameObject.GetComponent<ListenerScript>().SetDeactivateAll(false);
            //completeButton.SetActive(true);

            requestRenderer.material.SetFloat("_FillSlider", 1);
        }
        else
        {

            requestRenderer.material.SetFloat("_FillSlider", 1 - (currentTime / maxTime));

        }
    }

}
