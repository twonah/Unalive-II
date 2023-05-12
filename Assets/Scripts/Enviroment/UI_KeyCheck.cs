using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_KeyCheck : MonoBehaviour
{
    [SerializeField] private GameObject _pKey;
    [SerializeField] private GameObject _dKey;

    private GameObject _player;
    private GameObject _dreamform;
    private GameObject _portal;

    private DreamformCollectKey _dreamformCollected;
    private PlayerCollectKey _playerCollected;
    private Door _door;

    // Start is called before the first frame update
    void Start()
    {
        _pKey.SetActive(false);
        _dKey.SetActive(false);

        _player = GameObject.FindGameObjectWithTag("Player");
        _dreamform = GameObject.FindGameObjectWithTag("DreamForm");
        _portal = GameObject.FindGameObjectWithTag("Portal");

        _playerCollected = _player.GetComponent<PlayerCollectKey>();
        _dreamformCollected = _dreamform.GetComponent<DreamformCollectKey>();
        _door = _portal.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerCollected._IsCollectedPKey)
        {
            _pKey.SetActive(true);
        }

        if (_dreamformCollected._IsCollectedDKey)
        {
            _dKey.SetActive(true);
        }

        if(!_door._IsLocked)
        {
            _pKey.SetActive(false);
            _dKey.SetActive(false);
        }
    }
}
