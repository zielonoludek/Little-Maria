using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private Player player;
    private int roomNumer = 0;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) killPlayer();
        if (Input.GetKeyDown(KeyCode.R)) ResetRoom();
    }
    public void killPlayer()
    {
        player.Kill();
    }
    private void ResetRoom()
    {
        SceneManager.UnloadScene(roomNumer + 1);
        SceneManager.LoadScene(roomNumer + 1, LoadSceneMode.Additive);
        killPlayer();
    }
    public void NextRoom() {
        roomNumer++; 
    }
}
