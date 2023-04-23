using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoadLevel : MonoBehaviour
{
    public string sceneToLoad = "";
    void Start()
    {
        
    }
    public void clicked(){
        GlobalVariables.loadFromSave = false;
        SoundManager.playClickIfPossible();
        StartCoroutine(loadScene());
        StartCoroutine(fadeToBlack());
    }
    // Update is called once per frame
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(sceneToLoad);
        /*if(System.IO.File.Exists(Application.persistentDataPath+"/"+ GlobalVariables.savedirectoryName + "/" + GlobalVariables.saveName + ".bin")){
            GlobalVariables.load();
        }else{
            SceneManager.LoadScene(sceneToLoad);
        }*/
        
    }
    IEnumerator fadeToBlack()
    {
        yield return new WaitForSeconds(0.5f);
        PostProcessingController.fadeOut = true;
    }
}

