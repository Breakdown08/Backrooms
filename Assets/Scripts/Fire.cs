using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Камера игрока")] public Camera playerCamera;
    [Header("Префаб патрона")] public GameObject ammoPref;

    [Header("Скорость патрона")] [Range(5, 50)]
    public int speedAmmo = 20;
    

    void CreateAmmo()
    {
        if (Settings.countAmmo > 0)
        {
            Settings.countAmmo--;
            Interface.Instance.ammo.text = Settings.countAmmo.ToString();
            
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                float x = hit.point.x - playerCamera.transform.position.x;
                float y = hit.point.y - playerCamera.transform.position.y;
                float z = hit.point.z - playerCamera.transform.position.z;

                Vector3 direction = new Vector3(x, y, z).normalized;

                GameObject ammo = Instantiate(ammoPref, playerCamera.transform.position, Quaternion.identity) as GameObject;
                ammo.GetComponent<Rigidbody>().AddForce(direction * speedAmmo, ForceMode.Impulse);
            }
            else
            {
                GameObject ammo = Instantiate(ammoPref, playerCamera.transform.position, Quaternion.identity) as GameObject;
                ammo.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * speedAmmo, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AmmoEnemy")
        {
            Settings.playerHealth--;
            if (Settings.playerHealth < 0)
            {
                Settings.playerHealth = 0;
            }
            Interface.Instance.healthbar[Settings.playerHealth].SetActive(false); //текущий элемент скрываем
            if (Settings.playerHealth == 0)
            {
                Interface.Instance.menu.SetActive(true);
                Interface.Instance.gameOver.SetActive(true);
                Settings.ShowCursor();
            }
        }
        if (other.tag == "Medicine")
        {
            if (Settings.playerHealth < Settings.maxPlayerHealth)
            {
                if (Settings.playerHealth > Settings.maxPlayerHealth)
                {
                    Settings.playerHealth = Settings.maxPlayerHealth;
                }
                Interface.Instance.healthbar[Settings.playerHealth].SetActive(true);
                Settings.playerHealth++;
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "Box")
        {
            if (Settings.countAmmo < Settings.maxCountAmmo)
            {
                Settings.countAmmo += 5;
                if (Settings.countAmmo > Settings.maxCountAmmo)
                {
                    Settings.countAmmo = Settings.maxCountAmmo;
                }
                Interface.Instance.ammo.text = Settings.countAmmo.ToString();
                Destroy(other.gameObject);
            }
        }
    }
    
    void Start()
    {
        Settings.countAmmo = Settings.maxCountAmmo;
        Interface.Instance.ammo.text = Settings.countAmmo.ToString();
    }

    void Update()
    {
        if (!Interface.Instance.menu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
            {
                CreateAmmo();
                
            }
        }
    }
}