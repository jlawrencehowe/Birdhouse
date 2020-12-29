using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    GameController gc;
    int tapCounter = 3;
    bool broken;
    Animator squidAnim;

    // Start is called before the first frame update
    void Start()
    {
        broken = false;
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        squidAnim = GetComponent<Animator>();

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !broken)
        {
            tapCounter--;

            squidAnim.SetTrigger("rock");
            if (tapCounter == 0 && !broken)
            {
                broken = true;
                squidAnim.SetTrigger("squid");
            }
        }
    }
}
