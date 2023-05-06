using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapEditor : Singleton<MapEditor>
{
    public BackgroundSpawner BackgroundSpawner;

    public GameObject CurrentSelectedObject = null;

    public PlayInput playerInput;


    private string positionX, positionY;
    private string sizeX, sizeY;


    public override void Awake()
    {
        base.Awake();

        BackgroundSpawner = new BackgroundSpawner();
        playerInput = GameObject.Find("**PLAYER**/Player1Controller").GetComponent<PlayInput>();
    }


    void Update()
    {
        if (playerInput.click)
        {
            var mousePos = Mouse.current.position.value;

            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0)), Vector2.zero);

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    var firsthit = hits[i];
                    if (firsthit.collider != null)
                    {
                        var go = firsthit.collider.gameObject;
                        Debug.Log(go.name);
                        if (go.CompareTag("editor"))
                        {
                            CurrentSelectedObject = go;

                            positionX = CurrentSelectedObject.transform.position.x.ToString();
                            positionY = CurrentSelectedObject.transform.position.y.ToString();
                            sizeX = (100 / CurrentSelectedObject.transform.localScale.x).ToString();
                            sizeY = (100 / CurrentSelectedObject.transform.localScale.y).ToString();

                        }
                        break;
                    }
                }
            }
        }
    }

    void OnGUI()
    {

        if (GUI.Button(new Rect(10, 10, 100, 30), "Spawn road"))
        {
        }

        if (GUI.Button(new Rect(10, 40, 200, 30), "Spawn background"))
        {
            BackgroundSpawner.Create();
        }

        if (CurrentSelectedObject != null)
        {

            GUI.Label(new Rect(10, 80, 200, 30), "Name: " + CurrentSelectedObject.name);

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
                CurrentSelectedObject.transform.position = new Vector3(Convert.ToInt32(positionX), Convert.ToInt32(positionX), 0);                
                CurrentSelectedObject.transform.localScale = new Vector3(Convert.ToInt32(sizeX) / 100f, Convert.ToInt32(sizeY) / 100f, 0);
            }

            if (GUI.Button(new Rect(110, 190, 100, 30), "Deselect"))
            {
                CurrentSelectedObject = null;
            }


        }

    }
}
