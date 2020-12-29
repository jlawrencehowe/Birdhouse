using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerScript : MonoBehaviour
{

    public delegate void ButtonMethod();
    ButtonMethod onClickMethod;
    ButtonMethod onDestroyMethod;
    ButtonMethod onMouseOver;
    ButtonMethod onMouseExit;
    public bool deactivateAll;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0) && onClickMethod != null && !deactivateAll && !Tenant.openUI)
        {
            onClickMethod();
        }
        if(onMouseOver != null)
            onMouseOver();
    }

    private void OnMouseExit()
    {
        if (onMouseExit != null)
            onMouseExit();
    }

    private void OnDestroy()
    {
        if (onDestroyMethod != null)
        {
            onDestroyMethod();
        }
    }

    public void SetOnClickMethod(ButtonMethod onClick)
    {
        this.onClickMethod = onClick;
    }

    public void SetOnDestroyMethod(ButtonMethod onDestroy)
    {
        this.onDestroyMethod = onDestroy;
    }

    public void SetOnMouseExitMethod(ButtonMethod onExit)
    {
        onMouseExit = onExit;
    }

    public void SetOnMouseOverMethod(ButtonMethod onOver)
    {
        onMouseOver = onOver;
    }

    public void SetDeactivateAll(bool isDeact)
    {
        deactivateAll = isDeact;
    }
}
