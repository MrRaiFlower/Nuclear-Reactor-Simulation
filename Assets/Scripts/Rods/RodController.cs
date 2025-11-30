using UnityEngine;
using UnityEngine.InputSystem;

public class RodController : MonoBehaviour
{
    InputAction up;
    InputAction down;

    void Start()
    {
        up = InputSystem.actions.FindAction("Up");
        down = InputSystem.actions.FindAction("Down");
    }

    void Update()
    {
        if (up.WasPressedThisFrame())
        {
            this.gameObject.transform.position += Vector3.up * 1.0f;
        }
        if (down.WasPressedThisFrame())
        {
            this.gameObject.transform.position += Vector3.down * 1.0f;
        }
    }
}
