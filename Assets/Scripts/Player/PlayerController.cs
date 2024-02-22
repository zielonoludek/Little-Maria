using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool shooted = false;

    [SerializeField] private GameObject gas;
    [SerializeField] private int gasAmout = 0;
    [SerializeField] private bool paused = false;
    [SerializeField] private Vector2 movement;

    [SerializeField] private int insanityCheckMax = 20;
    [SerializeField] private float insanityCheckDelay = 25f;



    private bool isDead = false;
    private float moveSpeed = 5;
    private Camera camera;
    private float ignoreDuration = 1f;
    private Vector3 spawnPoint;
    private bool isFacingLeft = true;
    private float timer;
    private bool canMove = true;


    private Animator animator;
    private Rigidbody2D rb;
    private SFXScript sfx;
    [SerializeField] private AudioSource spraySource;
    [SerializeField] private AudioSource stepSource;
    [SerializeField] private AudioSource deathSource;
    [SerializeField] private AudioSource laughSource;

    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
        spawnPoint = transform.position;
        sfx = GetComponentInChildren<SFXScript>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        timer = insanityCheckDelay;
        GameManager.Instance.lastEntrance = transform.position;
    }

    private void FixedUpdate()
    {
        if (isDead) rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (!canMove) return;
            HandleMovement();
            FlipCheck();
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
    }
    private void LateUpdate()
    {
        if (GameManager.Instance.deathCounter < 2 && gasAmout > 0) return;
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            //print(timer);
        }
        else
        {
            timer = insanityCheckDelay;
            print("We go insane"); 
            GoingInsane();
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
        if (Input.GetMouseButtonDown(0))
        {
            Invoke("StopIgnoringCollision", ignoreDuration * 2);
            Invoke("SpawnGas", ignoreDuration);

            spraySource.Play();

            gasAmout--;
            shooted = true;
            SprayAnim();
        }
    }
    private void SprayAnim()
    {
        Vector3 target = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (target - transform.position).normalized;

/*
        animator.ResetTrigger("Spray Up");
        animator.ResetTrigger("Spray Down");
        animator.ResetTrigger("Spray Side");
*/


        /*if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0) animator.SetTrigger("Spray Up");
            else animator.SetTrigger("Spray Down");
        }
        else animator.SetTrigger("Spray Left");*/


        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0) animator.SetTrigger("Spray Up");
            else animator.SetTrigger("Spray Down");
        }
        else //if (!(Mathf.Abs(direction.y) > Mathf.Abs(direction.x)))
        {

            if (direction.x > 0)
            {
                if (isFacingLeft)
                {
                    ForceFlip();
                }
            }
            else //if (!(direction.x > 0)) 
            {
                if (!isFacingLeft)
                {
                    ForceFlip();
                }
            }
            animator.SetTrigger("Spray Side");
        }
    }
    void StopIgnoringCollision() => shooted = false;
    public void Kill()
    {
        sfx.PlayDieAnim();
        deathSource.Play();
        isDead = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            //spawnPoint = collision.transform.position;
            //Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Gas") && !shooted) GameManager.Instance.killPlayer(1);

        if (collision.CompareTag("Maria")) GameManager.Instance.killPlayer(5);
    }
    public void NewGas() => gasAmout = 6;
    private void SpawnGas() => Instantiate(gas, transform.position, transform.rotation);
    public void RoomRespawn()
    {
        isDead = false;
        transform.position = GameManager.Instance.lastEntrance;
    }
    public void GameRespawn()
    {
        isDead = false;
        transform.position = spawnPoint;
    }

    private void FlipCheck()
    {
        if (isFacingLeft && movement.x > 0f || !isFacingLeft && movement.x < 0f)
        {
            ForceFlip();
        }
    }
    private void ForceFlip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 LoaclScale = transform.localScale;
        LoaclScale.x *= -1f;
        transform.localScale = LoaclScale;
    }

    public int GetGasAmount() { return gasAmout; }


    private void GoingInsane()
    {
        int insanityRandomiser = Random.Range(GameManager.Instance.deathCounter, insanityCheckMax+1);
        if (insanityRandomiser >= insanityCheckMax)
        {
            print("Sanity check filed");
            canMove = false;
            laughSource.Play();
            animator.SetTrigger("Laught");
            Invoke("AllowMovemnt", 1.1f);

        }
        else print("Sanity check passed "+ insanityRandomiser);
    }

    void AllowMovemnt()
    {
        canMove = true;
    }
}