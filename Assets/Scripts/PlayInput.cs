using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayInput : MonoBehaviour
{
    PlayerInput input;
    public float steeringInput;
    public float accelerationInput;
    public bool click;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        steeringInput = input.actions["Horizontal"].ReadValue<float>();
        accelerationInput = input.actions["Vertical"].ReadValue<float>();
        click = input.actions["CLICK"].triggered;
        
    }
}
