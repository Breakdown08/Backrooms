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
    public GameObject lamp_prefab;
    public GameObject end_prefab;
    public GameObject Level;
    public GameObject Player;
    private const string destPathFloor = "/Level/Floor";
    private const string destPathCeiling = "/Level/Ceiling";
    public List<Coordinate> coordinates = new List<Coordinate>();
    Vector3 endPosition;



    public void FindEndPosition()
    {
        Vector3 startPosition = coordinates[0].position;
        float farthestDistance = 0f;
        Coordinate farthestCoordinate = coordinates[0];
        for (int i = 1; i < coordinates.Count; i++)
        {
            Coordinate coord = coordinates[i];
            float distance = Vector3.Distance(startPosition, coord.position);

            if (distance > farthestDistance)
            {
                farthestDistance = distance;
                farthestCoordinate = coord;
            }
        }
        farthestCoordinate.tag = "end";
    }

    private void Awake()
    {
        Instance = this;
    }


    IEnumerator WaitForDeleteAndBuild()
    {
        yield return new WaitForEndOfFrame();
        coordinates = new List<Coordinate>();
        Corridors.Generate();
        GameObject objToSpawn;
        objToSpawn = new GameObject("Ceiling");
        objToSpawn.transform.SetParent(transform.Find("/Level"), false);
        objToSpawn = new GameObject("Floor");
        objToSpawn.transform.SetParent(transform.Find("/Level"), false);
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
        //Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        //Type type = assembly.GetType("UnityEditor.LogEntries");
        //MethodInfo method = type.GetMethod("Clear");
        //method.Invoke(new object(), null);

    }

    private void Build_area()
    {
        GameObject ceiling;
        int i = 0;
        Clean_dublicates();
        FindEndPosition();
        foreach (Coordinate coord in coordinates)
        {
            i++;
            if (coord.tag == "corridor")
            {
                GameObject plane = Instantiate(coorridor_prefab, coord.position, Quaternion.identity) as GameObject;
                plane.transform.SetParent(transform.Find(destPathFloor), false);
            }
            if (coord.tag == "room")
            {
                GameObject plane = Instantiate(room_prefab, coord.position, Quaternion.identity) as GameObject;
                plane.transform.SetParent(transform.Find(destPathFloor), false);
            }
            if (coord.tag == "end")
            {
                GameObject plane = Instantiate(end_prefab, coord.position, Quaternion.identity) as GameObject;
                plane.transform.SetParent(transform.Find(destPathFloor), false);
            }
            if (i % 2 == 0)
            {
                ceiling = Instantiate(lamp_prefab, coord.position + new Vector3(0f, 2.8f, 0f), Quaternion.identity) as GameObject;
            }
            else 
            {
                ceiling = Instantiate(ceiling_prefab, coord.position + new Vector3(0f, 2.8f, 0f), Quaternion.identity) as GameObject;
            }
            ceiling.transform.SetParent(transform.Find(destPathCeiling), false);
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

    public void Re_build()
    {
        Player.transform.position = new Vector3(0f, 1f, 0f);
        Player.transform.eulerAngles = new Vector3(0f, 0f, 0f);
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
        //if (Input.GetKeyDown(KeyCode.R))
        //{
            //Re_build();
        //}
    }
}
