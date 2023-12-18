using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation = 90;

    public Transform orientation;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * MenuSceneManager.sens * 10;
            float mouseY = Input.GetAxisRaw("Mouse Y") * MenuSceneManager.sens * 10;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
