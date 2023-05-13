using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_TrackParts : MonoBehaviour
{


    public GameObject ButtonTemplate;

    private GameObject CurrentBackground;

    private Button TrackParts;
    private Button Backgrounds;
    private Button Obstacles;
    private Button Colliders;
    private Button Specials;

    private void Awake()
    {
        CurrentBackground = transform.Find("Background_TrackParts").gameObject;
        var titleGo = transform.Find("Title");

        var trackPartsGo = titleGo.transform.Find("Button_TrackParts");
        TrackParts = trackPartsGo.GetComponent<Button>();

        var backgroundsGo = titleGo.transform.Find("Button_Background");
        Backgrounds = backgroundsGo.GetComponent<Button>();

        var obstaclesGo = titleGo.transform.Find("Button_Obstacles");
        Obstacles = obstaclesGo.GetComponent<Button>();

        var collidersGo = titleGo.transform.Find("Button_Colliders");
        Colliders = collidersGo.GetComponent<Button>();

        var specialsGo = titleGo.transform.Find("Button_Specials");
        Specials = specialsGo.GetComponent<Button>();


    }

    public Button_Description AddButton(string text, Sprite image)
    {
        var obj = Instantiate(ButtonTemplate, Vector3.zero, Quaternion.identity, CurrentBackground.transform);
        var btnDesc =  obj.GetComponent<Button_Description>();

        btnDesc.SetButtonImage(image);
        btnDesc.SetButtonText(text);

        return btnDesc;
    }

}
