using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockForDestruction : MonoBehaviour
{
    public int idOkolnosti;
    public float minVzdaloenost = 2;
    GameObject player;
    bool isInRange = false;
    [Header("btCanvas neměnit")]
    public GameObject btCanvas;
    GameObject button;
    CanvasGroup btCanGr;
    Camera cam;
    UnityEngine.UI.Button btScript;
    bool firstTime = true;
    public float maxOpacity = 0.7f;
    LevelCotroller lc;
    void Start()
    {
        lc = GameObject.Find("LevelController").GetComponent<LevelCotroller>();
        player = GameObject.Find("Player");
        btCanvas = Resources.Load ("DestroyObjectUI/DestroyObjectUI") as GameObject;
        btCanvas = Instantiate(btCanvas);
        button = btCanvas.transform.GetChild(0).gameObject;
        //print(button);
        btCanGr = btCanvas.GetComponent<CanvasGroup>();
        cam = Camera.main.GetComponent<Camera>();
        btScript = button.GetComponent<UnityEngine.UI.Button>();
        btScript.onClick.AddListener(click);
        /*if(Vector3.Distance(player.transform.position, gameObject.transform.position) < minVzdaloenost){
            isInRange = true;
            btCanGr.alpha = 1;
            btCanGr.interactable = true;
        }else{
            btCanGr.alpha = 0;
            btCanGr.interactable = false;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if(firstTime){
            firstTime = false;
            if(Vector3.Distance(player.transform.position, gameObject.transform.position) < minVzdaloenost){
                isInRange = true;
                btCanGr.alpha = maxOpacity;
                btCanGr.interactable = true;
            }else{
                btCanGr.alpha = 0;
                btCanGr.interactable = false;
            }
        }
        if(Vector3.Distance(player.transform.position, gameObject.transform.position) < minVzdaloenost){
            if(!isInRange){
                isInRange = true;
                StartCoroutine(FadeIn(0.4f));
            }
            button.transform.position = cam.WorldToScreenPoint(gameObject.transform.position);
        }
        if(Vector3.Distance(player.transform.position, gameObject.transform.position) >= minVzdaloenost && isInRange){
            isInRange = false;
            StartCoroutine(FadeOut(0.4f));
        }
    }
    void click(){
        // Play click sound
        SoundManager.playClickIfPossible();
        print("destroy object");
        StartCoroutine(FadeOut(0.3f));
        StartCoroutine(DestroyObj(0.5f));

    }
    void clicked(){

    }
    public IEnumerator FadeIn(float t)
    {
        btCanGr.interactable = true;
        float waitTime = 0;
        while (waitTime < 1)
        {
        btCanGr.alpha = Mathf.Lerp(0,maxOpacity,waitTime);
        yield return null;
        waitTime += Time.deltaTime / t;
        }
        btCanGr.alpha = maxOpacity;
    }
    public IEnumerator FadeOut(float t)
    {
        float waitTime = 0;
        while (waitTime < 1)
        {
        btCanGr.alpha = Mathf.Lerp(maxOpacity,0,waitTime);
        yield return null;
        waitTime += Time.deltaTime / t;
        }
        btCanGr.interactable = false;
        btCanGr.alpha = 0;
    }
    public IEnumerator DestroyObj(float t)
    {
        yield return new WaitForSeconds(0.3f);
        
        Material mat = gameObject.GetComponent<Renderer>().material;
        float waitTime = 0;
        float defaultA = mat.color.a;
        while (waitTime < 1)
        {
        mat.color = new Color(mat.color.r,mat.color.g,mat.color.b,Mathf.Lerp(defaultA,0,waitTime));
        yield return null;
        waitTime += Time.deltaTime / t;
        }
        lc.IDOkolnosti[idOkolnosti] = true;
        GameObject.Destroy(gameObject);
        GameObject.Destroy(btCanvas);
        gameObject.GetComponent<BlockForDestruction>().enabled = false;
    }
}