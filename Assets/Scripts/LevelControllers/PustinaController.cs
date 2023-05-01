using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PustinaController : MonoBehaviour
{
    // Start is called before the first frame update
    LevelCotroller controller;
    public GameObject elevate;
    public GameObject slider;
    public GameObject rotate;
    

    public GameObject[] getMoveableComponents(){
        GameObject[] outs = {elevate, slider, rotate};
        return outs;
    }
    void Start()
    {
        controller = GameObject.Find("LevelController").GetComponent<LevelCotroller>();
    }
    void Update()
    {
        float elevated = elevate.transform.position.y;
        float slided = slider.transform.position.z;
        float lvlRot = rotate.transform.rotation.eulerAngles.y;

        if (lvlRot > 179 && lvlRot < 181)
        {
            controller.IDOkolnosti[0] = true;
            controller.IDOkolnosti[5] = true;
        }
        else
        {
            controller.IDOkolnosti[0] = false;
            controller.IDOkolnosti[5] = false;
        }
        if(elevated > -2.3f && elevated < -2.1f)
        {
            controller.IDOkolnosti[1] = true;
        }
        else
        {
            controller.IDOkolnosti[1] = false;
        }
        if(elevated > 5.9f && elevated < 6.1f)
        {
            controller.IDOkolnosti[2] = true;
        }
        else
        {
            controller.IDOkolnosti[2] = false;
        }
        
        if(slided > 31f && slided < 32f)
        {
            controller.IDOkolnosti[3] = true;
        }
        else
        {
            controller.IDOkolnosti[3] = false;
        }
        if(slided > 25f && slided < 26f)
        {
            controller.IDOkolnosti[4] = true;
        }
        else
        {
            controller.IDOkolnosti[4] = false;
        }
    }
}