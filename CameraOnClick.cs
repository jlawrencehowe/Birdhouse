using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraOnClick : MonoBehaviour
{

    Camera mainCam;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = this.GetComponent<Camera>();  
       
    }

    // Update is called once per frame
    void Update()
    {
        List<RaycastResult> results = new List<RaycastResult>(); ;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Code to be place in a MonoBehaviour with a GraphicRaycaster component
            GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
            //Create the PointerEventData with null for the EventSystem
            PointerEventData ped = new PointerEventData(null);
            //Set required parameters, in this case, mouse position
            ped.position = Input.mousePosition;
            //Create list to receive all results
            //Raycast it
            gr.Raycast(ped, results);
        }

        if (Input.GetMouseButtonDown(0) && results.Count == 0)
        { // if left button pressed...
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.TryGetComponent(out Tenant tenant))
                {
                    tenant.ClickedByPlayer();
                    Debug.Log("Tenant");
                }
            }
            else
            {
                Debug.Log("Missed");
            }

        }
    }
}
