using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class DecalManager : Singleton<DecalManager>
{

    public PlayerInput input;

    public Dictionary<string, GameObject> Decals = new Dictionary<string, GameObject>();
    public GameObject CurrentSelectedDecal = null;

    public override void Awake()
    {   
        base.Awake();

        input = GetComponent<PlayerInput>();
        var click = input.actions["CLICK"];
        click.started += ClickStart;
        

        var decal = Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Decals/Decal01.prefab");
        var go = decal.WaitForCompletion();
        Decals.Add(go.name, go);
    }

    private void ClickStart(InputAction.CallbackContext obj)
    {
        Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10,0,100,40), "Decal01"))
        {
            Instantiate(Decals["Decal01"], new Vector3(0,0,0), new Quaternion(0,0,0,0));
        }
    }
}
