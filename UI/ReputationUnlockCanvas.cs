using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReputationUnlockCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI unlockText;
    public Canvas canvas;
    public Button yesButton;
    public AudioSource audioSource;    

    void Start()
    {
        audioSource.volume = PlayerPrefHandler.GetSFX();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRepText(string text)
    {
        unlockText.text = "Reputation Level Up!\n" + text;
    }

    public void UpdateFavorText(string text)
    {
        unlockText.text = text;
    }

    public void ConfirmButton()
    {
        canvas.enabled = false;
        yesButton.enabled = false;
        Destroy(gameObject, 0.5f);
    }
}
