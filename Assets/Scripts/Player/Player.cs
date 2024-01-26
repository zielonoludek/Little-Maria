using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 spawnPoint;

    public void Kill()
    {
        //killing animation, screen dims
        transform.position = spawnPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            spawnPoint = collision.transform.position;
            Destroy(collision.gameObject);
        }
    }
}
