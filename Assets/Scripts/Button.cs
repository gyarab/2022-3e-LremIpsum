using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject click;
    public Point point = null;
    [Header("Jde o speciální sousedství [Zastaralé nepoužívat, ale m?lo by stále fungovat]")]
    public int id = 0;
    public float speed = 0;
    public GameObject direction = null;
    public SpecialConnection Sc;

    void clicked()
    {
        // Pokud je tøeba, zjisti kdo jsi
        if (point == null)
        {
            for (int k = 0; k < Player.mapa.Count; k++)
            {
                if (((Point)Player.mapa[k]).button == this.gameObject)
                {
                    point = (Point)Player.mapa[k];
                    break;
                }
            }
        }
        Instantiate(click, transform.position + transform.up.normalized * 0.01f, transform.rotation);
        //Debug.Log("click");
        Player.najdiCestu(point);
    }


    
    void Start()
    {
        
    }


    void Update()
    {
       
    }
}
