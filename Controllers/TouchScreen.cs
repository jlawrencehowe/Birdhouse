using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour
{
    Touch touch;
    public Camera mainCamera;
    public float moveSpeed;
    bool returnScrolling;
    private float botMax, topMax;
    private GameController gc;
    public float returnScrollSpeed;
    public bool scrollLock;
    private float retainedSpeed;
    public float scrollFriction;
    public float minRetainedSpeed;
    public bool useFriction;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        botMax = mainCamera.transform.position.y;
        topMax = mainCamera.transform.position.y;

        float highestTenant = topMax;
        foreach (var tenant in gc.tenants)
        {
            if (tenant.transform.position.y > highestTenant && tenant.GetComponent<Tenant>().GetIsUnlocked())
            {
                highestTenant = tenant.transform.position.y;
            }
        }
        topMax = highestTenant;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !scrollLock)
        {
            touch = Input.touches[0];
            if (touch.phase == TouchPhase.Moved)
            {
                //Vector3 tempVec = new Vector3(mainCamera.transform.position.x, touch.deltaPosition.y * moveSpeed, mainCamera.transform.position.z);
                // Debug.Log("Vector: " + tempVec);
                float deltaSpeed = touch.deltaPosition.y * moveSpeed;
                mainCamera.transform.Translate(0, -deltaSpeed, 0);
                if(Mathf.Abs(deltaSpeed) >= Mathf.Abs(retainedSpeed) || (deltaSpeed > 0 && retainedSpeed < 0) || (deltaSpeed < 0 && retainedSpeed > 0))
                    retainedSpeed = -deltaSpeed;

                

            }
            if (touch.deltaPosition.y >= -0.1f && touch.deltaPosition.y <= 0.1f)
            {
                retainedSpeed = 0;
            }

        }
        else if (scrollLock)
        {
            //var tempVector = new Vector3(mainCamera.transform.position.x, botMax, mainCamera.transform.position.z);
            //mainCamera.transform.position = tempVector;
            retainedSpeed = 0;
        }
        else if (useFriction)
        {
            if(retainedSpeed < minRetainedSpeed)
            {

                retainedSpeed += scrollFriction * Time.deltaTime;
            }
            else
            {

                retainedSpeed -= scrollFriction * Time.deltaTime;
            }
            if (retainedSpeed <= minRetainedSpeed && retainedSpeed >= -minRetainedSpeed)
            {
                retainedSpeed = 0;
            }
            mainCamera.transform.Translate(0, retainedSpeed, 0);

        }
        var tempPos = mainCamera.transform.position;
        if (mainCamera.transform.position.y > topMax || topMax <= botMax)
        {
            tempPos.y = topMax;
            retainedSpeed = 0;
        }
        else if (mainCamera.transform.position.y < botMax)
        {
            tempPos.y = botMax;
            retainedSpeed = 0;
        }

        mainCamera.transform.position = tempPos;

    }

    public bool GetIsScrolling()
    {

        if ((touch.deltaPosition.y >= -0.1f && touch.deltaPosition.y <= 0.1f) && retainedSpeed == 0)
            return false;
        else
            return true;
    }

    public void UpdateTopMax(float topPos)
    {
        if (topPos > topMax)
            topMax = topPos;

    }

    public float GetTopMax()
    {
        return topMax;
    }
}
