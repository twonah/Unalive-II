using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockeddoor : MonoBehaviour
{
    public Key isUnlocked;
    public GameObject key;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isUnlocked.islock == true)
            {
                Unlocked();
                Destroy(key);
            }
        }
    }
    public void Unlocked()
    {
        Destroy(gameObject);
    }
}
