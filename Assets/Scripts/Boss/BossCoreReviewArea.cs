using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreReviewArea : MonoBehaviour
{
    [SerializeField] private LayerMask _playerAndDreamformLayer;
    [SerializeField] private Transform area;
    [SerializeField] private float areaRange;

    [SerializeField] public bool IsEnterArea;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AreaCheck();
    }

    private void AreaCheck()
    {
        if (Physics2D.OverlapCircle(gameObject.transform.position, areaRange, _playerAndDreamformLayer))
        {
            IsEnterArea = true;
        }
        else
        {
            IsEnterArea = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (area == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(area.position, areaRange);
    }
}
