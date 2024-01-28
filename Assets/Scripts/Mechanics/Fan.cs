using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fan : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 100; 
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Vector3 target = transform.parent.position;
        direction = (transform.position - target).normalized;
        PlayAudioClip();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gas"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed * Time.deltaTime * 100);
        }
    }

    private void PlayAudioClip()
    {
        audioSource.Play();
        audioSource.loop = true;
    }
}
