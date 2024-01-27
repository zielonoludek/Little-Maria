using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5;
    private void FixedUpdate()
    {
        HandleMovement();
        UseGas();
    }
    private void HandleMovement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
    private void UseGas()
    {
        
    }
}