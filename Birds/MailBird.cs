using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBird : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        Invoke("TriggerBirdAnim", Random.Range(2f, 7f));


    }

    private void TriggerBirdAnim()
    {
        anim.SetTrigger("flap");
        Invoke("TriggerBirdAnim", Random.Range(2f, 7f));
    }

   
}
