using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamformCollectKey : MonoBehaviour
{
    [SerializeField] public bool _IsCollectedDKey;
     private GameObject _dKey;

    // Start is called before the first frame update
    void Start()
    {
        _dKey = GameObject.FindGameObjectWithTag("DKey");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DKey"))
        {
            _IsCollectedDKey = true;
            _dKey.SetActive(false);
        }
    }
}
