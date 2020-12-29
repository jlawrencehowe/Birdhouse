using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ExamineButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject examineObject;
    public TextMeshProUGUI examineText;
    public int buttonNum;
    public GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (examineObject.activeSelf && Input.GetMouseButtonUp(0))
        {
            examineObject.SetActive(false);
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        examineObject.SetActive(true);
        

        examineObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(11.7f, 330 - (85 * (buttonNum - 1)), 0);
        examineText.text = "Current material production: " + gc.savedData.GetRawMatLvlByID(buttonNum) + "/min";
    }
}
