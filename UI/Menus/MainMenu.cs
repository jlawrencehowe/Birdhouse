using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Canvas mainMenuCanvas;
    public Canvas optionsMenuCanvas;
    public Canvas loadingScreenCanvas;
    public Image loadBar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadMainStage()
    {
        if (optionsMenuCanvas.enabled == false)
        {
            loadingScreenCanvas.enabled = true;
            StartCoroutine(LoadYourAsyncScene());
        }

    }

    IEnumerator LoadYourAsyncScene()
    {
       

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainStage");
        //asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            var tempScale = new Vector3(asyncLoad.progress, loadBar.rectTransform.localScale.y, loadBar.rectTransform.localScale.z);
            loadBar.rectTransform.localScale = tempScale;
            yield return null;
        }
    }


    public void OpenOptions()
    {
        optionsMenuCanvas.GetComponent<OptionsMenu>().UpdateBarValuesAndText();
        optionsMenuCanvas.enabled = true;
    }

    public void CloseOptions()
    {
        optionsMenuCanvas.enabled = false;
    }
}
