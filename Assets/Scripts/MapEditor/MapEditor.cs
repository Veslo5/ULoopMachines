using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapEditor : Singleton<MapEditor>
{
    public Vector2 SnappingGridSize = new Vector2(16, 16); 
    public BackgroundSpawner BackgroundSpawner;
    public RoadSpawner RoadSpawner;
    public GameObject CurrentSelectedObject = null;
    public Camera EditorCamera;
    public MapEditorView MapeEditorView;

    public UnityEvent<GameObject> CurrentObjectSelectionChanged;
    public UnityEvent<GameObject> CurrentObjectSelectionMoved;


    public override void Awake()
    {
        base.Awake();

        BackgroundSpawner = new BackgroundSpawner();
        RoadSpawner = new RoadSpawner();

        EditorCamera = Camera.main;        

        MapeEditorView = GetComponent<MapEditorView>();

        CurrentObjectSelectionChanged = new UnityEvent<GameObject>();
    }



    public void SelectEditorObject(Vector2 mousePos)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(EditorCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0)), Vector2.zero);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                var firsthit = hits[i];
                if (firsthit.collider != null)
                {
                    var go = firsthit.collider.gameObject;
                    if (go.CompareTag("editor"))
                    {
                        CurrentSelectedObject = go;

                        this.CurrentObjectSelectionChanged.Invoke(CurrentSelectedObject);
                    }

                    break;
                }
            }
        }
    }

    public void MoveEditorObject(Vector2 mousePos)
    {
        if (CurrentSelectedObject != null)
        {
            var pos = EditorCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
            pos.z = 0;

            pos.x = Mathf.RoundToInt((pos.x * 100) / SnappingGridSize.x) * SnappingGridSize.x;
            pos.y = Mathf.RoundToInt((pos.y * 100) / SnappingGridSize.y) * SnappingGridSize.y;
            pos.x = pos.x / 100f;
            pos.y = pos.y / 100f;

            CurrentSelectedObject.transform.position = pos;

        }
    }

}
