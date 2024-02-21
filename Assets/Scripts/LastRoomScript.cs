using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastRoomScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if(GameManager.Instance.lastRoomReached == true)
            {
                EndScene();
            }
            else
            {
                print("Reached");
                GameManager.Instance.lastRoomReached = true;
            }
        }
    }


    private void EndScene()
    {
        print("The end, hehe");
    }
}
