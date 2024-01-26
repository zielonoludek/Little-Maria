using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Transform playerTransportPoint;
    private bool _isUnlocked;

    private void Start()
    {
        _isUnlocked = true;
        playerTransportPoint = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_isUnlocked)
            {
                Debug.Log("Doors are open");
                other.transform.position = playerTransportPoint.position;
            }
            else
            {
                Debug.Log("Doors are closed");
            }
        }
    }
}
