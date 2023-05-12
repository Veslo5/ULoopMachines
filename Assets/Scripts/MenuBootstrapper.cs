using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuBootstrapper : MonoBehaviour
{
    void Start()
    {
        var trackPath = Application.dataPath + "/Tracks";
        if(Directory.Exists(trackPath)){
            Directory.CreateDirectory(trackPath);
        }
    }
}
