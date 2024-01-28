using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private bool isDead = false;
    private float moveSpeed = 5;
    private Camera camera;
    [SerializeField] private int gasAmout = 0;
    private float ignoreDuration = 1f;
    public bool shooted = false;
    private Vector3 spawnPoint;

    [SerializeField] private GameObject gas;
    private Animator animator;
    private Rigidbody2D rb;
    private SFXScript sfx;

    private Vector2 movement;


    private bool isFacingRight = true;

    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
        spawnPoint = transform.position;
        sfx = GetComponentInChildren<SFXScript>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isDead) rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            HandleMovement();
            Flip();
            animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.magnitude));
            
        }
    }
    private void Update()
    {
        if (!isDead && gasAmout > 0) UseGas();
    }
    private void HandleMovement()
    {
        if (gasAmout > 0) animator.SetBool("HasGas", true);
        else animator.SetBool("HasGas", false);

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    private void UseGas()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            Invoke("SpawnGas", ignoreDuration);
            gasAmout--;
            shooted = true;
            Invoke("StopIgnoringCollision", ignoreDuration*2);
            SprayAnim();
        }
    }
    private void SpawnGas()
    {
        Instantiate(gas, transform.position, transform.rotation);
    }
    private void SprayAnim()
    {
        Vector3 target = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = (target - transform.position).normalized;
        Debug.Log(direction);

        animator.ResetTrigger("Spray Up");
        animator.ResetTrigger("Spray Down");
        animator.ResetTrigger("Spray Left");

        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0) animator.SetTrigger("Spray Up");
            else animator.SetTrigger("Spray Down");
        }
        else animator.SetTrigger("Spray Left");
    }
    void StopIgnoringCollision()
    {
        shooted = false;
    }
    public void Kill()
    {
        if (!shooted)
        {
            sfx.PlayDieAnim();
            isDead = true;
            Invoke("Born", 1);
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
    private void Born()
    {
        isDead = false;
        transform.position = spawnPoint;
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
    public int GetGasAmount() { return gasAmout;  }
    
}