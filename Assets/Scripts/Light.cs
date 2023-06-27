using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [Header(("Интервал мерцания"))] [Range(5, 100)] public int interval = 10;
    public GameObject light;
    void TrySpot()
    {
        light.SetActive(!light.activeSelf);
    }
    
    
    void Start()
    {
        Invoke("TrySpot", interval);
    }

    void Update()
    {

    }
}