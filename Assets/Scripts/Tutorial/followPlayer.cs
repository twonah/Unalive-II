using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    GameObject target;

    [SerializeField] float hideDelay;
    [SerializeField] GameObject pressD;
    [SerializeField] GameObject pressA;
    [SerializeField] GameObject pressSpace;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if(Input.GetKeyUp(KeyCode.D))
        {
            StartCoroutine(delayHideD());
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            StartCoroutine(delayHideA());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(delayHideSpace());
        }
    }

    IEnumerator delayHideD()
    {
        yield return new WaitForSeconds(hideDelay);
        pressD.SetActive(false);
    }

    IEnumerator delayHideA()
    {
        yield return new WaitForSeconds(hideDelay);
        pressA.SetActive(false);
    }

    IEnumerator delayHideSpace()
    {
        yield return new WaitForSeconds(hideDelay);
        pressSpace.SetActive(false);
    }
}
