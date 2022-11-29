using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Simple Camera Movement")]
    [Space]
    [Header("Bloky, po kterých se bude kamera pohybovat s hráèem horizontálnì:")]
    public GameObject[] horizontalsObj;
    [Space]
    [Header("Bloky, po kterých se bude kamera pohybovat s hráèem vertikálnì:")]
    public GameObject[] verticalsObj;

    [Header("Pozice speciálních pohybù kamery")]
    public CameraPosition[] positions;
    [Header("Aktivaèní pozice hráèe pro speciální polohy")]
    public GameObject[] activationPositionsObj;

    public int offsetZ;
    public int offsetY;

    Point[] horizontals;
    Point[] verticals;
    Point[] activationPositions;

    bool active = false;

    void Start()
    {
        StartCoroutine(LateStart(3f));
        
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // Získání Point objekty pozic
        horizontals = new Point[horizontalsObj.Length];
        verticals = new Point[verticalsObj.Length];
        activationPositions = new Point[activationPositionsObj.Length];

        for (int i = 0; i < horizontalsObj.Length; i++)
        {
            horizontals[i] = najdiPoint(horizontalsObj[i]);
        }

        for (int i = 0; i < verticalsObj.Length; i++)
        {
            verticals[i] = najdiPoint(verticalsObj[i]);
        }

        for (int i = 0; i < activationPositionsObj.Length; i++)
        {
            activationPositions[i] = najdiPoint(activationPositionsObj[i]);
        }

        if (positions.Length != activationPositionsObj.Length)
        {
            Debug.LogError("Chyba pøi zadání parametrù do CameraMovemet. Poèet CameraPosition a ActivationPositions nesedí!");
        }
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            if (standingOn(horizontals))
            {
                Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, (GameObject.Find("Player").transform.position.z + offsetZ));
                Vector3 velocity = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.125f);
            }
            if (standingOn(verticals))
            {
                Vector3 desiredPosition = new Vector3(transform.position.x, (GameObject.Find("Player").transform.position.y + offsetY), transform.position.z);
                Vector3 velocity = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.125f);
            }
            for (int i = 0; i < activationPositions.Length; i++)
            {
                if (Player.currentPlayerPoint == activationPositions[i])
                {
                    Vector3 velocity = Vector3.zero;
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(positions[i].x, positions[i].y, positions[i].z), ref velocity, 0.250f);
                    break;
                }
            }
        }
    }

    // Najdi point na základì GameObject
    Point najdiPoint(GameObject obj)
    {
        Point point = null;
        for (int k = 0; k < Player.mapa.Count; k++)
        {
            if (((Point)Player.mapa[k]).button == obj)
            {
                point = (Point)Player.mapa[k];
                break;
            }
        }
        return point;
    }
    bool standingOn(Point[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            if(Player.currentPlayerPoint == arr[i])
            {
                return true;
            }
        }
        return false;
    }
}
