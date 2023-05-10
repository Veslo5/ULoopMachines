using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner
{

    public int RoadID = 0;
    public int BackgroundID = 0;

    public GameObject SpawnRoad(MapParts roadtype, string set, bool editor)
    {
        RoadID++;

        var name = "road" + RoadID;
        var go = new GameObject(name);


        string assetPath = "";

                assetPath = set + "/" + roadtype.ToString();

        var sprite = GameResources.Instance.GetSprite(set, assetPath);

        var spriteSize = addSpriteRenderer(go, sprite).size;

        if(editor)
        addEditorSettings(go, assetPath, set, roadtype.ToString(),  spriteSize, TrackPropertyType.ROAD);

        // roadObjects.Add(name, go);

        return go;

    }


    public GameObject SpawnBackground(string set, bool editor)
    {
        BackgroundID++;

        var name = "background" + RoadID;
        var go = new GameObject(name);

        string assetPath = set + "/" + MapParts.background_1;

        var sprite = GameResources.Instance.GetSprite(set, assetPath);

        var spriteSize = addSpriteRenderer(go, sprite).size;

        if(editor)
        addEditorSettings(go, assetPath, set, null, spriteSize, TrackPropertyType.BACKGROUND);

        // roadObjects.Add(name, go);

        return go;
    }


    private SpriteRenderer addSpriteRenderer(GameObject go, Sprite sprite)
    {

        var spriteRenderer = go.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        var spriteSize = new Vector2(spriteRenderer.sprite.texture.width / 100f, spriteRenderer.sprite.texture.height / 100f);

        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        spriteRenderer.size = spriteSize;
        spriteRenderer.sortingOrder = 0;

        return spriteRenderer;
    }

    private void addEditorSettings(GameObject go, string assetPath, string set, string customType, Vector2 spriteSize, TrackPropertyType type)
    {
        var prop = go.AddComponent<MapEditorProperty>();
        prop.Type = type;
        prop.SpritePath = assetPath;
        prop.Set = set;
        prop.CustomType = customType;

        var collider = go.AddComponent<BoxCollider2D>();
        collider.size = spriteSize;
        collider.isTrigger = true;

        go.tag = "editor";

        Vector3 lookAtPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        go.transform.position = new Vector3(lookAtPosition.x, lookAtPosition.y, 0);
    }    

}
