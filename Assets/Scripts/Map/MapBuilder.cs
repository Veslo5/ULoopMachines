using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{

    public static MapBuilder FindMe() => GameObject.Find("**TRACK**/MapBuilder").GetComponent<MapBuilder>();

    public MapSpawner MapSpawner;

    private void Awake()
    {
        MapSpawner = new MapSpawner();
    }

    public void BuildMap(MapData mapData)
    {
        foreach (var roadPart in mapData.TrackParts)
        {
            var type = (TrackPropertyType)Enum.Parse(typeof(TrackPropertyType), roadPart.Type);
            GameObject go = null;
            switch (type)
            {
                case TrackPropertyType.BACKGROUND:
                    go = MapSpawner.SpawnBackground(roadPart.Set, false);
                    break;

                case TrackPropertyType.ROAD:
                    go = MapSpawner.SpawnRoad((MapParts)Enum.Parse(typeof(MapParts), roadPart.CustomType), roadPart.Set, false);
                    break;
            }
            
            go.transform.position = roadPart.Position;
            go.transform.localScale = roadPart.Scale;
            go.transform.eulerAngles = roadPart.Rotation;
        }
    }
}
