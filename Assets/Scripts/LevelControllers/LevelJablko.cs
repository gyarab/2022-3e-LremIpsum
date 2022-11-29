using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelJablko : MonoBehaviour
{
    LevelCotroller controller;
    MoveableController rotate;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("LevelController").GetComponent<LevelCotroller>();
        rotate = GameObject.Find("Rotate").GetComponent<MoveableController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Rotate").transform.rotation.eulerAngles.y > 179 && GameObject.Find("Rotate").transform.rotation.eulerAngles.y < 181)
        {
            controller.IDOkolnosti[0] = true;
        }
        else
        {
            controller.IDOkolnosti[0] = false;
        }
    }
}
