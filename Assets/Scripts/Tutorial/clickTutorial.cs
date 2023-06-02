using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickTutorial : MonoBehaviour
{
    [SerializeField] float speed;
    GameObject target;

    [SerializeField] GameObject text;
    [SerializeField] Controll_Script cs;

    [SerializeField] int clickCount;
    [SerializeField] int clickLimit;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("DreamForm");
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
        if(cs.isDreamWalkerToDreamform)
        {
            text.SetActive(true);
        }
        if(cs.isDreamWalkerToPlayer)
        {
            Destroy(gameObject);
        }
        
        if(cs.isDreamform && Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(clickDelay());
        }

        if(clickCount >= clickLimit)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator clickDelay()
    {
        text.SetActive(false);
        clickCount += 1;
        yield return new WaitForSecondsRealtime(0.75f);
        text.SetActive(true);
    }
}
