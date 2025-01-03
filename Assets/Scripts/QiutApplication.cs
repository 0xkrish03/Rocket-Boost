using UnityEngine;
using UnityEngine.InputSystem;

public class QiutApplication : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Quitting Application");
            Application.Quit();
        }
    }
}
