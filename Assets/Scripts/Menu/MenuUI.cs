using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private Button btn_Play;
    private Button btn_MapEditor;
    private Button btn_Quit;


    private void Awake()
    {

        btn_Play = GameObject.Find("Canvas/Btn_Play").GetComponent<Button>();
        btn_MapEditor = GameObject.Find("Canvas/Btn_MapEditor").GetComponent<Button>();
        btn_Quit = GameObject.Find("Canvas/Btn_Quit").GetComponent<Button>();

        btn_Play.onClick.AddListener(btn_Play_Click);
        btn_MapEditor.onClick.AddListener(btn_MapEditor_Click);
        btn_Quit.onClick.AddListener(btn_Quit_Click);

    }

    private void btn_Quit_Click()
    {   
        Application.Quit();
    }

    private void btn_MapEditor_Click()
    {
        Debug.Log("test");
        SceneManager.LoadScene("MapEditor");
    }

    private void btn_Play_Click()
    {
        SceneManager.LoadScene("MapEditor");
    }
}
