using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Transform playerTransportPoint;
    private GameManager gameManager;

    private void Start()
    {
        playerTransportPoint = transform.GetChild(0).GetChild(0);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = playerTransportPoint.position;
            gameManager.AddRoom();
        }
    }
}
