using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Transform parentPlayer;
    public bool islock;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            islock = true;
            gameObject.transform.SetParent(parentPlayer);
            gameObject.transform.position = parentPlayer.transform.position + new Vector3(-0.008f, 0.195f, 0f);
            //Vector3 pos = parent.transform.position;
            //pos.y = 1;
            //parent.transform.position = pos;
        }
        else
        {
            islock = false;
        }
    }
}
