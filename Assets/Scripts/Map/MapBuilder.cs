using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
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
            switch (type)
            {
                case TrackPropertyType.BACKGROUND:
                var bgGo = MapSpawner.SpawnBackground(roadPart.Set, false);
                break;

                case TrackPropertyType.ROAD:
                var rGo = MapSpawner.SpawnRoad((RoadType)Enum.Parse(typeof(RoadType), roadPart.CustomType), roadPart.Set, false);
                break;
            }
        }
    }
}
