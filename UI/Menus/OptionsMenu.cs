using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;

public class OptionsMenu : MonoBehaviour


{


    //notification options
    //check box for mute

    public Text sfxText;
    public Text musicText;
    public Slider musicBar;
    public Slider sfxBar;

    public GameUI gameUI;

    public Image optionsMenu;
    private Canvas optionsCanvas;
    private GameController gc;
    private MusicPlayer mp;
    public GameObject confirmationCanvas;

    public Toggle notiToggle;

    public Button resumeButton;

    // Start is called before the first frame update
    void Start()
    {
        optionsCanvas = GetComponent<Canvas>();
        if(GameObject.Find("GameController") != null)
            gc = GameObject.Find("GameController").GetComponent<GameController>();
        if(mp == null)
        {
            mp = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
        }
        notiToggle.isOn = PlayerPrefHandler.GetNotification();
    }



    public void UpdateBarValuesAndText()
    {
        UpdateBarValues();
        UpdateBarText();
    }

    public void UpdateBarValues()
    {
        musicBar.value = PlayerPrefHandler.GetMusic() * 100;
        sfxBar.value = PlayerPrefHandler.GetSFX() * 100;
    }

    public void UpdateBarText()
    {

        sfxText.text = "SFX: " + (PlayerPrefHandler.GetSFX() * 100);
        musicText.text = "Music: " + (PlayerPrefHandler.GetMusic() * 100);
    }


    public void OnMusicBarUpdate()
    {
        PlayerPrefHandler.SetMusic(musicBar.value/100);
        if(gc != null)
            gc.musicPlayer.UpdateVol();
        UpdateBarText();

    }


    public void OnSFXBarUpdate()
    {
        PlayerPrefHandler.SetSFX(sfxBar.value/100);
        UpdateBarText();
    }

    public void OpenMenu()
    {
        UpdateBarValuesAndText();
        optionsCanvas.enabled = true;
        resumeButton.enabled = true;
    }

    public void CloseMenu()
    {
        if (gc != null)
        {
            gc.gameUI.SetActiveMenu(GameUI.ActiveMenu.none);
            gc.touchScreen.scrollLock = false;
        }
        optionsCanvas.enabled = false;
        Tenant.openUI = false;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetGame()
    {

        confirmationCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        confirmationCanvas.SetActive(true);
        resumeButton.enabled = false;
        CancelInvoke();
    }

    public void ConfirmReset()
    {
        if (gc != null)
        {
            gc.gameUI.SetActiveMenu(GameUI.ActiveMenu.none);
            gc.touchScreen.scrollLock = false;
            gc.notificationHandler.ClearRequestNotification();
        }
        Tenant.openUI = false;
        Tenant.activeRequest = false;
        Tenant.totalFavor = 0;
        GlobalMultipliers.ResetAll();
        SavedData.deleteData("save1");
        SavedData.deleteData("save2");
        SceneManager.LoadScene("MainMenu");
    }

    public void DenyReset()
    {
        confirmationCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(10000, 10000);
        Invoke("DeactivateConfirmationCanvas", 0.2f);
        resumeButton.enabled = true;
    }

    void DeactivateConfirmationCanvas()
    {

        confirmationCanvas.SetActive(false);
    }

    public void SetNotificationCheckBox()
    {
        PlayerPrefHandler.SetNotification(notiToggle.isOn);
    }

}
