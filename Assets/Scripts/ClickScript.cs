using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    // -----------------   Animace Alpha kanálu   -----------------
    public float duration = 0.5f;
    float startTime;
    MeshRenderer[] renderers;
    void Start()
    {
        startTime = Time.time;
        renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        StartCoroutine(ExampleCoroutine());
    }
    void Update()
    {
        foreach (MeshRenderer rend in renderers)
        {
            Color clr = rend.material.color;
            Color newColor = new Color(clr.r, clr.g, clr.b, clr.a - ((Time.deltaTime / duration)));
            rend.material.SetColor("_Color", newColor);
        }
    }

    // Zniè tento objekt
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
