using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingBlock : MonoBehaviour
{
    [TextArea(3, 10)]
    public string text = "Sample text";
    public bool restrictToDistanceFromPlayer = false;
    public float distance = 8f;
    public bool showBasedOnId = false;
    public int idOkolnosti = 0;
    GameObject CanvasToLoad;
    GameObject instancedCanvas;
    InfoText instanceScript;
    LevelCotroller lc;
    Material mat;
    public bool isOnDisplay = false;
    void Start()
    {
        CanvasToLoad = Resources.Load ("InfoText/InfoText") as GameObject;
        mat = gameObject.GetComponent<Renderer>().material;
        lc = ((LevelCotroller)GameObject.Find("LevelController").GetComponent<LevelCotroller>());
        if(lc.IDOkolnosti[idOkolnosti] == true){
            isOnDisplay = true;
        }else{
            mat.SetFloat("_Alpha", 0);
        }
    }

    // Update is called once per frame
    void clicked()
    {
        if(InfoText.exists == false){
            if(restrictToDistanceFromPlayer == true && Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position) > distance){
                return;
            }
            if(showBasedOnId == true && lc.IDOkolnosti[idOkolnosti] == false){
                return;
            }
            instancedCanvas = Instantiate(CanvasToLoad);
            instanceScript = instancedCanvas.GetComponent<InfoText>();
            instanceScript.display(text);
        }
    }
    void Update(){
        if(showBasedOnId){
            if(lc.IDOkolnosti[idOkolnosti] == true && isOnDisplay == false){
                StartCoroutine(FadeIn(0.4f));
                isOnDisplay = true;
            }else if(lc.IDOkolnosti[idOkolnosti] == false && isOnDisplay == true){
                StartCoroutine(FadeOut(0.4f));
                isOnDisplay = false;
            }
        }
    }
    public IEnumerator FadeIn(float t)
    {
        float waitTime = 0;
        while (waitTime < 1)
        {
        mat.SetFloat("_Alpha",Mathf.Lerp(0,0.22f,waitTime));
        yield return null;
        waitTime += Time.deltaTime / t;
        }
    }
    public IEnumerator FadeOut(float t)
    {
        float waitTime = 0;
        while (waitTime < 1)
        {
        mat.SetFloat("_Alpha",Mathf.Lerp(0.22f,0,waitTime));
        yield return null;
        waitTime += Time.deltaTime / t;
        }
        mat.SetFloat("_Alpha",0);
    }
}
