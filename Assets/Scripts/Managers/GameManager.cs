using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private int currentRoom = 0;
    
    public float maryAppearTimer;

    public Transform lastEntrance;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        SceneManager.LoadScene("Room0", LoadSceneMode.Additive);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) killPlayer();
        if (Input.GetKeyDown(KeyCode.R)) ResetRoom();
        if (Input.GetKeyDown(KeyCode.Escape)) UIManager.Instance.TogglePauseMenu();
         
        if (maryAppearTimer > 0) maryAppearTimer -= Time.deltaTime;
    }
    public void killPlayer()
    {
        player.Kill();
        maryAppearTimer -= 15;
        ResetRoom();
    }
    private void ResetRoom()
    {
        SceneManager.UnloadScene("Room"+currentRoom);
        SceneManager.LoadScene("Room" + currentRoom, LoadSceneMode.Additive);
    }
    public void AddRoom(Transform entrance) 
    {
        lastEntrance = entrance;
        currentRoom++;
        LoadNextRoom();
    }
    private void LoadNextRoom()
    {
        maryAppearTimer += 45;
        SceneManager.LoadScene("Room" + currentRoom, LoadSceneMode.Additive);
    }
}
