using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static bool allowDrag = true;
    public bool isDragActive = false;
    Vector2 lastScreenPosition;
    Vector2 screenPosition;
    Vector3 worldPosition;
    private Vector2 velocity = Vector2.zero;
    public float smoothTime = 0.3f;

    public float horizontalDrag = 0;
    public float verticalDrag = 0;
    void Start()
    {

    }
    void Update()
    {
        if(!allowDrag){
            return;
        }
        if (isDragActive && (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            drop();
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        if (isDragActive)
        {
            drag();
        }
        else
        {
 
            initDrag();     
        }
    }

    void initDrag()
    {
        isDragActive = true;
        lastScreenPosition.x = screenPosition.x;
        lastScreenPosition.y = screenPosition.y;
    }
    void drag()
    {
        screenPosition = Vector2.SmoothDamp(lastScreenPosition, screenPosition, ref velocity, smoothTime);
        horizontalDrag = (screenPosition.x - lastScreenPosition.x) * Time.deltaTime;
        verticalDrag = (screenPosition.y - lastScreenPosition.y) * Time.deltaTime;
        lastScreenPosition = screenPosition;
    }

    void drop()
    {
        horizontalDrag = 0;
        verticalDrag = 0;
        isDragActive = false;
    }
}
