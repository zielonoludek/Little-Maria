using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private int currentRoom = 1;
    [SerializeField]private int numberOfRooms;

    
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        RoomController[] roomList = FindObjectsOfType<RoomController>();
        numberOfRooms = roomList.Length;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) killPlayer();
        if (Input.GetKeyDown(KeyCode.R)) ResetRoom();
        if (Input.GetKeyDown(KeyCode.Escape)) UIManager.Instance.TogglePauseMenu();
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
