using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GiftNameUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private bool mouseOver;
    

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
