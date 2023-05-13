using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button_Description : MonoBehaviour
{
    private Button Clickable;
    private TMP_Text Text;

    private void Awake()
    {
        var button = transform.Find("Button");
        var text = transform.Find("Text");

        Clickable = button.GetComponent<Button>();
        Text = text.GetComponent<TMP_Text>();


    }

    public void SetButtonText(string text){
        Text.text = text;
    }

    public void SetButtonImage(Sprite sprite){
        Clickable.image.sprite = sprite; 
    }

    public void OnClick(UnityAction action)
    {
        Clickable.onClick.AddListener(action);
    }
}
