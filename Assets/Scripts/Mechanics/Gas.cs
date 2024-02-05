using UnityEngine;

public class Gas : MonoBehaviour
{
    private Camera camera;
    private Rigidbody2D rb;
    private float speed = 300;
    [SerializeField] bool isStatic = false;

    public void Awake()
    {
        camera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        Vector3 target = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (target - transform.position).normalized;
        if(!isStatic) rb.AddForce(direction * speed);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Room")) Destroy(gameObject);
        else if (collider.gameObject.CompareTag("Movable") && collider.gameObject.layer != 7)
        {
            Destroy(collider.gameObject);
        }
    }
}
