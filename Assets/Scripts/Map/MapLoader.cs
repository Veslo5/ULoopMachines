using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : Singleton<MapLoader>
{

    public string GetJsonTrack(){
        var goObjects = GameObject.FindGameObjectsWithTag(Globals.TAG_EDITOR);

        var mapData = new MapData();

        foreach (var go in goObjects)
        {
            var prop = go.GetComponent<MapEditorProperty>();
            mapData.TrackParts.Add(new TrackPart{
                Position = go.transform.position,
                Rotation = go.transform.rotation.eulerAngles,
                Scale = go.transform.localScale,
                Type = prop.Type.ToString(),
                Asset = prop.SpritePath,
                Set = prop.Set,
                CustomType = prop.CustomType
            });
            Debug.Log(go.name);
        } 

        return JsonUtility.ToJson(mapData);

    }


    public void SaveToDisc(){

    }

    public void SaveToPrefs(){
        PlayerPrefs.SetString("editormap", GetJsonTrack());
    }

    public MapData LoadFromPrefs(){
        return JsonUtility.FromJson<MapData>(PlayerPrefs.GetString("editormap"));
    }

}
