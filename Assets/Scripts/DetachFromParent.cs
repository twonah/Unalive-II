using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachFromParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.parent = null;
    }
}
