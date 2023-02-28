using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialConnections : MonoBehaviour
{
    [Header("Special connection")]
    public string conName = "voluntairly";
    public GameObject firstEndPoint;
    public GameObject secondEndPoint;
    public int idSpojeni;
    [Header("Teleportation using two overlapping buttons")]
    public bool teleporationConnection = false;
    public GameObject middlePoint;
    public bool isMiddlePointOverlappingFirtst = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
