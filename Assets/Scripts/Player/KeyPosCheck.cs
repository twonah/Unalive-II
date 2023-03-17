using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPosCheck : MonoBehaviour
{
    [SerializeField] private Transform Pos1;
    [SerializeField] private Transform Pos2;

    private GameObject _player;
    private GameObject _dreamform;

    private DreamformCollectKey _dreamformCollected;
    private PlayerCollectKey _playerCollected;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _dreamform = GameObject.FindGameObjectWithTag("DreamForm");

        _playerCollected = _player.GetComponent<PlayerCollectKey>();
        _dreamformCollected = _dreamform.GetComponent<DreamformCollectKey>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerCollected._IsCollectedPKey == true)
        {
            _playerCollected._PKeyPos.transform.position = Pos1.transform.position;
        }

        if (_dreamformCollected._IsCollectedDKey == true)
        {
            _dreamformCollected._DKeyPos.transform.position = Pos2.transform.position;
        }
    }
}
