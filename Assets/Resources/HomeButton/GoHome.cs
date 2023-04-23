using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour
{
    public void goHome(){
        SoundManager.playClickIfPossible();
        StartCoroutine(loadScene());
        StartCoroutine(fadeToBlack());
    }
    // Update is called once per frame
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(GlobalVariables.menuSceneName);
    }
    IEnumerator fadeToBlack()
    {
        yield return new WaitForSeconds(0.5f);
        PostProcessingController.fadeOut = true;
    }
}
