using UnityEngine;
using UnityEngine.SceneManagement;

public class MaryController : MonoBehaviour
{
    [SerializeField] private float maryMoveSpeed;
    
    private PlayerController player;
    private GameManager gameManager;

    private int playerCatched;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    { 
        if (gameManager.maryAppearTimer <= 0) GoTowardsPlayer();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7)
        {
            maryMoveSpeed = 0;
            playerCatched++;
            GameManager.Instance.killPlayer(5);

            if (playerCatched > 3) SceneManager.LoadScene("end");
        }
    }
    private void GoTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * (maryMoveSpeed * Time.deltaTime));
        }
    }
}
