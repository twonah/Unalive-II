using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderController : MonoBehaviour
{
    [SerializeField] private float _eyeRange;

    [SerializeField] private EnemyPatrol E_Patrol;
    [SerializeField] private EnemyExplode E_Explode;
    [SerializeField] private EnemyMoveToPlayer E_MoveTo;
    [SerializeField] private FaceDirectionCheck Self_FC;

    [SerializeField] private Transform _eyePoint;
    [SerializeField] private LayerMask _targetLayer;

    private bool _see = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ExploderDetection();

        ExploderControls();
    }

    private void ExploderDetection()
    {
        _see = Physics2D.OverlapCircle(_eyePoint.position, _eyeRange, _targetLayer);    //Is there any collider with Player layer
    }

    private void ExploderControls()
    {
        if (!_see && !E_Explode._ChargeOn && !E_Explode._IsDie)  //Player not enter eye range
        {
            E_Patrol.enabled = true;
            E_MoveTo.enabled = false;
        }

        if (_see && !E_Explode._IsDie)    //Player enter eye range 
        {
            E_MoveTo.enabled = true;
            E_Patrol.enabled = false;
        }

        if (E_Explode._PlayerEnterAttackRange == true || E_Explode._ChargeOn == true)    //Player enter attack range
        {
            E_MoveTo.enabled = false;
        }

        //Additional check
        if (E_Patrol._WalkSpeed <= -0.1 && Self_FC._FacingRight && !E_Patrol.enabled)
        {
            E_Patrol._WalkSpeed *= -1;
        }

        if (E_Patrol._WalkSpeed >= 0.1 && Self_FC._FacingLeft && !E_Patrol.enabled)
        {
            E_Patrol._WalkSpeed *= -1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_eyePoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_eyePoint.position, _eyeRange);
    }
}
