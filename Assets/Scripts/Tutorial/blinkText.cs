using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class blinkText : MonoBehaviour
{
    TextMeshPro text;
    [SerializeField] float interval;
    float alpha;
    float elapsedTime;
    bool isBlinking;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        StartCoroutine(Blink());
    }
    private void OnEnable()
    {
        text = GetComponent<TextMeshPro>();
        StartCoroutine(Blink());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Blink()
    {
        while (true)
        {
            alpha = Mathf.PingPong(Time.unscaledTime, interval);
            text.alpha = alpha;
            yield return null;
        }
    }

    private void OnDisable()
    {
        text.alpha = 1f;
        alpha = 0f;
    }
}
