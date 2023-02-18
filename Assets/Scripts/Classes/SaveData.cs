using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct  SaveData
{
    public string sceneName;
    public TransformSerializable playerPosition;

    public int idOfPointPlayerIsStandingOn;
    public bool[] idOkolnosti;
    public TransformSerializable cameraPosition;
    public TransformSerializable[] moveableComponentsPosition;
    public float[] extra;
    public SaveData(string sceneName,TransformSerializable playerPosition,
                    int idOfPointPlayerIsStandingOn,bool[] idOkolnosti,
                    TransformSerializable cameraPosition,TransformSerializable[] moveableComponentsPosition,float[] extra){
        this.sceneName = sceneName;
        this.playerPosition = playerPosition;
        this.idOfPointPlayerIsStandingOn = idOfPointPlayerIsStandingOn;
        this.idOkolnosti = idOkolnosti;
        this.cameraPosition = cameraPosition;
        this.moveableComponentsPosition = moveableComponentsPosition;
        this.extra = extra;
    }
}