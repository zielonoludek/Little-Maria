using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Image imageSlot;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        imageSlot.enabled = false;
    }

    public void UpdateItemUIPanel(Sprite itemImage)
    {
        imageSlot.sprite = itemImage;
        imageSlot.enabled = true;
    }
}
