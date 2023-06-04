using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public static Interface Instance { get; private set; } //Паттер синглтон

    [Header("Интерфейс (меню)")] public GameObject menu;
    [Header("Интерфейс (поражение)")] public GameObject gameOver;
    [Header("Интерфейс (победа)")] public GameObject victory;
    [Header("Интерфейс (здоровье)")] public List<GameObject> healthbar = new List<GameObject>();
    [Header("Интерфейс (патроны игрока)")] public Text ammo;
    [Header("Интерфейс (уничтожено врагов)")] public Text killed;
    private void Awake()
    {
        Instance = this;
        Settings.ResetGame();
        Settings.HideCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver.activeSelf)
        {
            menu.SetActive(!menu.activeSelf);
            if (menu.activeSelf)
            {
                Time.timeScale = 0;
                Settings.ShowCursor();
            }
            else
            {
                Time.timeScale = 1;
                Settings.HideCursor();
            }
        }
    }
}
