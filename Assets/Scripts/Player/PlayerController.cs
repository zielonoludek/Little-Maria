using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5;
    [SerializeField] private int gasAmout = 0;
    private float ignoreDuration = 1f;
    public bool shooted = false;
    private Vector3 spawnPoint;

    [SerializeField] private GameObject gas;

    private void Update()
    {
        HandleMovement();
        if(gasAmout > 0) UseGas();
    }
    private void HandleMovement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += movement * moveSpeed * Time.deltaTime;
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
        if (!shooted) transform.position = spawnPoint;
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
}