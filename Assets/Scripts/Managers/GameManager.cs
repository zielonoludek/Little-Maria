using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private Player player;
    private int currentRoom = 0;
    [SerializeField]private int numberOfRooms;

    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        RoomController[] roomList = FindObjectsOfType<RoomController>();
        numberOfRooms = roomList.Length;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) killPlayer();
        if (Input.GetKeyDown(KeyCode.R)) ResetRoom();
    }
    public void killPlayer()
    {
        player.Kill();
        ResetRoom();
    }
    private void ResetRoom()
    {
        SceneManager.UnloadScene(currentRoom + 1);
        SceneManager.LoadScene(currentRoom + 1, LoadSceneMode.Additive);
    }
    public void AddRoom() {
        currentRoom++;
        LoadNextRoom();
    }
    private void LoadNextRoom()
    {
        SceneManager.LoadScene(currentRoom + 1, LoadSceneMode.Additive);
    }
}
