using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    //uninteractable ones, delete after x amount of time on the last string, if more than one string, auto scroll
    //interactable ones, tap to add all text, when all text there include an indicator for tapping, when tapped and all text there go to next one
    //if at end and all text there include buttons

    public TextMeshProUGUI dialogueText;
    protected int currentText;
    protected int currentChar;
    public float readSpeed;
    bool autoText;
    bool isDone;
    bool nextText;
    protected bool usesButtons;
    protected Animator anim;
    public Button fixButton, cancelButton;
    //public TenantButton.ButtonMethod passedButtonMethod;
    public GameObject nextArrow;

    public Tutorial tutorial;
    public int firstCharID;
    public int lastCharID;
    protected bool closedButton;
    protected bool properClose;

    public Image portrait;

    public List<AudioClip> birdChirps;
    AudioSource audioSource;

    protected void Awake()
    {

        nextArrow.SetActive(false);
        anim = GetComponent<Animator>();
        /*passedButtonMethod = FixButton;
        if (fixButton != null)
        {
            fixButton.SetFunction(passedButtonMethod);
        }
        
        passedButtonMethod = ExitDialogueButton;
        if (cancelButton != null)
        {
            cancelButton.SetFunction(passedButtonMethod);
        }
        */
        if(fixButton != null)
            fixButton.onClick.AddListener(FixButton);
        if(cancelButton != null)
            cancelButton.onClick.AddListener(ExitDialogueButton);

        audioSource = GetComponent<AudioSource>();

    }

    protected void Start()
    {
        if (fixButton != null)
        {
            fixButton.gameObject.SetActive(false);
        }
        if (cancelButton != null)
        {
            cancelButton.gameObject.SetActive(false);
        }
        dialogueText.maxVisibleCharacters = 0;
        dialogueText.ForceMeshUpdate();
    }

    protected void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !autoText)
        {
            Debug.Log("Cliked");

            if (isDone && !usesButtons)
            {
                Debug.Log("Trigger Close");
                if(tutorial != null)
                {
                    //tutorial.NextStep();
                }
                //anim.SetTrigger("Close");

                properClose = true;
                Destroy(gameObject);
            }
            else if (nextText)
            {
                /*
                nextArrow.SetActive(false);
                currentChar = 0;
                dialogueText.pageToDisplay = currentText;
                for (int i = 0; i < dialogueText.textInfo.characterInfo.Length; i++)
                {
                    dialogueText.textInfo.characterInfo[i].isVisible = false;
                }
                ReadLine();
                nextText = false;
                */
                nextArrow.SetActive(false);
                dialogueText.maxVisibleCharacters = lastCharID;
                dialogueText.pageToDisplay = currentText + 1;
                ReadLine();
                nextText = false;
            }
            else if(currentText < dialogueText.textInfo.pageCount)
            {
                /*
                //fill text then trigger next char
                for (int i = currentChar; i < dialogueText.textInfo.characterInfo.Length; i++)
                {
                    dialogueText.textInfo.characterInfo[i].isVisible = true;
                }
                currentChar = dialogueText.textInfo.characterCount;
                */
                dialogueText.maxVisibleCharacters = lastCharID;
                currentChar = lastCharID;

            }
        }
    }

    public void DialogueClicked()
    {
        if (!autoText)
        {
            Debug.Log("Cliked");

            if (isDone && !usesButtons)
            {
                Debug.Log("Trigger Close");
                if (tutorial != null)
                {
                    //tutorial.NextStep();
                }
                //anim.SetTrigger("Close");

                properClose = true;
                Destroy(gameObject);
            }
            else if(isDone && usesButtons)
            {
                SetupFinalPage();
            }
            else if (nextText)
            {
                /*
                nextArrow.SetActive(false);
                currentChar = 0;
                dialogueText.pageToDisplay = currentText;
                for (int i = 0; i < dialogueText.textInfo.characterInfo.Length; i++)
                {
                    dialogueText.textInfo.characterInfo[i].isVisible = false;
                }
                ReadLine();
                nextText = false;
                */
                nextArrow.SetActive(false);
                dialogueText.maxVisibleCharacters = lastCharID;
                dialogueText.pageToDisplay = currentText + 1;
                ReadLine();
                nextText = false;
            }
            else if (currentText < dialogueText.textInfo.pageCount)
            {
                /*
                //fill text then trigger next char
                for (int i = currentChar; i < dialogueText.textInfo.characterInfo.Length; i++)
                {
                    dialogueText.textInfo.characterInfo[i].isVisible = true;
                }
                currentChar = dialogueText.textInfo.characterCount;
                */
                dialogueText.maxVisibleCharacters = lastCharID;
                currentChar = lastCharID;

            }
        }
    }

    public void ExitDialogueButton()
    {
        Debug.Log("Close");
        //anim.SetTrigger("Close");
        closedButton = true;
        GetComponent<Canvas>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    public virtual void FixButton()
    {
        Debug.Log("Fix");
        anim.SetTrigger("Close");

        gameObject.transform.position = new Vector3(10000, 10000, 0);
        properClose = true;
        Destroy(gameObject, 0.5f);
    }

    public void AddText(string text)
    {
        isDone = false;
        nextText = false;
        currentChar = 0;
        currentText = 0;
        dialogueText.text = "";
        dialogueText.text = text;
        dialogueText.pageToDisplay = 1;
        dialogueText.ForceMeshUpdate();
        ReadLine();
    }

    public void AddText(List<string> texts)
    {
        isDone = false;
        nextText = false;
        currentChar = 0;
        currentText = 0;
        dialogueText.text = "";
        dialogueText.pageToDisplay = 1;
        foreach (var text in texts)
        {
            dialogueText.text += ("" + text);
        }

        dialogueText.ForceMeshUpdate();
        ReadLine();
        //ReadLine();

        /*
        if (listOfText == null)
        {
            listOfText = new List<string>();
        }
        listOfText.Clear();
        isDone = false;
        nextText = false;
        currentChar = 0;
        currentText = 0;
        listOfText = texts;
        dialogueText.text = "";
        ReadLine();*/
    }

   

    public void SetAutoText(bool isAuto)
    {
        autoText = isAuto;
    }

    public void SetAudioClipType(int type)
    {
        if(type == 0)
        {
            audioSource.pitch = 1.2f;
        }
        else if(type == 2)
        {
            audioSource.pitch = 0.8f;
        }
        else
        {
            audioSource.pitch = 1f;
        }
        
    }


    //trigger after the box has fully opened from the animation
    public void ReadLine()
    {
        firstCharID = dialogueText.textInfo.pageInfo[currentText].firstCharacterIndex;
        lastCharID = dialogueText.textInfo.pageInfo[currentText].lastCharacterIndex;
        InvokeRepeating("NextChar", 0, readSpeed);
        
    }

    public void SetUsesButtons(bool usesButtons)
    {
        this.usesButtons = usesButtons;
    }

    protected void NextChar()
    {


        if (currentText < dialogueText.textInfo.pageCount && currentChar <= lastCharID)
        {
            currentChar++;
            dialogueText.maxVisibleCharacters = currentChar;
            /* //All letters
            if (!audioSource.isPlaying)
            {
                audioSource.volume = PlayerPrefHandler.GetSFX();
                var num = Random.Range(0, 4);
                audioSource.clip = birdChirps[num];
                audioSource.Play();
            }*/
            //Only at spaces
            if(dialogueText.text[currentChar - 1] == ' ')
            {
                audioSource.volume = PlayerPrefHandler.GetSFX();
                var num = Random.Range(0, 4);
                audioSource.clip = birdChirps[num];
                audioSource.Play();
            }
        }
        else
        {
            currentText++;
            CancelInvoke();
            if (currentText >= dialogueText.textInfo.pageCount)
            {
                if (autoText)
                {
                    Invoke("CloseText", 2f);
                }
                else
                {
                    
                    if (usesButtons && !isDone)
                    {
                        nextArrow.SetActive(true);
                    }
                    else
                    {
                        nextArrow.SetActive(true);
                    }
                    isDone = true;
                }
            }
            else
            {

                if (autoText)
                {
                    dialogueText.text = "";
                    Invoke("ReadLine", 2f);
                }
                else
                {
                    nextArrow.SetActive(true);
                    nextText = true;
                }
            }
        }
    }

    public void CloseText()
    {
        anim.SetTrigger("Close");
        IsFixActive(false);
        Destroy(gameObject);
    }

    protected virtual void SetupFinalPage()
    {
        fixButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }

    public void IsFixActive(bool isFixActive)
    {
        fixButton.interactable = isFixActive;
    }

    public virtual void OnDestroy()
    {
        
    }
}
