using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    private SpriteRenderer renderer;

    public Vector3 current;

    private Vector3 Respawn = new Vector3 (-0.58f, 0.37f, 0);
    private Vector3 Death = new Vector3(-0.7f, 0.6f, 0);


    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        current = transform.localPosition;
    }

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



}
