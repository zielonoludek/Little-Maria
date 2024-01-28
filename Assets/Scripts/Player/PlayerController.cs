using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using static Unity.VisualScripting.Member;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject gas;
    [SerializeField] private int gasAmout = 0;
    private GameManager gameManager;

    private bool isDead = false;
    private float moveSpeed = 5;
    private Camera camera;
    private float ignoreDuration = 1f;
    public bool shooted = false;
    public Vector3 spawnPoint;
    [SerializeField]  private bool paused = false;

    private Animator animator;
    private Rigidbody2D rb;
    private SFXScript sfx;
    [SerializeField] private AudioSource spraySource;
    [SerializeField] private AudioSource stepSource;
    [SerializeField] private AudioSource deathSource;
    [SerializeField] private AudioSource laughSource;

    [SerializeField] private Vector2 movement;
    private bool isFacingRight = true;
    private float timer = 13f;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
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
        if (movement == Vector2.zero)
        {
            stepSource.Pause();
            paused = true;
        }
        else if (paused)
        {
            paused = false;
            stepSource.Play();
        }
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            timer = 13;
            laughSource.Play();
        }
            
            
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
            Invoke("StopIgnoringCollision", ignoreDuration * 2);
            Invoke("SpawnGas", ignoreDuration);

            spraySource.Play();

            gasAmout--;
            shooted = true;
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
            deathSource.Play();
            isDead = true;
            Invoke("Born", 1);
            gameManager.maryAppearTimer -= 15;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            spawnPoint = collision.transform.position;
            gameManager.lastSpawnPoint = collision.gameObject;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Gas")) Kill();
        else if (collision.CompareTag("Maria"))
        {
            Kill();
            LoadLastscene();
        }
    }
    private void LoadLastscene() { SceneManager.LoadScene(17); }
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