using System;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;

    public void TogglePause()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        Time.timeScale = Convert.ToInt16(!gameObject.activeSelf);
    }
}
