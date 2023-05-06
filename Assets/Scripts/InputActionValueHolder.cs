using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputActionValueHolder
{

    public bool Click = false;
    public bool Drag = false;

    public UnityEvent Pressed;
    public UnityEvent Up;

    private InputAction action;

    public InputActionValueHolder(InputAction action)
    {
        this.action = action;
        action.started += actionStarted;
        action.canceled += actionEnded;

        Pressed = new UnityEvent();
        Up = new UnityEvent();
    }

    private void actionStarted(InputAction.CallbackContext context)
    {
        Drag = true;
        Pressed.Invoke();
    }

    private void actionEnded(InputAction.CallbackContext context)
    {
        Click = true;
        Drag = false;
        Up.Invoke();
    }
}
