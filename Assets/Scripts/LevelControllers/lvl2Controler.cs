using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl2Controler : MonoBehaviour
{
    // Start is called before the first frame update
    LevelCotroller controller;
    public GameObject rotate;
   

    public GameObject[] getMoveableComponents()
    {
        GameObject[] outs = {rotate};
        return outs;
    }
    void Start()
    {
        controller = GameObject.Find("LevelController").GetComponent<LevelCotroller>();
    }
    void Update()
    {
        float lvlRot = rotate.transform.rotation.eulerAngles.y;
        
        if (lvlRot > 89 && lvlRot < 91)
        {
            controller.IDOkolnosti[0] = true;
        }
        else
        {
            controller.IDOkolnosti[0] = false;
        }
        if (lvlRot > 269 && lvlRot < 271)
        {
            controller.IDOkolnosti[1] = true;
        }
        else
        {
            controller.IDOkolnosti[1] = false;
        }

    }
}
