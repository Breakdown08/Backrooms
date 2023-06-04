using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


public class Generation : MonoBehaviour
{


    public static Generation Instance { get; private set; } //ПАТТЕРН СИНГЛТОН
    public GameObject coorridor_prefab;
    public GameObject room_prefab;
    public GameObject ceiling_prefab;
    public GameObject Level;
    private const string destPathFloor = "/Level/Floor";
    private const string destPathCeiling = "/Level/Ceiling";
    public List<Coordinate> coordinates = new List<Coordinate>();


    private void Awake()
    {
        Instance = this;
    }


    IEnumerator WaitForDeleteAndBuild()
    {
        yield return new WaitForEndOfFrame();
        coordinates = new List<Coordinate>();
        Corridors.Generate();
        Build_area();
    }

    public int Chance()
    {
        int percent = 70;
        int res = UnityEngine.Random.Range(1, 101);
        int range = UnityEngine.Random.Range(11, 15);
        if (res <= percent) return 2; else return range;
    }

    public int Chance_room()
    {
        int percent = 5;
        int res = UnityEngine.Random.Range(1, 101);
        if (res <= percent) return 1; else return 0;
    }



    public static void ClearLog()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type type = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);

    }

    private void Build_area()
    {
        Clean_dublicates();
        foreach (Coordinate coord in coordinates)
        {
            if (coord.tag == "corridor")
            {
                GameObject plane = Instantiate(coorridor_prefab, coord.position, Quaternion.identity) as GameObject;
                plane.transform.SetParent(transform.Find(destPathFloor), false);
                GameObject ceiling = Instantiate(ceiling_prefab, coord.position + new Vector3(0f, 2.8f, 0f), Quaternion.identity) as GameObject;
                ceiling.transform.SetParent(transform.Find(destPathCeiling), false);
            }
            if (coord.tag == "room")
            {
                GameObject plane = Instantiate(room_prefab, coord.position, Quaternion.identity) as GameObject;
                plane.transform.SetParent(transform.Find(destPathFloor), false);
                GameObject ceiling = Instantiate(ceiling_prefab, coord.position + new Vector3(0f, 2.8f, 0f), Quaternion.identity) as GameObject;
                ceiling.transform.SetParent(transform.Find(destPathCeiling), false);
            }
        }
    }

    private void Clean_dublicates()
    {
        for (int i = 0; i < coordinates.Count; i++)
        {
            int have_dublicate = 0;
            for (int j = 0; j < coordinates.Count; j++)
            {
                if (coordinates[i].position == coordinates[j].position)
                {
                    have_dublicate++;
                    if (have_dublicate > 1)
                    {
                        if (coordinates[j].tag == "room") //чтобы комната не затиралась коридором, а наоборот
                        {
                            coordinates[i].position = new Vector3(coordinates[i].position.x, -500f, coordinates[i].position.z);
                        }
                        else
                        {
                            coordinates[j].position = new Vector3(coordinates[j].position.x, -500f, coordinates[j].position.z);
                        }
                    }
                }
            }
        }

        int check = 0;
        while (check < coordinates.Count)
        {
            if (coordinates[check].position.y == -500f)
            {
                coordinates.Remove(coordinates[check]);
                check = 0;
            }
            else check++;
        }
        for (int i = 0; i < coordinates.Count; i++) //поднимаем пустоту по оси Y
        {
            if (coordinates[i].tag == "void") coordinates[i].position = new Vector3(coordinates[i].position.x, 5.01f, coordinates[i].position.z);
        }
    }

    private void Re_build()
    {
        ClearLog();
        int n = Level.transform.childCount;
        for (int i = 0; i < n; i++)
        {
            Destroy(Level.transform.GetChild(i).gameObject);
        }
        StartCoroutine(WaitForDeleteAndBuild());
    }


    void Start()
    {
        ClearLog();
        Corridors.Generate();
        //BuildVoid.Build_Void();
        Build_area();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Re_build();
        }
    }
}
