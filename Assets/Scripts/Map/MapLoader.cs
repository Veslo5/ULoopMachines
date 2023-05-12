using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

    public static MapLoader FindMe() => GameObject.Find("**DATA**/MapLoader").GetComponent<MapLoader>();

    public string GetJsonTrack()
    {
        var goObjects = GameObject.FindGameObjectsWithTag(Globals.TAG_EDITOR);

        var mapData = new MapData();

        foreach (var go in goObjects)
        {
            var prop = go.GetComponent<MapEditorProperty>();
            mapData.TrackParts.Add(new TrackPart
            {
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

    public void SaveToDisc(string name, string trackData)
    {
        File.WriteAllText(Application.dataPath + "/Tracks/" + name + ".json", trackData);
    }

    public MapData LoadFromDisc(string name)
    {
        var trackData = File.ReadAllText(Application.dataPath + "/Tracks/" + name + ".json");
        return JsonUtility.FromJson<MapData>(trackData);
    }

    public void SaveToPrefs()
    {
        Session.Instance.SetString("editorMap", GetJsonTrack());
    }

    public MapData LoadFromPrefs()
    {
        if (Session.Instance.HasKey("editorMap"))
        {
            return JsonUtility.FromJson<MapData>(Session.Instance.GetString("editorMap"));
        }
        else
        {
            return null;
        }

    }

}
