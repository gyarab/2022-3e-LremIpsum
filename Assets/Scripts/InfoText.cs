using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoText : MonoBehaviour
{
    public TMP_Text textComponent;
    public static bool exists = false;
    CanvasGroup canGroup;
    public void display(string text){
        canGroup = gameObject.GetComponent<CanvasGroup>();
        canGroup.alpha = 0;
        textComponent = gameObject.GetComponentInChildren<TMP_Text>();
        textComponent.SetText(text);
        StartCoroutine(FadeIn(0.4f));
        StartCoroutine(FadeOut(0.4f, 4));
    }

    public IEnumerator FadeIn(float t)
    {
        exists = true;
        float waitTime = 0;
        while (waitTime < 1)
        {
        canGroup.alpha = Mathf.Lerp(0, 1, waitTime);
        yield return null;
        waitTime += Time.deltaTime / t;
        }
    }
    public IEnumerator FadeOut(float t, float delay)
    {
        yield return new WaitForSeconds(delay);
        float waitTime = 0;
        while (waitTime < 1)
        {
        canGroup.alpha = Mathf.Lerp(1, 0, waitTime);
        yield return null;
        waitTime += Time.deltaTime / t;
        }
        exists = false;
        Destroy(gameObject);
    }
}
