using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    private Camera camera;
    private Rigidbody2D rb;
    private PlayerController player;
    private float speed = 200;
    [SerializeField] bool isStatic = false;

    public void Awake()
    {
        camera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }
    public void Start()
    {
        Vector3 target = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (target - transform.position).normalized;
        if(!isStatic) rb.AddForce(direction * speed);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Room")) Destroy(gameObject);
        else if (collider.gameObject.CompareTag("Movable"))
        {
            player.Kill();
            if (collider.gameObject.layer != 7)  Destroy(collider.gameObject);
        }
    }
}
