using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{

    static Debugger debugger;
    public bool isDebugger;
    public Canvas canvas;

    public Text gameTimeText;
    public float gameTime;
    GameController gc;

    public Text timeScaleText;

    public GameObject debuggerObj, debuggerObj2;

    // Start is called before the first frame update
    void Start()
    {

        if (!isDebugger || debugger != null)
        {
            Destroy(gameObject);
        }
        else
        {
            debugger = this;
        }

        gc = GameObject.Find("GameController").GetComponent<GameController>();

    }

    private void Update()
    {
        gameTime += Time.unscaledDeltaTime;

        //skip 2hours
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (var tenant in gc.tenants)
            {
                if (tenant.GetComponent<Tenant>().GetIsUnlocked())
                {
                    tenant.GetComponent<Tenant>().UpdateMoney(7200);
                    tenant.GetComponent<Tenant>().DebugReqeustTime(7200);
                    //Debug.Log("UpdatedTenant");

                }
            }

            if (gc.deliveryBird != null && gc.deliveryBird.GetIsUnlocked())
            {
                gc.deliveryBird.UpdateRawMaterials(7200);
            }
            gameTime += 120;
        }
        //add money
        if (Input.GetKeyDown(KeyCode.O))
        {
            gc.currencyManager.AddMoney(10000000);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            canvas.enabled = true;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            GlobalMultipliers.MoneyGen += 5;
        }

        gameTimeText.text = "Time: " + (int)gameTime;
    }

    public void UnlockSomeTenants()
    {
        gc.reputation.UnlockNextTenant(1);
        gc.reputation.UnlockNextTenant(2);
        gc.reputation.UnlockNextTenant(3);
        gc.reputation.UnlockNextTenant(4);
        gc.reputation.UnlockNextTenant(5);
        gc.reputation.UnlockNextTenant(6);
    }

    public void CloseDebug()
    {
        debuggerObj.SetActive(false);
        debuggerObj2.SetActive(true);
    }

    public void OpenDebug()
    {
        debuggerObj.SetActive(true);
        debuggerObj2.SetActive(false);
    }

    public void SkipTime()
    {
        foreach (var tenant in gc.tenants)
        {
            if (tenant.GetComponent<Tenant>().GetIsUnlocked())
            {
                tenant.GetComponent<Tenant>().UpdateMoney(7200);
                tenant.GetComponent<Tenant>().DebugReqeustTime(7200);
                //Debug.Log("UpdatedTenant");

            }
        }

        if (gc.deliveryBird != null && gc.deliveryBird.GetIsUnlocked())
        {
            gc.deliveryBird.UpdateRawMaterials(7200);
        }

    }

    public void TimeScaleToggle()
    {
        var tempTimeScale = Time.timeScale;
        if(tempTimeScale == 1)
        {
            timeScaleText.text = "Time Scale: 7";
            Time.timeScale = 7;
        }
        else
        {
            timeScaleText.text = "Time Scale: 1";
            Time.timeScale = 1;
        }
    }


    public void MaxRep()
    {
        gc.reputation.currentRepLevel = 30;
        gc.reputation.AddReputation(63);
        gc.reputation.shownReputation = 63;
    }

    public void WinGame() {

        Tenant.totalFavor = 63;
        //gc.reputation.reputation = 31;
        gc.reputation.ActivateFinale(false);

    }
}
