using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // Start is called before the first frame update
    LevelCotroller controller;
    public GameObject slider1;
    public GameObject slider2;

    public GameObject[] getMoveableComponents(){
        GameObject[] outs = {slider1, slider2};
        return outs;
    }
    void Start()
    {
        controller = GameObject.Find("LevelController").GetComponent<LevelCotroller>();
    }
    void Update()
    {
        float slide1 = slider1.transform.position.y;
        float slide2 = slider2.transform.position.y;

        if(slide1 > -0.1f && slide1 < 0.1f){
            controller.IDOkolnosti[0] = true;
        }else{
            controller.IDOkolnosti[0] = false;
        }
        if(slide1 > 7.8f && slide1 < 8f)
        {
            controller.IDOkolnosti[1] = true;
        }
        else
        {
            controller.IDOkolnosti[1] = false;
        }
        if(slide2 > 7.8f && slide2 < 8f)
        {
            controller.IDOkolnosti[2] = true;
        }
        else
        {
            controller.IDOkolnosti[2] = false;
        }
        if(slide2 > 2f && slide2 < 2.1f)
        {
            controller.IDOkolnosti[5] = true;
        }
        else
        {
            controller.IDOkolnosti[5] = false;
        }
        if(slide2 > 3.9f && slide2 < 4f)
        {
            controller.IDOkolnosti[4] = true;
        }
        else
        {
            controller.IDOkolnosti[4] = false;
        }
        if(slide2 > 5.9f && slide2 < 6f)
        {
            controller.IDOkolnosti[3] = true;
        }
        else
        {
            controller.IDOkolnosti[3] = false;
        }
        if(slide1 > -0.1f && slide1 < 0.1f){
            controller.IDOkolnosti[6] = true;
        }else{
            controller.IDOkolnosti[6] = false;
        }
        if(slide1 > 1.8f && slide1 < 2f){
            controller.IDOkolnosti[7] = true;
        }else{
            controller.IDOkolnosti[7] = false;
        }
        if(slide1 > 3.8f && slide1 < 4f){
            controller.IDOkolnosti[8] = true;
        }else{
            controller.IDOkolnosti[8] = false;
        }
        if(slide1 > 5.8f && slide1 < 6f){
            controller.IDOkolnosti[9] = true;
        }else{
            controller.IDOkolnosti[9] = false;
        }
        if(slide1 > 7.8f && slide1 < 8f){
            controller.IDOkolnosti[10] = true;
        }else{
            controller.IDOkolnosti[10] = false;
        }
    }
}