using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner
{
    private Dictionary<string, GameObject> roadObjects = new Dictionary<string, GameObject>();
    int objectID = 0;

    public void Create()
    {

        objectID++;

        var name = "road" + objectID;
        var go = new GameObject(name);
        var spriteRenderer = go.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = MapEditorResources.Instance.Road001;

        var size = new Vector2( spriteRenderer.sprite.texture.width / 100f,  spriteRenderer.sprite.texture.height / 100f);

        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        spriteRenderer.size = size;
        spriteRenderer.sortingOrder = 0;


        var collider = go.AddComponent<BoxCollider2D>();
        collider.size = size;
        collider.isTrigger = true;

        go.tag = "editor";
        Vector3 lookAtPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        go.transform.position = new Vector3(lookAtPosition.x, lookAtPosition.y, 0);

        roadObjects.Add(name, go);


    }

}