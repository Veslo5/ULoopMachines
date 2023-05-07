using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackPropertyType
{
    ROAD = 0,
    BACKGROUND = 1,
    COLLIDER = 2
}

public class MapEditorProperty : MonoBehaviour
{
    public TrackPropertyType Type;
    public string SpritePath;
    public string Set;
    public string CustomType;
}
