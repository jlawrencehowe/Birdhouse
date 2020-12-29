using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{

    public delegate void ButtonMethod();
    ButtonMethod activeMethod;

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

        if (Input.GetMouseButtonDown(0))
        {
            activeMethod();
        }
    }

    public void SetActiveMethod(ButtonMethod method)
    {
        activeMethod = method;
    }
}
