using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMenu : MonoBehaviour
{
    private GameController gc;
    // Start is called before the first frame update
    void Start()
    {

        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
    

    public void OpenMenu()
    {

    }

    public void CloseMenu()
    {
        /*
        gc.gameUI.SetActiveMenu(GameUI.ActiveMenu.none);
        gc.touchScreen.scrollLock = false;

        Tenant.openUI = false;*/
    }
}
