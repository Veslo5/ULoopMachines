using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapEditorInput : MonoBehaviour
{
    private PlayInput playerInput;

    private void Awake()
    {
        playerInput = GameObject.Find("**PLAYER**/Player1Controller").GetComponent<PlayInput>();
    }


    private void Start()
    {
        playerInput.LeftClickAction.Up.AddListener(LeftClick);
        playerInput.SpaceAction.Pressed.AddListener(SpaceDragStart);
        playerInput.SpaceAction.Up.AddListener(SpaceDragEnd);
    }

    private void SpaceDragStart()
    {
        MapEditor.Instance.MapeEditorView.StartDrag(Mouse.current.position.value);
    }

    private void SpaceDragEnd()
    {
        MapEditor.Instance.MapeEditorView.EndDrag(Mouse.current.position.value);

    }

    private void LeftClick()
    {
        MapEditor.Instance.SelectEditorObject(Mouse.current.position.value);
    }

    void Update()
    {
        //RightClick
        if (playerInput.RightClickAction.Drag)
        {
            MapEditor.Instance.MoveEditorObject(Mouse.current.position.value);
        }

        // Scroll
        if (playerInput.Scroll > 0)
        {
            MapEditor.Instance.MapeEditorView.Zoom();
        }
        else if (playerInput.Scroll < 0)
        {
            MapEditor.Instance.MapeEditorView.UnZoom();
        }

        MapEditor.Instance.MapeEditorView.CustomUpdate(Mouse.current.position.value);
    }
}
