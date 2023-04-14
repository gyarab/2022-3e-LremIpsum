using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodzemiController : MonoBehaviour
{
    LevelCotroller controller;
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject pos4;
    void Start()
    {
        controller = GameObject.Find("LevelController").GetComponent<LevelCotroller>();
    }

    // Update is called once per frame
    void Update()
    {
        float s1 = pos1.transform.position.y;
        float s2 = pos2.transform.position.y;
        float s3 = pos3.transform.position.y;
        float s4 = pos4.transform.position.y;
        if((s1>8f&&s1<8.2f)&&(s2>8.1f&&s2<8.3f)&&(s3>8.2f&&s3<8.35f)&&(s4>8.1f&&s4<8.3f)){
            controller.IDOkolnosti[0] = true;
        }else{
            controller.IDOkolnosti[0] = false;
        }
    }
}
