using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayInput : MonoBehaviour
{
    public float SteeringInput;
    public float AccelerationInput;

    public InputActionValueHolder LeftClickAction;

    public InputActionValueHolder RightClickAction;

    public InputActionValueHolder SpaceAction;

    public float Scroll = 0;

    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        RightClickAction = new InputActionValueHolder(input.actions["RIGHTCLICK"]);
        LeftClickAction = new InputActionValueHolder(input.actions["CLICK"]);
        SpaceAction = new InputActionValueHolder(input.actions["SPACE"]);

    }


    // Update is called once per frame
    void Update()
    {
        SteeringInput = input.actions["Horizontal"].ReadValue<float>();
        AccelerationInput = input.actions["Vertical"].ReadValue<float>();
        Scroll =  input.actions["SCROLL"].ReadValue<float>();
        
        LeftClickAction.Click = false;
        RightClickAction.Click = false;
        SpaceAction.Click = false;
    }
}
