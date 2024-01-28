using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaryController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource himerasource;
    private float maryMoveSpeed = 3;
    
    private PlayerController player;
    private GameManager gameManager;
    private float timer = 30;
    private bool attack = false;
    private float himeraTimer = 10;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        source.Play();
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else if (timer == 0)
        {
            attack = true;
            transform.position = gameManager.lastSpawnPoint.transform.position;
        }

        if (attack)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * (maryMoveSpeed * Time.deltaTime));
        }
        source.volume += Time.deltaTime;

        if (himeraTimer > 0) himeraTimer -= Time.deltaTime;
        else
        {
            himeraTimer = 10;
            himerasource.Play();
        }
    }
}
