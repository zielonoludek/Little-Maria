using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    private bool _isPlayerInRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraController.SetFollowTarget(transform);
            GameManager.instance.currentRoom = transform.gameObject;
        }
    }
    public void Reset()
    {
        
    }
}
