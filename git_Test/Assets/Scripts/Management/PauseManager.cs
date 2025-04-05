using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    public bool isPaused { get; private set; }

    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            currentSpeed = Time.timeScale;
            Time.timeScale = 0f;
            Debug.Log("***PAUSED****");

            InputManager.PlayerInput.SwitchCurrentActionMap("UI");
        }
    }

    public void UnpauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = currentSpeed;
            Debug.Log("***UNPAUSED****");

            InputManager.PlayerInput.SwitchCurrentActionMap("Player");
        }
    }


}
