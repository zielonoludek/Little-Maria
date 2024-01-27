using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Sprite pickupSprite;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.UpdateItemUIPanel(pickupSprite);
            Destroy(gameObject);
        }
    }

    
}
