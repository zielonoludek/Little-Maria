using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Range(0, 360)] public float viewAngle;
    public float viewRadius;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();

    private void Start() => StartCoroutine(FindTargetsWithDelay(0.2f));
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    private void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, playerMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.up, dirToTarget);

            if (angle < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) && !visibleTargets.Contains(target))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) angleInDegrees -= transform.eulerAngles.z;
        return new Vector2 (Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}