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
    [Header("Интерфейс (время)")] public Text time;
    [Header("Интерфейс (осталось светильников)")] public Text ammo;
    [Header("Интерфейс (счет)")] public GameObject counter;

    [Header("Интерфейс (играть)")] public GameObject labelPlay;
    [Header("Интерфейс (рестарт)")] public GameObject labelRestart;
    [Header("Интерфейс (вернуться в игру)")] public GameObject labelContinue;
    bool firstStart = true;
    private void Awake()
    {
        Instance = this;
        Settings.ResetGame();
        Time.timeScale = 0;
        Settings.ShowCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver.activeSelf && firstStart == false)
        {
            menu.SetActive(!menu.activeSelf);
            if (menu.activeSelf)
            {
                Time.timeScale = 0;
                Settings.ShowCursor();
                counter.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                Settings.HideCursor();
                counter.SetActive(true);
            }
        }
    }
}
