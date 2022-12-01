using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWelderControl : MonoBehaviour
{
    [SerializeField] private float _eyeRange = 0.5f;

    [SerializeField] private EnemyPatrol EP;

    [SerializeField] private Transform _eyePoint;
    [SerializeField] private LayerMask _playerLayer;

    private bool _see;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WelderDetection();

        if(_see == true)
        {
            WelderControls();
        }
    }

    private void WelderDetection()
    {
        _see = Physics2D.OverlapCircle(_eyePoint.position, _eyeRange, _playerLayer);    //Is there any collider with Player layer
    }

    private void WelderControls()
    {
        Debug.Log("See player");
    }

    private void OnDrawGizmosSelected()
    {
        if(_eyePoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_eyePoint.position, _eyeRange);
    }
}
