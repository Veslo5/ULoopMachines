using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private Material gridMaterial;
    private void Awake()
    {

        gridMaterial = this.GetComponent<MeshRenderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        var camera = Camera.main;

        // this.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, 10);

        if (MapEditor.Instance.CurrentSelectedObject != null)
        {
            this.transform.position = MapEditor.Instance.CurrentSelectedObject.transform.position;
        }


        var scale = (Screen.height / 2.0f) / Camera.main.orthographicSize;

        this.transform.localScale = new Vector3(Screen.width / scale, Screen.width / scale, 1);
        gridMaterial.SetFloat("_Scale", camera.orthographicSize);

    }
}
