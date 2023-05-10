using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBootstrapper : MonoBehaviour
{

    private MapLoader mapLoader;
    private MapBuilder mapBuilder;

    private void Awake()
    {
        mapBuilder = MapBuilder.FindMe();
        mapLoader = MapLoader.FindMe();
    }

    // Start is called before the first frame update
    void Start()
    {
        mapBuilder.BuildMap(mapLoader.LoadFromPrefs());
    }


}
