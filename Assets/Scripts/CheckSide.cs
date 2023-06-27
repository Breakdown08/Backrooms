using System.Collections;
using UnityEngine;
using System;

public class CheckSide : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Pillar;
    public GameObject Void;
    private float step = 4f;
    private const string destPath = "/Level/Floor";
    Vector3 origin1;
    Vector3 origin2;
    Vector3 origin3;
    Vector3 origin4;
    Vector3 origin5;
    Vector3 origin6;
    Vector3 origin7;
    Vector3 origin8;
    Vector3 originVoid;

    private int len = 1;
    Vector3 direction;        //вектор луча


    void RayDraw()
    {
        origin1 = new Vector3(transform.position.x, 1f, transform.position.z + step);
        origin2 = new Vector3(transform.position.x + step, 1f, transform.position.z);
        origin3 = new Vector3(transform.position.x, 1f, transform.position.z - step);
        origin4 = new Vector3(transform.position.x - step, 1f, transform.position.z);

        origin5 = new Vector3(transform.position.x - step, 1f, transform.position.z + step);
        origin6 = new Vector3(transform.position.x + step, 1f, transform.position.z + step);
        origin7 = new Vector3(transform.position.x - step, 1f, transform.position.z - step);
        origin8 = new Vector3(transform.position.x + step, 1f, transform.position.z - step);
        Debug.DrawRay(origin1, direction, Color.blue); //отрисовка лучей для сцены (для наглядности)
        Debug.DrawRay(origin2, direction, Color.red); //отрисовка лучей для сцены (для наглядности)
        Debug.DrawRay(origin3, direction, Color.cyan); //отрисовка лучей для сцены (для наглядности)
        Debug.DrawRay(origin4, direction, Color.yellow); //отрисовка лучей для сцены (для наглядности)
        Debug.DrawRay(origin5, direction, Color.black); //отрисовка лучей для сцены (для наглядности)
        Debug.DrawRay(origin6, direction, Color.white); //отрисовка лучей для сцены (для наглядности)
        Debug.DrawRay(origin7, direction, Color.gray); //отрисовка лучей для сцены (для наглядности)
        Debug.DrawRay(origin8, direction, Color.green); //отрисовка лучей для сцены (для наглядности)
    }
    

    void Build_walls()
    {
        origin1 = new Vector3(transform.position.x, 1f, transform.position.z + step);
        origin2 = new Vector3(transform.position.x + step, 1f, transform.position.z);
        origin3 = new Vector3(transform.position.x, 1f, transform.position.z - step);
        origin4 = new Vector3(transform.position.x - step, 1f, transform.position.z);

        origin5 = new Vector3(transform.position.x - step, 1f, transform.position.z + step);
        origin6 = new Vector3(transform.position.x + step, 1f, transform.position.z + step);
        origin7 = new Vector3(transform.position.x - step, 1f, transform.position.z - step);
        origin8 = new Vector3(transform.position.x + step, 1f, transform.position.z - step);

        direction = -transform.up * len; //задаем вектор направления лучей рейкаста

        RaycastHit hit;



        if (Physics.Raycast(origin1, direction, out hit, len) == false)
        {
            //Debug.Log("Ставлю стену спереди от "+transform.name+" от "+ transform.position);
            GameObject wall = Instantiate(Wall, new Vector3(origin1.x, 0f, origin1.z - step / 2), Quaternion.Euler(0f, 180f, 0f)) as GameObject;
            wall.transform.SetParent(transform.Find(destPath), false);
        }
        if (Physics.Raycast(origin2, direction, out hit, len) == false)
        {
            //Debug.Log("Ставлю стену справа от " + transform.name + " от " + transform.position);
            GameObject wall = Instantiate(Wall, new Vector3(origin2.x - step / 2, 0f, origin2.z), Quaternion.Euler(0f, -90f, 0f)) as GameObject;
            wall.transform.SetParent(transform.Find(destPath), false);
        }
        if (Physics.Raycast(origin3, direction, out hit, len) == false)
        {
            //Debug.Log("Ставлю стену сзади от " + transform.name + " от " + transform.position);
            GameObject wall = Instantiate(Wall, new Vector3(origin3.x, 0f, origin3.z + step / 2), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            wall.transform.SetParent(transform.Find(destPath), false);
        }
        if (Physics.Raycast(origin4, direction, out hit, len) == false)
        {
            //Debug.Log("Ставлю стену слева от " + transform.name + " от " + transform.position);
            GameObject wall = Instantiate(Wall, new Vector3(origin4.x + step / 2, 0f, origin4.z), Quaternion.Euler(0f, 90, 0f)) as GameObject;
            wall.transform.SetParent(transform.Find(destPath), false);
        }

        //ищем углы
        if (Physics.Raycast(origin6, direction, out hit, len) == false && Physics.Raycast(origin1, direction, out hit, len) && Physics.Raycast(origin2, direction, out hit, len))
        {
            //Debug.Log("Есть угол справа вверху" + transform.position);
            GameObject pillar = Instantiate(Pillar, transform.position + new Vector3(2.5f, 0f, 2.5f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            pillar.transform.SetParent(transform.Find(destPath), false);
        }
        if (Physics.Raycast(origin5, direction, out hit, len) == false && Physics.Raycast(origin1, direction, out hit, len) && Physics.Raycast(origin4, direction, out hit, len))
        {
            //Debug.Log("Есть угол слева вверху" + transform.position);
            GameObject pillar = Instantiate(Pillar, transform.position + new Vector3(-2.5f, 0f, 2.5f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            pillar.transform.SetParent(transform.Find(destPath), false);
        }
        if (Physics.Raycast(origin8, direction, out hit, len) == false && Physics.Raycast(origin2, direction, out hit, len) && Physics.Raycast(origin3, direction, out hit, len))
        {
            //Debug.Log("Есть угол справа внизу" + transform.position);
            GameObject pillar = Instantiate(Pillar, transform.position + new Vector3(2.5f, 0f, -2.5f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            pillar.transform.SetParent(transform.Find(destPath), false);
        }
        if (Physics.Raycast(origin7, direction, out hit, len) == false && Physics.Raycast(origin4, direction, out hit, len) && Physics.Raycast(origin3, direction, out hit, len))
        {
            //Debug.Log("Есть угол слева внизу" + transform.position);
            GameObject pillar = Instantiate(Pillar, transform.position + new Vector3(-2.5f, 0f, -2.5f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            pillar.transform.SetParent(transform.Find(destPath), false);
        }
    }

    private void Build_void(int layers)
    {
        direction = -transform.up * len; //задаем вектор направления лучей рейкаста
        Vector3 startPosition = new Vector3(-step*layers , 5.01f, -step*layers)  + transform.position;
        Vector3 endPosition = new Vector3(step*layers , 5.01f, step*layers) + transform.position;
        RaycastHit hit;
        for (int i = Convert.ToInt32(startPosition.x); i <= Convert.ToInt32(endPosition.x); i+=Convert.ToInt32(step))
        {
            for (int j = Convert.ToInt32(startPosition.z); j <= Convert.ToInt32(endPosition.z); j+=Convert.ToInt32(step))
            {
                originVoid = new Vector3(i, 5.5f,j);
                if (Physics.Raycast(originVoid, direction, out hit, 5.6f) == false)
                {
                    //Instantiate(Void, new Vector3(i,5.01f,j), Quaternion.identity);
                    GameObject plane = Instantiate(Void, new Vector3(i,5.01f,j), Quaternion.identity) as GameObject;
                    plane.transform.SetParent(transform.Find(destPath), false);
                }
            }
        }
    }
    

    private void Start()
    {
        Build_walls();
        //Build_void(2);
    }

    private void Update()
    {
        //RayDraw();
        if (Input.GetKeyDown(KeyCode.T))
        {
        }
    }
}
