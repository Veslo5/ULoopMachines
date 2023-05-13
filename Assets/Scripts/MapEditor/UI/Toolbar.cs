using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    private Button Inspector;
    private Button Parts;
    private void Awake() {
        var inspectorGo = transform.Find("Button_Inspector");
        var partsGo = transform.Find("Button_Parts");

        Inspector = inspectorGo.GetComponent<Button>();
        Parts = inspectorGo.GetComponent<Button>();
    }


    public void InspectorClick(UnityAction action) => Inspector.onClick.AddListener(action);
    public void PartsClick(UnityAction action) => Parts.onClick.AddListener(action);
}
