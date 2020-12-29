using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBird : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        Invoke("Flap", Random.Range(2f, 5f));
    }

    void Flap()
    {
        anim.SetTrigger("flap");
        Invoke("Flap", Random.Range(2f, 5f));
    }


}
