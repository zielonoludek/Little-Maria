using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Sprite pickupSprite;
    PlayerController player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            player.NewGas();
            UIManager.Instance.PickupItemUIPanel(pickupSprite);
            Destroy(transform.gameObject);
        }
    }
}