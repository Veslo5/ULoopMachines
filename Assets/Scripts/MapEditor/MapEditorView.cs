using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorView : MonoBehaviour
{
    public Camera EditorCamera;

    public float MinimalZoom = 3f;
    public float ZoomStep = 0.5f;

    private bool cameraDragging = false;
    private Vector3 startedMousePos;

    private void Awake()
    {
        EditorCamera = Camera.main;
    }


    public void StartDrag(Vector2 mousePos)
    {
        startedMousePos = EditorCamera.ScreenToWorldPoint(mousePos);
        cameraDragging = true;
    }

    public void EndDrag(Vector2 mousePos)
    {
        cameraDragging = false;
    }


    public void Zoom()
    {
        if (EditorCamera.orthographicSize - ZoomStep >= MinimalZoom)
        {
            EditorCamera.orthographicSize -= ZoomStep;
        }
    }

    public void UnZoom()
    {
        EditorCamera.orthographicSize += ZoomStep;
    }

    public void CustomUpdate(Vector2 mousePos)
    {

        if (cameraDragging)
        {
            var difference = EditorCamera.ScreenToWorldPoint(mousePos) - EditorCamera.transform.position;
            EditorCamera.transform.position = startedMousePos - difference;
        }

    }

}
