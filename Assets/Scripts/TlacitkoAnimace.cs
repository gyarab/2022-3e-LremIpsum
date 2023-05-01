using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;


public class TlacitkoAnimace : MonoBehaviour
{
    public UnityEngine.UI.Button bt;
    public GameObject animovany;
    private Animator anim;
    public string setScene = "";
    // Start is called before the first frame update
    void Start()
    {
        anim = animovany.GetComponent<Animator>();
        bt.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        anim.Play("Click");
        // PLaysound
        SoundManager.playClickIfPossible();
        StartCoroutine(loadScene());
        StartCoroutine(fadeToBlack());
    }
    void OnMouseDown()
    {
        Debug.Log("Down");
    }
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.8f);
        if(setScene!= "")
        {
            SceneManager.LoadScene(setScene);
        }
        if(System.IO.File.Exists(Application.persistentDataPath+"/"+ GlobalVariables.savedirectoryName + "/" + GlobalVariables.saveName + ".bin")){
            GlobalVariables.load();
        }else{
            SceneManager.LoadScene(GlobalVariables.sceneNames[0]);
        }
        
    }
    IEnumerator fadeToBlack()
    {
        yield return new WaitForSeconds(0.5f);
        PostProcessingController.fadeOut = true;
    }
}
