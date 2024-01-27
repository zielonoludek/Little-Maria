using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Patrol,
        Attack,
        Dead
    }

    private State _currentState;

    [SerializeField] private FieldOfView fov;
    private PlayerController player;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float waitTimeAtPatrolPoint = 2f;

    private int _currentPatrolIndex;
    private float _timer;

    private void Awake()
    {
        _currentState = State.Patrol;
        player = FindObjectOfType<PlayerController>();   
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Patrol:
                PatrolState();
                break;
            case State.Attack:
                AttackState();
                break;
            case State.Dead:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PatrolState()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points set");
            return;
        }
    
        Transform targetPatrolPoint = patrolPoints[_currentPatrolIndex];
        if (_timer <= 0f)
        {
            Vector3 direction = (targetPatrolPoint.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, targetPatrolPoint.position, patrolSpeed * Time.deltaTime);
        
            if (Vector2.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
            {
                _timer = waitTimeAtPatrolPoint;
                _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }

        if (fov.visibleTargets.Count > 0)
        {
            _currentState = State.Attack;
        }
    }

    private void AttackState()
    {
        player.Kill();
    }
}
