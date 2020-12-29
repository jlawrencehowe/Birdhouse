using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleHandler : MonoBehaviour
{

    public GameObject DialogueRef;
    GameController gc;
    List<string> loadedDialogue;
    public Sprite penny, penguin, owl, batBird, pigeon, platypus;

    public GameObject confetti, trophyRef;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        Tenant.openUI = true;
        gc.gameUI.CloseAllMenus();
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        loadedDialogue = new List<string>();
        TextAsset text = Resources.Load<TextAsset>("Dialogue/Finale/pennyDialogue");
        loadedDialogue.Add(text.text);
        tempDialogue.AddText(loadedDialogue);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = penny;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(PenguinDialogue);
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x, 
            gc.tenants[0].transform.position.y, gc.mainCam.transform.position.z);
        gc.touchScreen.scrollLock = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PenguinDialogue()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        loadedDialogue.Clear();
        TextAsset text = Resources.Load<TextAsset>("Dialogue/Finale/penguinDialogue");
        loadedDialogue.Add(text.text);
        tempDialogue.AddText(loadedDialogue);
        tempDialogue.SetAudioClipType(2);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = penguin;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(OwlDialogue);
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x,
            gc.tenants[1].transform.position.y, gc.mainCam.transform.position.z);
    }

    void OwlDialogue()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        loadedDialogue.Clear();
        TextAsset text = Resources.Load<TextAsset>("Dialogue/Finale/owlDialogue");
        loadedDialogue.Add(text.text);
        tempDialogue.AddText(loadedDialogue);
        tempDialogue.SetAudioClipType(1);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = owl;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(BatBirdDialogue);
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x,
            gc.tenants[4].transform.position.y, gc.mainCam.transform.position.z);
    }

    void BatBirdDialogue()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        loadedDialogue.Clear();
        TextAsset text = Resources.Load<TextAsset>("Dialogue/Finale/batBirdDialogue");
        loadedDialogue.Add(text.text);
        tempDialogue.AddText(loadedDialogue);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = batBird;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(PigeonDialogue);
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x,
            gc.tenants[10].transform.position.y, gc.mainCam.transform.position.z);
    }

    void PigeonDialogue()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        loadedDialogue.Clear();
        TextAsset text = Resources.Load<TextAsset>("Dialogue/Finale/pigeonDialogue");
        loadedDialogue.Add(text.text);
        tempDialogue.AddText(loadedDialogue);
        tempDialogue.SetAudioClipType(1);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = pigeon;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(PlatypusDialogue);
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x,
            gc.tenants[13].transform.position.y, gc.mainCam.transform.position.z);
    }

    void PlatypusDialogue()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        loadedDialogue.Clear();
        TextAsset text = Resources.Load<TextAsset>("Dialogue/Finale/platypusDialogue");
        loadedDialogue.Add(text.text);
        tempDialogue.AddText(loadedDialogue);
        tempDialogue.SetAudioClipType(1);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = platypus;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(PennyDialogue2);
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x,
            gc.tenants[20].transform.position.y, gc.mainCam.transform.position.z);
    }

    void PennyDialogue2()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        loadedDialogue.Clear();
        TextAsset text = Resources.Load<TextAsset>("Dialogue/Finale/pennyDialogue2");
        loadedDialogue.Add(text.text);
        tempDialogue.AddText(loadedDialogue);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = penny;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(FinishDialogue);
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x,
            gc.deliveryBird.transform.position.y, gc.mainCam.transform.position.z);
    }

    void FinishDialogue()
    {

        Instantiate(trophyRef, new Vector3(-0.45f, -10.5f, -1), transform.rotation);
        Tenant.openUI = false;
        gc.touchScreen.scrollLock = false;
        Destroy(confetti);
        Destroy(gameObject);
    }
}
