using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamformCollectKey : MonoBehaviour
{
    [SerializeField] public bool _IsCollectedDKey;
    private GameObject _player;
    private GameObject _dKey;
    [HideInInspector] public Transform _DKeyPos;

    // Start is called before the first frame update
    void Start()
    {
        _dKey = GameObject.FindGameObjectWithTag("DKey");
        _player = GameObject.FindGameObjectWithTag("Player");
        _DKeyPos = _dKey.GetComponent<Transform>();
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
            _DKeyPos.transform.SetParent(_player.transform);

            Collider2D dKeyCollider = _dKey.GetComponent<Collider2D>();
            dKeyCollider.enabled = false;
        }
    }
}
