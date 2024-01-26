using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float moveSpeed;
    
    private bool _isPlayerChangingRooms;
    private int _doorNumber;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (!_isPlayerChangingRooms)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        transform.position += moveDir * moveDistance;
    }
}
