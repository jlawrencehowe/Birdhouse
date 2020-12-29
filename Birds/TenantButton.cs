using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenantButton : MonoBehaviour
{

    public delegate void ButtonMethod();
    ButtonMethod activeMethod;
    public bool disableClick;
    public Sprite activeSprite, disabledSprite;
    private SpriteRenderer spriteR;
    // Start is called before the first frame update
    void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !disableClick && activeMethod != null)
        {
            activeMethod();
        }
    }

    public void SetFunction(ButtonMethod activeMethod)
    {
        this.activeMethod = activeMethod;
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
