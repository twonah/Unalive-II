using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupImage : MonoBehaviour
{
    [SerializeField] private GameObject _iconPopUp;
    [SerializeField] private Door _portal;

    // Start is called before the first frame update
    void Start()
    {
        _iconPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && _portal._IsLocked)
        {
            _iconPopUp.SetActive(true);
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || !_portal._IsLocked)
        {
            _iconPopUp.SetActive(false);
        }
    }
}
