using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaryController : MonoBehaviour
{
    [SerializeField] private AudioClip maryLaughingSound;
    [SerializeField] private float maryMoveSpeed;
    
    private PlayerController player;
    private GameManager gameManager;

    private int playerCatched;
    private Vector3 spawnPosition;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.maryAppearTimer <= 0)
        {
            GoTowardsPlayer();   
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7)
        {
            maryMoveSpeed = 0;
            playerCatched++;
            player.Kill();

            if (playerCatched > 3)
            {
                //play cutscene
            }
        }
    }

    private void GoTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * (maryMoveSpeed * Time.deltaTime));
        }
        else
        {
            Debug.LogWarning("Player not found.");
        }
    }
}
