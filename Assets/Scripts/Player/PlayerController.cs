using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private bool moving;

    private Vector2 movement;


    private bool isFacingRight = true;

    private void Awake()
    {
        sfx = GetComponentInChildren<SFXScript>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.magnitude));
        if (gasAmout > 0) UseGas();
        Flip();
    }
    private void HandleMovement()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //transform.position += movement * (moveSpeed * Time.deltaTime);

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
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



    private void Flip()
    {
        if (isFacingRight && movement.x > 0f || !isFacingRight && movement.x < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 LoaclScale = transform.localScale;
            LoaclScale.x *= -1f;
            transform.localScale = LoaclScale;
        }
    }



}