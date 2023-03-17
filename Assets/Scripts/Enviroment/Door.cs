using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject _player;
    private GameObject _dreamform;

    private DreamformCollectKey _dreamformCollected;
    private PlayerCollectKey _playerCollected;

    private GameObject _pKey;
    private GameObject _dKey;

    [SerializeField] private bool _isLocked;
    [SerializeField] private GameObject _portal;

    // Start is called before the first frame update
    void Start()
    {
        _isLocked = true;

        _player = GameObject.FindGameObjectWithTag("Player");
        _dreamform = GameObject.FindGameObjectWithTag("DreamForm");

        _playerCollected = _player.GetComponent<PlayerCollectKey>();
        _dreamformCollected = _dreamform.GetComponent<DreamformCollectKey>();

        _pKey = GameObject.FindGameObjectWithTag("PKey");
        _dKey = GameObject.FindGameObjectWithTag("DKey");
    }

    // Update is called once per frame
    void Update()
    {
        if(_isLocked)
        {
            _portal.SetActive(false);
        }
        else
        {
            _portal.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(_playerCollected._IsCollectedPKey && _dreamformCollected._IsCollectedDKey)
            {
                _isLocked = false;
                _pKey.SetActive(false);
                _dKey.SetActive(false);
            }
        }
    }
}
