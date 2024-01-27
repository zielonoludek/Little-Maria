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
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UIManager.Instance.UpdateItemUIPanel(pickupSprite);
            player.NewGas();
            Destroy(gameObject);
        }
    }
}