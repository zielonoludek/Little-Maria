using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5;
    [SerializeField] private int gasAmout = 0;
    private float ignoreDuration = 1f;
    public bool shooted = false;
    private Vector3 spawnPoint;

    [SerializeField] private GameObject gas;
    private Animator animator;
    private Rigidbody2D rb;

    private SFXScript sfx;

    private void Awake()
    {
        sfx = GetComponentInChildren<SFXScript>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        if (gasAmout > 0) UseGas();
    }
    private void HandleMovement()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector2 rbmovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity += rbmovement * (moveSpeed * Time.deltaTime);
        //transform.position += movement * (moveSpeed * Time.deltaTime);
    }
    private void UseGas()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(gas, transform.position, transform.rotation);
            gasAmout--;
            shooted = true;
            Invoke("StopIgnoringCollision", ignoreDuration);
        }
    }
    void StopIgnoringCollision()
    {
        shooted = false;
    }
    public void Kill()
    {
        if (!shooted)
        {
            moveSpeed = 0;
            sfx.PlayDieAnim();
            transform.position = spawnPoint;
            Invoke("IncreaseMoveSpeed", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            spawnPoint = collision.transform.position;
            Destroy(collision.gameObject);
        }
    }
    public void NewGas()
    {
        gasAmout = 6;
    }
    private void IncreaseMoveSpeed()
    {
        moveSpeed = 0;
    }
}