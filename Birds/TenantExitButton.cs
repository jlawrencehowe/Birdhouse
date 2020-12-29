using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenantExitButton : MonoBehaviour
{

    public TenantButtonHandler buttonHandler;
    bool disableClick;
    public Sprite activeSprite, disabledSprite;
    private SpriteRenderer spriteR;

    void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !disableClick)
        {
            buttonHandler.HideTenantButtons();
        }
    }

    public void SetDisableClick(bool isDisable)
    {
        disableClick = isDisable;
        if (isDisable)
        {
            spriteR.sprite = disabledSprite;
        }
        else
        {
            spriteR.sprite = activeSprite;
        }
    }
}
