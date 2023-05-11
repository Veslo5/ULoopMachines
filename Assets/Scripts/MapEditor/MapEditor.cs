using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapEditor : Singleton<MapEditor>
{
    public MapSpawner MapSpawner;
    public MapLoader MapLoader;
    public MapEditorView MapEditorView;

    public Vector2 SnappingGridSize = new Vector2(8, 8); 

    public GameObject Selector = null;
    public GameObject CurrentSelectedObject = null;

    public Camera EditorCamera;
    

    public UnityEvent<GameObject> CurrentObjectSelectionChanged;
    public UnityEvent<GameObject> CurrentObjectSelectionMoved;


    public override void Awake()
    {
        base.Awake();

        MapSpawner = new MapSpawner();

        EditorCamera = Camera.main;        

        MapEditorView = GetComponent<MapEditorView>();

        CurrentObjectSelectionChanged = new UnityEvent<GameObject>();

        MapLoader = MapLoader.FindMe();

        Selector = GameObject.Find("EditorSelector");
        Selector.SetActive(false);
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

                        Selector.transform.position = CurrentSelectedObject.transform.position;
                        var collider = CurrentSelectedObject.GetComponent<BoxCollider2D>();

                        Selector.GetComponent<SpriteRenderer>().size = collider.size + new Vector2(0.01f,0.01f);
                        Selector.SetActive(true);
                    }

                    break;
                }
            }
        }else{
            CurrentSelectedObject = null;
            Selector.SetActive(false);
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

            Selector.transform.position = pos;
            CurrentSelectedObject.transform.position = pos;

        }
    }

}
