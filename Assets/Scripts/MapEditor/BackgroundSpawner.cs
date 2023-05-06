using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner
{
    private Dictionary<string, GameObject> editorObjects = new Dictionary<string, GameObject>();
    int objectID = 0;


    public void Create()
    {
        objectID++;
        var name = "background" + objectID;
        var go = new GameObject(name);
        var spriteRenderer = go.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = MapEditorResources.Instance.BasicTexture;

        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        spriteRenderer.size = new Vector2(1, 1);
        spriteRenderer.sortingOrder = 0;


        var collider = go.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);
        collider.isTrigger = true;

        go.tag = "editor";
        Vector3 lookAtPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        go.transform.position = new Vector3(lookAtPosition.x, lookAtPosition.y, 0);

        editorObjects.Add(name, go);
    }

}
