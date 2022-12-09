using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserButton : MonoBehaviour
{
    public GameObject Taser;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Taser.SetActive(true);
        }
        else
        {
            Taser.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Taser.SetActive(false);
        }
    }
}
