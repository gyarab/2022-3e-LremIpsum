using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnder : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Pokud má level skočit a má dojít k návratu do menu, nechte políčko prázdné")]
    public string nameOfNextScene = "";
    public GameObject posledniDlazdice;
    void Start()
    {
        if(posledniDlazdice != null){
            InvokeRepeating("activationCheck", 1, 1);
        }
        if(nameOfNextScene == ""){
            nameOfNextScene = GlobalVariables.menuSceneName;
        }
    }
    void activationCheck(){
        if(Player.currentPlayerPoint == ((Button)posledniDlazdice.GetComponent<Button>()).point){
            GlobalVariables.loadFromSave = false;
            StartCoroutine(loadScene());
            StartCoroutine(fadeToBlack());
        }
    }
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(nameOfNextScene);
    }
    IEnumerator fadeToBlack()
    {
        yield return new WaitForSeconds(0.5f);
        PostProcessingController.fadeOut = true;
    }
}
