using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirectionCheck : MonoBehaviour
{
    public bool _FacingRight;
    public bool _FacingLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceCheck();
    }

    private void FaceCheck()
    {
        if(transform.localScale.x <= 0)
        {
            _FacingLeft = true;
            _FacingRight = false;
        }
        else if(transform.localScale.x >= 0)
        {
            _FacingRight = true;
            _FacingLeft = false;
        }
    }
}
