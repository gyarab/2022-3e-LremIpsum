using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retezController : MonoBehaviour
{
    // Start is called before the first frame update
    LevelCotroller controller;
    public GameObject rotate;
    public GameObject slider;

    public GameObject[] getMoveableComponents(){
        GameObject[] outs = {rotate, slider};
        return outs;
    }
    void Start()
    {
        controller = GameObject.Find("LevelController").GetComponent<LevelCotroller>();
    }
    void Update(){
        float lvlRot = rotate.transform.rotation.eulerAngles.y;
        float slide = slider.transform.position.y;
        if(lvlRot > 89 && lvlRot < 91)
        {
            controller.IDOkolnosti[0] = true;
        }
        else
        {
            controller.IDOkolnosti[0] = false;
        }

        if(slide > 6.9f && slide < 7.1f)
        {
            controller.IDOkolnosti[1] = true;
        }
        else
        {
            controller.IDOkolnosti[1] = false;
        }

        if(lvlRot > -1f && lvlRot < 1f && slide > 5.02f && slide < 5.1f)
        {
            controller.IDOkolnosti[2] = true;
        }
        else
        {
            controller.IDOkolnosti[2] = false;
        }
    }
}
