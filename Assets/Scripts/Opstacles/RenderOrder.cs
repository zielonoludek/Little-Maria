using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrder : MonoBehaviour
{
    private int sorintgOrder = 1000;
    [SerializeField] private int offset = 0;
    private Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        renderer.sortingOrder = (int)(sorintgOrder - transform.position.y - offset);
    }
}