using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaryController : MonoBehaviour
{
    [SerializeField] private AudioClip maryLaughingSound;
    [SerializeField] private float maryMoveSpeed;
    
    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        GoTowardsPlayer();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7)
        {
            maryMoveSpeed = 0;
            player.Kill();
            Debug.Log("u fkd up");
            //death animation, restart level etc.
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
