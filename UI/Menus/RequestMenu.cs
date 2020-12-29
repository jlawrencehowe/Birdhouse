using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequestMenu : DialogueBox
{

    private List<int> upgradeCosts;
    public GameObject UIRef;
    private List<UIObject> uiObjects;
    public List<Sprite> listOfMaterialImages;
    private Tenant tenant;
    public GameController gc;
    public int reputationValue;
    private Sprite objectSprite;
    private string requestObjectName;
    public GameObject background;
    bool finalSetupLock;


    private List<string> acceptedText;
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

    public new void Awake()
    {

        uiObjects = new List<UIObject>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        base.Awake();

    }


    public void SetInfo(List<string> dialogueText, List<string> acceptText, List<string> completedMessage,
        List<int> listOfCosts, Tenant tenant, int reputationValue, Sprite objectSprite, string requestObjectName)
    {
        this.tenant = tenant;
        SetAutoText(false);
        SetUsesButtons(true);
        AddText(dialogueText);
        acceptedText = acceptText;
        //requestText.text = dialogueText;
        upgradeCosts = listOfCosts;
        this.reputationValue = reputationValue;
        this.objectSprite = objectSprite;
        this.requestObjectName = requestObjectName;
        //secondaryText = completedMessage;

        //instantiate enough UIRef for each of the costs that aren't 0
        /*
        int counter = 0;
        for (int i = 0; i < upgradeCosts.Count; i++)
        {
            if (upgradeCosts[i] != 0)
            {
                var tempGameobject = Instantiate(UIRef, this.transform.position, this.transform.rotation) as GameObject;
                UIObject tempUIObject = new UIObject(tempGameobject, tempGameobject.transform.Find("CostText").GetComponent<TextMeshProUGUI>(),
                    tempGameobject.transform.Find("MaterialImage").GetComponent<Image>());
                uiObjects.Add(tempUIObject);
                tempGameobject.transform.SetParent(transform);
                tempGameobject.transform.localPosition = new Vector3(-2.29f + (1.5f * counter), -1.02f, 0);
                tempUIObject.costText.text = upgradeCosts[i].ToString();
                tempUIObject.materialImage.sprite = listOfMaterialImages[i];
                if (upgradeCosts[i] > gc.currencyManager.GetCurrencyByID(i) || Tenant.activeRequest)
                {
                    NotEnoughMats();
                    //turn quantity number red?
                }
                counter++;
            }
        }*/

    }

    public void NotEnoughMats()
    {
        IsFixActive(false);
    }

    public override void FixButton()
    {

        AddText(acceptedText);
        int counter = uiObjects.Count;
        //Debug.Log("Counter: " + counter);
        for (int i = 0; i < counter; i++)
        {
            Destroy(uiObjects[i].uiRef);
        }
        fixButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        SetUsesButtons(false);



    }

    public override void OnDestroy()
    {
        if (!closedButton && properClose)
        {
            /*
            for (int i = 0; i < uiObjects.Count; i++)
            {
                uiObjects[i].materialImage.enabled = false;
            }*/
            Debug.Log("destry");
            //unlock screen scroll
            gc.currencyManager.SpendCurrency(upgradeCosts);
            //tenant.IncreaseRequest();
            tenant.StartRequest();
            //gc.reputation.AddReputation(reputationValue);
            //AddText(secondaryText);
            fixButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            //SetUsesButtons(false);
            gc.savedData.saveData();
            //Destroy(gameObject);
            //Debug.Log("Quest: " + secondaryText);
            //base.FixButton();
        }

    }

    protected override void SetupFinalPage()
    {
        if (!finalSetupLock)
        {
            base.SetupFinalPage();
            dialogueText.text = requestObjectName + ":\nRequirements:";
            nextArrow.SetActive(false);
            int counter = 0;
            for (int i = 0; i < upgradeCosts.Count; i++)
            {
                if (upgradeCosts[i] != 0)
                {
                    var tempGameobject = Instantiate(UIRef, this.transform.position, this.transform.rotation) as GameObject;
                    UIObject tempUIObject = new UIObject(tempGameobject, tempGameobject.transform.Find("CostText").GetComponent<TextMeshProUGUI>(),
                        tempGameobject.transform.Find("MaterialImage").GetComponent<Image>());
                    uiObjects.Add(tempUIObject);
                    tempGameobject.transform.SetParent(background.transform, false);
                    tempGameobject.GetComponent<RectTransform>().localPosition = new Vector3(-70f + (70f * counter), -30, 0);
                    tempUIObject.costText.text = "x" + upgradeCosts[i].ToString();
                    tempUIObject.materialImage.sprite = listOfMaterialImages[i];
                    if (upgradeCosts[i] > gc.currencyManager.GetCurrencyByID(i) || Tenant.activeRequest)
                    {
                        NotEnoughMats();
                        //turn quantity number red?
                    }
                    counter++;
                }
                
            }
            {
                var tempGameobject = Instantiate(UIRef, this.transform.position, this.transform.rotation) as GameObject;
                UIObject tempUIObject = new UIObject(tempGameobject, tempGameobject.transform.Find("CostText").GetComponent<TextMeshProUGUI>(),
                    tempGameobject.transform.Find("MaterialImage").GetComponent<Image>());
                uiObjects.Add(tempUIObject);
                tempGameobject.transform.SetParent(background.transform, false);
                tempGameobject.GetComponent<RectTransform>().localPosition = new Vector3(135, 8, 0);
                tempUIObject.costText.text = " ";
                tempUIObject.materialImage.sprite = objectSprite;
                tempUIObject.materialImage.preserveAspect = true;
                var materialImageRect = tempUIObject.materialImage.GetComponent<RectTransform>();
                materialImageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
                materialImageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
            }
            finalSetupLock = true;
        }
    }

}
