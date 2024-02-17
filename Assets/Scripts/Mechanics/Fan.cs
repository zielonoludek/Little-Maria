using UnityEngine;

public class Fan : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 100;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        Vector3 target = transform.parent.position;
        direction = (transform.position - target).normalized;
        //audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gas"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed * Time.deltaTime * 100);
        }
    }
}
