using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TransformSerializable
{
    public float px;
    public float py;
    public float pz;
    public float rx;
    public float ry;
    public float rz; 
    public TransformSerializable(float px, float py, float pz, float rx, float ry, float rz){
        this.px = px;
        this.py = py;
        this.pz = pz;
        this.rx = rx;
        this.ry = ry;
        this.rz = rz;
    }
}
