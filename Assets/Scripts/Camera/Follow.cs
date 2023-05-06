using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public Camera CurrentCamera;

    public GameObject Following;

    // Start is called before the first frame update
    void Start()
    {
        CurrentCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentCamera.transform.position = Following.transform.position + new Vector3(0,0, -10);
    }
}
