using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojuhelnikyController : MonoBehaviour
{
    // Start is called before the first frame update
    LevelCotroller controller;
    public GameObject rotate;
    public GameObject slider;
    bool[] def = new bool[11];
    bool[] vys = new bool[11];

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
        float slidY = slider.transform.position.y;

        for (int i = 1; i <= 10; i++)
        {
            vys[i] = false;
        }

        if(lvlRot > 359 || lvlRot < 1)
        {
            vys[1] = true;
            vys[2] = true;
        }
        if(lvlRot > 89 && lvlRot < 91)
        {
            vys[3] = true;
            vys[4] = true;
        }
        if(lvlRot > 179 && lvlRot < 181)
        {
            vys[5] = true;
            vys[6] = true;
        }
        if(lvlRot > 269 && lvlRot < 271)
        {
            vys[7] = true;
            vys[8] = true;
        }
        controller.IDOkolnosti[1] = vys[1];
        controller.IDOkolnosti[2] = vys[2];
        controller.IDOkolnosti[3] = vys[3];
        controller.IDOkolnosti[4] = vys[4];
        controller.IDOkolnosti[5] = vys[5];
        controller.IDOkolnosti[6] = vys[6];
        controller.IDOkolnosti[7] = vys[7];
        controller.IDOkolnosti[8] = vys[8];

        if(slidY < -1.2f){
            vys[9] = true;
        }
        if(slidY > 0.86f){
            vys[10] = true;
        }
        controller.IDOkolnosti[9]  = vys[9];
        controller.IDOkolnosti[10] = vys[10];
    }
}
