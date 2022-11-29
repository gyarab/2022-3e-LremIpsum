using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TlacitkoAnimace : MonoBehaviour
{
    public Button bt;
    public GameObject animovany;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = animovany.GetComponent<Animator>();
        //bt.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        anim.Play("Click");
        Debug.Log("jéé");
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
        SceneManager.LoadScene(sceneName: "1. level");
    }
    IEnumerator fadeToBlack()
    {
        yield return new WaitForSeconds(0.5f);
        PostProcessingController.fadeOut = true;
    }
}
