using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoadType
{
    STRAIGH = 0,
    CURVE = 1
}


public class RoadSpawner
{
    private Dictionary<string, GameObject> roadObjects = new Dictionary<string, GameObject>();
    int objectID = 0;

    public void Create(RoadType roadtype, string set)
    {

        objectID++;

        var name = "road" + objectID;
        var go = new GameObject(name);
        var spriteRenderer = go.AddComponent<SpriteRenderer>();

        switch (roadtype)
        {
            case RoadType.STRAIGH:
                spriteRenderer.sprite = GameResources.Instance.GetSprite(set, GameResources.MEDIUM1);
                break;
            case RoadType.CURVE:
                spriteRenderer.sprite = GameResources.Instance.GetSprite(set, GameResources.ROUND901);
                break;
        }


        var size = new Vector2(spriteRenderer.sprite.texture.width / 100f, spriteRenderer.sprite.texture.height / 100f);

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
