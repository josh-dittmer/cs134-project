using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // reference to the player
    public GameObject player;

    // mouse sensitivity
    public float mouseSensitivity = 100f;
    // distance from the player
    public float distance = 5f;
    // minimum pitch
    public float minPitch = -30f;
    // maximum pitch
    public float maxPitch = 60f;

    // yaw and pitch
    private float yaw = 0f;
    private float pitch = 20f;

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        // if player is null or not active, return
        if (player == null || !player.activeSelf) return;

        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;

        // Clamp vertical rotation
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Create rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Calculate position
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = player.transform.position + offset;

        // Always look at player
        transform.LookAt(player.transform.position);
    }
}

