using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public void Play()
    {
        if (Interface.Instance.gameOver.activeSelf == true || Interface.Instance.victory.activeSelf == true || Interface.Instance.firstStart == true)
        {
            Debug.Log(Interface.Instance.gameOver.activeSelf);
            Debug.Log(Interface.Instance.victory.activeSelf);
            Debug.Log(Interface.Instance.firstStart);
            Interface.Instance.ResetGame();
            Interface.Instance.firstStart = false;
        }
        Interface.Instance.SwitchToGame();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
