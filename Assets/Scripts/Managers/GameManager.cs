using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private PlayerController player;
    private int currentRoom = 0;
    
    public float maryAppearTimer;
    private float maryTimerInit;

    public bool lastRoomReached { get; set; } = false;
    
    public Vector3 lastEntrance { get; set; }


    private void Awake()
    {
        Instance = this;
        maryTimerInit = maryAppearTimer;
        player = FindObjectOfType<PlayerController>();
        SceneManager.LoadScene("Room0", LoadSceneMode.Additive);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) killPlayer(0);
        if (Input.GetKeyDown(KeyCode.L)) killPlayer(5);
        if (Input.GetKeyDown(KeyCode.R)) ResetRoom();
        if (Input.GetKeyDown(KeyCode.Escape)) UIManager.Instance.TogglePauseMenu();

        if (maryAppearTimer > 0)
        {
            maryAppearTimer -= Time.deltaTime;
        }
    }
    public void killPlayer(int value)
    {
        player.Kill();

        switch (value)
        {
            case 0:
                
                Invoke("ResetRoom", 1);
                player.Invoke("RoomRespawn",1);
                break;
            case 1:
                maryAppearTimer -= 15;
                Invoke("ResetRoom", 1);
                player.Invoke("RoomRespawn", 1);
                break;
            case 5:
                maryAppearTimer = maryTimerInit;
                Invoke("ResetToStart", 1);
                player.Invoke("GameRespawn", 1);
                break;
        }

        
    }
    private void ResetRoom()
    {
        if (lastRoomReached)
        {
            ResetToStart();
        }
        else
        {
            SceneManager.UnloadSceneAsync("Room" + currentRoom);
            SceneManager.LoadScene("Room" + currentRoom, LoadSceneMode.Additive);
        }
    }
    private void ResetToStart()
    {
        SceneManager.UnloadSceneAsync("Room" + currentRoom);
        currentRoom = 0;
        SceneManager.LoadScene("Room" + currentRoom, LoadSceneMode.Additive);

    }
    public void AddRoom(Transform entrance) 
    {
        lastEntrance = entrance.position;
        SceneManager.UnloadSceneAsync("Room" + currentRoom);
        currentRoom++;
        LoadNextRoom();
    }
    private void LoadNextRoom()
    {
        maryAppearTimer += 45;
        SceneManager.LoadScene("Room" + currentRoom, LoadSceneMode.Additive);
    }
}
