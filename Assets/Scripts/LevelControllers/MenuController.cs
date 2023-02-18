using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    private Vector3 vel = Vector3.zero;
    public SwipeController ct;
    public float speed = 0.5f;
    public float sensitivity = 2;

    public float minX = 0;
    public float maxX = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sp = speed;
        if(ct.isDragActive){
            sp = 0.2f;
        }
        Vector3 shift = new Vector3(-ct.horizontalDrag * sensitivity, 0, 0);
        // Boundaries test
        if(camera.transform.position.x <= minX && ct.horizontalDrag > 0){
            shift = Vector3.zero;
        }
        if(camera.transform.position.x >= maxX && ct.horizontalDrag < 0){
            shift = Vector3.zero;
        }
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, camera.transform.position + shift, ref vel, sp);
    }
}
