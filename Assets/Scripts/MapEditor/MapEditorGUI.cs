using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorGUI : MonoBehaviour
{
    public string positionX, positionY;
    public string sizeX, sizeY;

    private void Awake()
    {
    }

    private void Start() {
        
        MapEditor.Instance.CurrentObjectSelectionChanged.AddListener(ObjectSelectionChanged);
    }

    private void ObjectSelectionChanged(GameObject newObject)
    {

        var spriteRenderer = newObject.GetComponent<SpriteRenderer>();

        positionX = newObject.transform.position.x.ToString();
        positionY = newObject.transform.position.y.ToString();
        sizeX = (spriteRenderer.size.x * 100).ToString();
        sizeY = (spriteRenderer.size.y * 100).ToString();
    }


    void OnGUI()
    {

        if (GUI.Button(new Rect(10, 10, 100, 30), "Spawn road"))
        {
            MapEditor.Instance.RoadSpawner.Create();
        }

        if (GUI.Button(new Rect(10, 40, 200, 30), "Spawn background"))
        {
            MapEditor.Instance.BackgroundSpawner.Create();
        }

        if (MapEditor.Instance.CurrentSelectedObject != null)
        {

            GUI.Label(new Rect(10, 80, 200, 30), "Name: " + MapEditor.Instance.CurrentSelectedObject.name);

            GUI.Label(new Rect(10, 110, 60, 30), "Position:");
            positionX = GUI.TextField(new Rect(70, 110, 50, 30), positionX);
            positionY = GUI.TextField(new Rect(120, 110, 50, 30), positionY);
            // GUI.TextField(new Rect(170, 110, 50, 30), "0");

            GUI.Label(new Rect(10, 150, 60, 30), "Size(px):");
            sizeX = GUI.TextField(new Rect(70, 150, 50, 30), sizeX);
            sizeY = GUI.TextField(new Rect(120, 150, 50, 30), sizeY);
            // GUI.TextField(new Rect(170, 150, 50, 30), "0");

            if (GUI.Button(new Rect(10, 190, 100, 30), "Apply"))
            {
                //100 = pixel per unit 

                var spriteRenderer = MapEditor.Instance.CurrentSelectedObject.GetComponent<SpriteRenderer>();
                var collider = MapEditor.Instance.CurrentSelectedObject.GetComponent<BoxCollider2D>();
                var size = new Vector2(Convert.ToInt32(sizeX) / 100f, Convert.ToInt32(sizeY) / 100f);
                spriteRenderer.size = size;
                collider.size = size;

                MapEditor.Instance.CurrentSelectedObject.transform.position = new Vector3(Convert.ToInt32(positionX), Convert.ToInt32(positionX), 0);
            }

            if (GUI.Button(new Rect(110, 190, 100, 30), "Deselect"))
            {
                MapEditor.Instance.CurrentSelectedObject = null;
            }


        }

    }

}