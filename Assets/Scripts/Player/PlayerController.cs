using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    private bool _isPlayerChangingRooms = false;

    private void FixedUpdate()
    {
        if (!_isPlayerChangingRooms) HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += movement * 5 * Time.deltaTime;
    }
}
