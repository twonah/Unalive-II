using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarpPlayer : MonoBehaviour
{
    private GameObject _SwitchControl;
    private GameObject _player;
    private GameObject _dreamform;

    public Transform warpPoint;

    public bool IsWarp;

    // Start is called before the first frame update
    void Start()
    {
        _SwitchControl = GameObject.FindWithTag("SwitchControl");
        _player = GameObject.FindWithTag("Player");
        _dreamform = GameObject.FindWithTag("DreamForm");


    }

    // Update is called once per frame
    void Update()
    {
        WarpPlayer();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void WarpPlayer()
    {
        _player.transform.position = warpPoint.position;
        _dreamform.transform.position = warpPoint.position;
        IsWarp = true;
    }
}
