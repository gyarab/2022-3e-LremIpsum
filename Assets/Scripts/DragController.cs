using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public static bool allowDrag = true;
    bool isDragActive = false;
    Vector2 lastScreenPosition;
    Vector2 screenPosition;
    Vector3 worldPosition;
    MoveableController dragging;
    MoveableController ct;
    private Vector2 velocity = Vector2.zero;
    public float smoothTime = 0.3f;
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
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                ct = hit.transform.gameObject.GetComponent<MoveableController>();
                if (ct != null)
                {
                    dragging = ct;
                    lastScreenPosition = Input.GetTouch(0).position;
                    initDrag();
                }

            }
        }
    }

    void initDrag()
    {
        ct.interactionStarted();
        isDragActive = true;
        ct.interacting = true;
        ct.afterInteraction = false;
        ct.sliding = false;    
    }
    void drag()
    {
        screenPosition = Vector2.SmoothDamp(lastScreenPosition, screenPosition, ref velocity, smoothTime);
        ct.distance = Mathf.Pow(Mathf.Pow(screenPosition.x - lastScreenPosition.x, 2)+ Mathf.Pow(screenPosition.y - lastScreenPosition.y, 2), 0.5f)*Time.deltaTime;
        ct.distanceX = (screenPosition.x - lastScreenPosition.x) * Time.deltaTime;
        ct.distanceY = (screenPosition.y - lastScreenPosition.y) * Time.deltaTime;
        lastScreenPosition = screenPosition;
    }

    void drop()
    {
        isDragActive = false;
        ct.interacting = false;
        ct.afterInteraction = true;
    }
}
