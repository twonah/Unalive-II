using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectKey : MonoBehaviour
{
    [SerializeField] public bool _IsCollectedPKey;
    private GameObject _pKey;

    // Start is called before the first frame update
    void Start()
    {
        _pKey = GameObject.FindGameObjectWithTag("PKey");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PKey"))
        {
            _IsCollectedPKey = true;
            _pKey.SetActive(false);
        }
    }
}
