using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private bool isPaused;

    InputAction pause;
    InputAction restart;

    void Start()
    {
        pause = InputSystem.actions.FindAction("Pause");
        restart = InputSystem.actions.FindAction("Restart");
    }

    void Update()
    {
        if (pause.WasPressedThisFrame())
        {
            if (isPaused)
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }

            isPaused = !isPaused;
        }
        if (restart.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
