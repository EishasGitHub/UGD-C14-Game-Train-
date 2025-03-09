using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MouseCamLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public Transform torch;
    private PlayerMovement pm;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            pm = playerObj.GetComponent<PlayerMovement>();
        }

        else
        {
            Debug.LogError("Player GameObject not found!");
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (pm.GameStarted)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            if (torch != null)
            {
                torch.localRotation = Quaternion.Euler(torch.localEulerAngles.x, torch.localEulerAngles.y, -xRotation);
            }

            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }
}