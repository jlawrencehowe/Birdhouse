using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMat : MonoBehaviour
{

    public Vector3 startPos, endPos, camPos;
    private Camera mainCam;
    public float minSpeed, maxSpeed;
    private float speed;
    private float distance;
    public float magMax, magMin;
    private float magnitude;
    private bool isGoingRight;
    GameController gc;
    private int amount;
    public GameObject FloatingMat;



    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        startPos = transform.position;
        camPos = mainCam.transform.position;
        //endPos = camPos;
    }

    public void Init(Vector3 endPos, Sprite sprite, GameController gc, int amount)
    {
        this.gc = gc;
        this.endPos = endPos;
        this.amount = amount;
        GetComponent<SpriteRenderer>().sprite = sprite;
        magnitude = Random.Range(magMin, magMax);
        speed = Random.Range(minSpeed, maxSpeed);
        if (Random.Range(0, 2) == 0)
        {
            isGoingRight = true;
        }
        //Camera.main.ScreenToWorldPoint(rectTransform.transform.position)
    }

    // Update is called once per frame
    void Update()
    {

        if (distance < 1)
        {
            distance += speed * Time.deltaTime;
        }
        else
        {
            distance = 1;
            gc.gameUI.PulseInventoryImage();
            Vector3 tempPos = this.transform.position;
            tempPos.z = -2;
            GameObject tempMat = Instantiate(FloatingMat, tempPos, this.transform.rotation) as GameObject;
            tempMat.GetComponent<FloatingMat>().InitStats(GetComponent<SpriteRenderer>().sprite, amount);
            tempMat.transform.SetParent(mainCam.transform);
            Destroy(gameObject);
        }
        Vector3 camDif = mainCam.transform.position - camPos;
        Vector3 direction = endPos - startPos;
        startPos += camDif;
        endPos += camDif;
        camPos = mainCam.transform.position;
        Vector3 curveDirection;
        if (isGoingRight)
        {
            curveDirection = Rotate90CW(direction).normalized;
        }
        else
        {
            curveDirection = Rotate90CCW(direction).normalized;
        }
        this.transform.position = (curveDirection * Mathf.Sin(Mathf.PI * distance) * magnitude) + Vector3.Lerp(startPos, endPos, distance);


    }


    Vector3 Rotate90CW(Vector3 aDir)
    {
        return new Vector3(aDir.y, -aDir.x, 0);
    }

    Vector3 Rotate90CCW(Vector3 aDir)
    {
        return new Vector3(-aDir.y, aDir.x, 0);
    }


}
