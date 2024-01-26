using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Player player;
    public void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) killPlayer();
    }

    public void killPlayer()
    {
        player.Kill();
    }
}
