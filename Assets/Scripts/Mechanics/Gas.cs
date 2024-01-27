using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    private Vector3 target;
    private Rigidbody2D rb;
    private PlayerController player;
    private float speed = 1000;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }
    public void Start()
    {
        transform.position = player.transform.position;
        target = Input.mousePosition;
        Vector3 force = (target - transform.position).normalized;
        rb.AddForce(force * speed);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Room")) Destroy(gameObject);
        if (collider.gameObject.CompareTag("Movable")) Destroy(gameObject);
    }
}
