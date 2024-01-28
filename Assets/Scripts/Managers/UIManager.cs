using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    public static Action OnGamePaused;
    public static Action OnGameUnpaused;

    [SerializeField] private Image imageSlot;
    [SerializeField] private Sprite gas1;
    [SerializeField] private Sprite gas2;
    [SerializeField] private Sprite gas3;
    [SerializeField] private Sprite gas4;

    private PlayerController playerController;

    private bool _isGamePaused;
    
    private void Awake()
    {
        Instance = this;
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        _isGamePaused = false;
        imageSlot.enabled = false;
    }

    private void Update()
    {
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        Debug.Log(playerController.GetGasAmount());
        if (playerController.GetGasAmount() > 4)
        {
            imageSlot.sprite = gas1;
        }
        else if (playerController.GetGasAmount() > 2 && playerController.GetGasAmount() <= 4)
        {
            imageSlot.sprite = gas2;
        }
        else if (playerController.GetGasAmount() > 0 && playerController.GetGasAmount() <= 2)
        {
            imageSlot.sprite = gas3;
        }
        else if (playerController.GetGasAmount() == 0)
        {
            imageSlot.sprite = gas4;
        }
    }

    public void PickupItemUIPanel(Sprite itemImage)
    {
        imageSlot.sprite = itemImage;
        imageSlot.enabled = true;
    }

    public void TogglePauseMenu()
    {
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
        }
    }
}
