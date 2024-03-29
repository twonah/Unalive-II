using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamForm_AttackAnimation : MonoBehaviour
{
    [SerializeField] GameObject attackPrefab;
    [SerializeField] float speed = 10f;
    [SerializeField] float delayTime;
    [SerializeField] float DestroyTimer = 0;
    [SerializeField] bool isShooting;
    public Transform playerTransform;
    public float xOffset;

    private GameObject control;
    private Controll_Script controlSwitch;

    private void Start()
    {
        control = GameObject.FindGameObjectWithTag("SwitchControl");
        controlSwitch = control.GetComponent<Controll_Script>();
    }

    void Update()
    {
        if (playerTransform.localScale.x > 0)
        {
            transform.position = new Vector2(playerTransform.position.x + xOffset, transform.position.y);
        }
        else if (playerTransform.localScale.x < 0)
        {
            transform.position = new Vector2(playerTransform.position.x - xOffset, transform.position.y);
        }
        if (Input.GetMouseButtonDown(0) && isShooting == false && controlSwitch.isDreamform)
        {
            StartCoroutine(shootDelay());
            //Shoot();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void Shoot()
    {

        // Get the mouse position in world space
        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 direction = target - (Vector2)transform.position;
        //direction.Normalize();

        GameObject bullet = Instantiate(attackPrefab, transform.position, Quaternion.identity);

        bullet.transform.localScale = new Vector3(Mathf.Sign(playerTransform.localScale.x), 1.3f, 1);

        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed * Mathf.Sign(playerTransform.localScale.x);

        Destroy(bullet, DestroyTimer);
    }

    private IEnumerator shootDelay()
    {
        isShooting = true;
        yield return new WaitForSeconds(delayTime);

        Shoot();

        isShooting = false;
    }
}
