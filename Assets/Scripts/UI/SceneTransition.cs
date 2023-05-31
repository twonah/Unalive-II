using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public bool _TransitionEnd;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TransitionEndTrue()
    {
        _TransitionEnd = true;
    }

    public void TransitionEndFalse()
    {
        _TransitionEnd = false;
    }
}
