using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private CameraController cameraController;


    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) cameraController.SetFollowTarget(transform);
    }
}
