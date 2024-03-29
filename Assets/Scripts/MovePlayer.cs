using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    
    [Header("Камера игрока")] public Camera playerCamera;
    
    private float rotationX, rotationY;
    private Rigidbody rb;
    private bool contactIsNormal = true;


    private void Awake()
    {
        Settings.positionPlayer = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Settings.currentTime = Settings.maxTime;
    }
    
    bool isPause()
    {
        if (Time.timeScale == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        if (Settings.currentTime <= 0)
        {
            Interface.Instance.menu.SetActive(true);
            Interface.Instance.gameOver.SetActive(true);
            Interface.Instance.labelRestart.SetActive(true);
            Interface.Instance.labelPlay.SetActive(false);
            Interface.Instance.labelContinue.SetActive(false);
            Time.timeScale = 0;
            Settings.ShowCursor();
        }

        if (!isPause())
        {
            Settings.positionPlayer = transform.position;

            rotationX -= Input.GetAxis("Mouse Y") * Settings.sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, Settings.minVert, Settings.maxVert);
            rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Settings.sensitivityHor;
            
            //Поворачиваем игрока только вокруг оси Y
            transform.localEulerAngles = new Vector3(0f, rotationY, 0f);
            
            //Камера - дочерний объект игрока, поэтому вращаем её только вокруг оси X
            playerCamera.transform.localEulerAngles = new Vector3(rotationX, 0f, 0f);
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up || collision.gameObject.tag == "JumpIsAllowed")
        {
            contactIsNormal = true;
        }
    }

    private void FixedUpdate()
    {
        if (!Interface.Instance.menu.activeSelf)
        {
            if ((Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space)) && contactIsNormal)
            {
                contactIsNormal = false;
                rb.AddForce(Vector3.up * Settings.forceJump, ForceMode.Impulse);
            }
        }

        Vector3 movement = new Vector3(0f, rb.velocity.y - 0.5f, 0f);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movement += transform.forward * Settings.speedPlayer;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movement -= transform.forward * Settings.speedPlayer;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement -= transform.right * Settings.speedPlayer;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement += transform.right * Settings.speedPlayer;
        }

        rb.velocity = movement;
        if (Settings.currentTime >= 0)
        {
            Settings.currentTime -= 1 * Time.deltaTime;
        }
        
        Interface.Instance.time.text = ((int)Settings.currentTime).ToString();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            Interface.Instance.menu.SetActive(true);
            Interface.Instance.victory.SetActive(true);
            Interface.Instance.labelRestart.SetActive(true);
            Interface.Instance.labelPlay.SetActive(false);
            Interface.Instance.labelContinue.SetActive(false);
            Time.timeScale = 0;
            Settings.ShowCursor();
        }
    }
}
