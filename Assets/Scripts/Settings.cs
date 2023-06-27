using UnityEngine;
public static class Settings
{
    public static int sensitivityVert = 3;
    public static int sensitivityHor = 3;

    public static int minVert = -45;
    public static int maxVert = 45;

    public static int forceJump = 800; // Усилие для прыжка игрока
    public static int speedPlayer = 12;
    public static int speedAmmo = 20;
    public static float maxDistanceFireForEnemy = 10f;

    public static Vector3 positionPlayer;

    
 
    public static int playerHealth;
    public static int countAmmo;
    public static int score;
    public static int maxCountAmmo = 20;
    public static int maxPlayerHealth = 5;
    public static int maxCountEnemy;
    public static int maxTime = 120;
    public static float speedRotationEnemy = 5f;


    public static void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public static void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void ResetGame()
    {
        playerHealth = maxPlayerHealth;
        score = 0;
        maxCountEnemy = 0;
    }
}