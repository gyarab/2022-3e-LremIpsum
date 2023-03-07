using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogCanvasController : MonoBehaviour
{
    [Header("Dlaždice, která okno spustí (nepovinné)")]
    public GameObject aktivačníDlaždice;
    public GameObject textMeshObject;
    CanvasGroup canvasGroup;
    public float duration = 0.5f;
    bool active = false;
    TMP_Text textComponent;
    [TextArea(3, 10)]
    public string[] dialog;
    int readingProgress = 0;

    public void activate(){
        textComponent.SetText(dialog[0]);
        StartCoroutine(appearCanvas(duration));
    }
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        textComponent = textMeshObject.GetComponent<TMP_Text>();
        if(dialog.Length > 0){
            textComponent.SetText(dialog[0]);
        }
        if(aktivačníDlaždice != null){
            InvokeRepeating("activationCheck", 3, 3);
        }
    }
    public IEnumerator appearCanvas(float t)
    {
        canvasGroup.interactable = true;
        float waitTime = 0;
        while (canvasGroup.alpha < 0.99f)
        {
        canvasGroup.alpha = Mathf.Lerp(0, 1, waitTime);
        yield return null;
        waitTime += Time.deltaTime / t;

        }
    }
    public IEnumerator vanishCanvas(float t)
    {
        float waitTime = 0;
        while (canvasGroup.alpha > 0.01f)
        {
        canvasGroup.alpha = Mathf.Lerp(1, 0, waitTime);
        yield return null;
        waitTime += Time.deltaTime / t;

        }
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0f;
    }
    public void click(){
        // Play click sound
        SoundManager.playClickIfPossible();
        if(readingProgress+1 < dialog.Length){
            readingProgress++;
            StartCoroutine(FadeOut(duration/2f, textComponent, dialog[readingProgress]));
        }else{
            readingProgress = 0;
            StartCoroutine(vanishCanvas(duration));
        }
    }
    public IEnumerator FadeIn(float t, TMP_Text i)
    {
        float waitTime = 0;
        while (waitTime < 1)
        {
        i.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.clear, Color.white, waitTime));
        yield return null;
        waitTime += Time.deltaTime / t;
        }
    }
 
    public IEnumerator FadeOut(float t, TMP_Text i, string text)
    {
        float waitTime = 0;
        while (waitTime < 1)
        {
        i.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.white, Color.clear, waitTime));
        yield return null;
        waitTime += Time.deltaTime / t;
        }
        textComponent.SetText(text);
        StartCoroutine(FadeIn(duration/2f, textComponent));
    }

    // Update is called once per frame
    void activationCheck(){
        if(Player.currentPlayerPoint == ((Button)aktivačníDlaždice.GetComponent<Button>()).point && active == false){
            active = true;
            activate();
        }else if(Player.currentPlayerPoint != ((Button)aktivačníDlaždice.GetComponent<Button>()).point){
            active = false;
        }
    }
}
