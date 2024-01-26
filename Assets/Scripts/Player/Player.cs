using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private Transform spawnPoint;

    private void Awake() => Initialize();
    public void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public void Kill()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spawnPoint"))
        {
            spawnPoint = collision.transform;
            Destroy(collision.gameObject);
        }
    }
}
