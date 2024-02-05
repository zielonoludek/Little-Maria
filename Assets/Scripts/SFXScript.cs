using UnityEngine;

public class SFXScript : MonoBehaviour
{
    private SpriteRenderer renderer;
    private Animator animator;

    public Vector3 current;
    private Vector3 Respawn = new Vector3 (-0.58f, 0.37f, 0);
    private Vector3 Death = new Vector3(-0.7f, 0.6f, 0);
    private Vector3 Spray = new Vector3(-0.7f, 0.6f, 0);

    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = null;
        renderer.enabled = false;
    }
    private void Update() => current = transform.localPosition;
    public void PlayRespawnAnim()
    {
        transform.localPosition = Respawn;
        transform.localScale = new Vector3(1f,1f,1f);
        renderer.enabled = true;

        animator.SetTrigger("Respawn");
    }
    public void PlayDieAnim()
    {
        transform.localPosition = Death;
        transform.localScale = new Vector3(5f, 5f, 5f);
        renderer.enabled = true;

        animator.SetTrigger("Death");
    }
    public void PlaySprayAnim()
    {
        transform.localPosition = Spray;
        transform.localScale = new Vector3(5f, 5f, 5f);
        renderer.enabled = true;

        animator.SetTrigger("Death");
    }
}
