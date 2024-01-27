using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    public void PlayRespawnAnim()
    {
        renderer.enabled = true;
    }


    public void PlayDieAnim()
    {
        renderer.enabled = true;
    }

}
