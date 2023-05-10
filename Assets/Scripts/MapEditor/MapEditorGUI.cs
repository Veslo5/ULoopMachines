using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEditorGUI : MonoBehaviour
{


    private Rect buildWindowRect = new Rect(Screen.width - 240, 20, 220, 500);
    public string SetName = "Prototype";
    public string positionX, positionY;
    public string sizeX, sizeY;

    private void Awake()
    {
    }

    private void Start()
    {

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


    private void buildWindow(int windowID)
    {

        var editor = MapEditor.Instance;
        if (GUI.Button(new Rect(10, 20, 50, 50), "S"))
        {
            editor.MapSpawner.SpawnRoad(MapParts.small_1, SetName, true);
        }

        if (GUI.Button(new Rect(60, 20, 50, 50), "M"))
        {
            editor.MapSpawner.SpawnRoad(MapParts.medium_1, SetName, true);
        }

        if (GUI.Button(new Rect(110, 20, 50, 50), "L"))
        {
            editor.MapSpawner.SpawnRoad(MapParts.large_1, SetName, true);
        }

        if (GUI.Button(new Rect(10, 70, 50, 50), "90S"))
        {
            editor.MapSpawner.SpawnRoad(MapParts.round90_s_1, SetName, true);
        }

        if (GUI.Button(new Rect(60, 70, 50, 50), "90M"))
        {
            editor.MapSpawner.SpawnRoad(MapParts.round90_m_1, SetName, true);
        }

        if (GUI.Button(new Rect(10, 120, 50, 50), "TRNMS"))
        {
            editor.MapSpawner.SpawnRoad(MapParts.transition_ms_1, SetName, true);
        }

        if (GUI.Button(new Rect(60, 120, 50, 50), "TRNML"))
        {
            editor.MapSpawner.SpawnRoad(MapParts.transition_ml_1, SetName, true);
        }

        if (GUI.Button(new Rect(10, 170, 50, 50), "BCKG"))
        {
            editor.MapSpawner.SpawnBackground(SetName, true);
        }

        GUI.TextField(new Rect(10, 460, 180, 30), SetName);

        GUI.DragWindow(new Rect(0, 0, 10000, 20));

    }

    void OnGUI()
    {

        buildWindowRect = GUI.Window(0, buildWindowRect, buildWindow, "Building parts");

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


            GUI.Label(new Rect(10, 190, 60, 30), "Rotate:");
            if (GUI.Button(new Rect(70, 190, 40, 30), "+90"))
            {
                MapEditor.Instance.CurrentSelectedObject.transform.Rotate(new Vector3(0, 0, 90));
            }

            if (GUI.Button(new Rect(120, 190, 40, 30), "-90"))
            {
                MapEditor.Instance.CurrentSelectedObject.transform.Rotate(new Vector3(0, 0, -90));
            }

            if (GUI.Button(new Rect(10, 230, 100, 30), "Apply"))
            {
                //100 = pixel per unit 

                var spriteRenderer = MapEditor.Instance.CurrentSelectedObject.GetComponent<SpriteRenderer>();
                var collider = MapEditor.Instance.CurrentSelectedObject.GetComponent<BoxCollider2D>();
                var size = new Vector2(Convert.ToInt32(sizeX) / 100f, Convert.ToInt32(sizeY) / 100f);
                spriteRenderer.size = size;
                collider.size = size;

                MapEditor.Instance.CurrentSelectedObject.transform.position = new Vector3(Convert.ToInt32(positionX), Convert.ToInt32(positionX), 0);
            }

            if (GUI.Button(new Rect(110, 230, 100, 30), "Deselect"))
            {
                MapEditor.Instance.CurrentSelectedObject = null;
            }

        }

        if (GUI.Button(new Rect((Screen.width / 2) - 50, 20, 100, 30), "Test Map"))
        {
            var mapLoader = MapEditor.Instance.MapLoader;
            Debug.Log(mapLoader.GetJsonTrack());
            mapLoader.SaveToPrefs();
            SceneManager.LoadScene("Track");
        }

    }

}
