using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVAnimator : MonoBehaviour
{
    public Vector2 direction; // Direction
    public int materialIndex = 0;

    private Renderer rendererComponent; // Renderer component
    void Start()
    {
        rendererComponent = GetComponent<Renderer>();
    }

    void Update()
    {
        rendererComponent.materials[materialIndex].mainTextureOffset = new Vector2(direction.x * Time.time, direction.y *  Time.time);
    }
}
