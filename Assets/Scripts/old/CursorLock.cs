using UnityEngine;

public class CursorLock : MonoBehaviour
{

    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject dialog;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Hide the cursor if desired
    }

    void Update()
    {
        // Ensure the cursor remains locked
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(pause.active == true||dialog.active==true) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // Hide the cursor if desired
        }
    }
}
