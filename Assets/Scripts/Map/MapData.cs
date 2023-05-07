using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public List<TrackPart> TrackParts = new List<TrackPart>();

}

[Serializable]
public class TrackPart
{
    public string Type;

    public string CustomType;
    public string Asset;
    public string Set;
    public Vector3 Position;
    public Vector3 Scale;
    public Vector3 Rotation;

}