using UnityEngine;

public class RenderOrder : MonoBehaviour
{
    private Renderer renderer;
    private int sorintgOrder = 1000;
    private int offset = 0;
    
    void Start() => renderer = GetComponent<Renderer>();
    void LateUpdate()
    {
        renderer.sortingOrder = (int)(sorintgOrder - transform.position.y - offset);
    }
}