using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScreen : MonoBehaviour
{

    public List<GameObject> screens;
    public float scrollSpeed;
    public float screenEdge;
    public Camera cam;
    public float skySize;
    // Start is called before the first frame update
    void Start()
    {
        screenEdge = cam.transform.position.x - (cam.orthographicSize * cam.aspect);
        
        skySize = screens[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x * screens[0].transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        float screenMoveAmount = scrollSpeed * Time.deltaTime;
        foreach (var screen in screens)
        {
            var tempPos = screen.transform.position;
            tempPos.x -= screenMoveAmount;
            if (tempPos.x + skySize/2 <= screenEdge)
            {

                tempPos.x += skySize * 2;
            }
            screen.transform.position = tempPos;

            
        }
        
        
    }
}
