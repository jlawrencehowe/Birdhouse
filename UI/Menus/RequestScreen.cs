using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequestScreen : MonoBehaviour
{
   
    private List<int> upgradeCosts;
    public Text requestText;
    public GameObject UIRef;
    private List<UIObject> uiObjects;
    public List<Sprite> listOfMaterialImages;
    public Button yesButton;
    private Tenant tenant;
    public GameController gc;
    public int reputationValue;
    private string completeMessage;
    public class UIObject
    {
        public GameObject uiRef;
        public TextMeshProUGUI costText;
        public Image materialImage;

        public UIObject(GameObject uiRef, TextMeshProUGUI costText, Image materialImage)
        {
            this.uiRef = uiRef;
            this.costText = costText;
            this.materialImage = materialImage;
        }
    }

    public void Awake()
    {

        uiObjects = new List<UIObject>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();

    }


    public void SetInfo(string dialogueText, List<int> listOfCosts, Tenant tenant, string completeMessage, int reputationValue)
    {
        this.tenant = tenant;
        requestText.text = dialogueText;
        upgradeCosts = listOfCosts;
        this.reputationValue = reputationValue;
        this.completeMessage = completeMessage;

        //instantiate enough UIRef for each of the costs that aren't 0
        for (int i = 0; i < upgradeCosts.Count; i++)
        {
            if (upgradeCosts[i] != 0)
            {
                var tempGameobject = Instantiate(UIRef, this.transform.position, this.transform.rotation) as GameObject;
                UIObject tempUIObject = new UIObject(tempGameobject, tempGameobject.transform.Find("CostText").GetComponent<TextMeshProUGUI>(),
                    tempGameobject.transform.Find("MaterialImage").GetComponent<Image>());
                uiObjects.Add(tempUIObject);
                tempGameobject.transform.SetParent(transform);
                tempUIObject.costText.text = upgradeCosts[i].ToString();
                tempUIObject.materialImage.sprite = listOfMaterialImages[i];
                if (upgradeCosts[i] > gc.currencyManager.GetCurrencyByID(i))
                {
                    NotEnoughMats();
                    //turn quantity number red?
                }
            }
        }
        
    }

    public void NotEnoughMats()
    {
        yesButton.enabled = false;
    }

    public void YesButton()
    {
        //unlock screen scroll
        gc.currencyManager.SpendCurrency(upgradeCosts);
        tenant.IncreaseRequest();
        gc.reputation.AddReputation(reputationValue);
        Debug.Log("Quest: " + completeMessage);
        gc.touchScreen.scrollLock = false;
        Destroy(gameObject);
    }

    public void NoButton()
    {
        //unlock screen scroll
        gc.touchScreen.scrollLock = false;
        Destroy(gameObject);
    }


}
