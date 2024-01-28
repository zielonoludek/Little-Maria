using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private int currentRoom = 2;
    
    public float maryAppearTimer;
    
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        SceneManager.LoadScene(currentRoom, LoadSceneMode.Additive);


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) killPlayer();
        if (Input.GetKeyDown(KeyCode.R)) ResetRoom();
        if (Input.GetKeyDown(KeyCode.Escape)) UIManager.Instance.TogglePauseMenu();

        if (maryAppearTimer > 0)
        {
            maryAppearTimer -= Time.deltaTime;
        }
    }
    public void killPlayer()
    {
        player.Kill();
        maryAppearTimer -= 15;
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
        maryAppearTimer += 45;
        SceneManager.LoadScene(currentRoom + 1, LoadSceneMode.Additive);
    }
}
