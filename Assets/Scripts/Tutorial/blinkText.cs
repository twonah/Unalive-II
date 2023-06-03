using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class blinkText : MonoBehaviour
{
    TextMeshPro text;
    TextMeshProUGUI textUI;
    [SerializeField] float interval;
    float alpha;
    float elapsedTime;
    bool isBlinking;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        textUI = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Blink());
    }
    private void OnEnable()
    {
        text = GetComponent<TextMeshPro>();
        textUI = GetComponent<TextMeshProUGUI>();
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
            if(text != null)
            {
                text.alpha = alpha;
            }
            if (textUI != null)
            {
                textUI.alpha = alpha;
            }
            yield return null;
        }
    }

    private void OnDisable()
    {
        if (text != null)
        {
            text.alpha = 1f;
        }
        if (textUI != null)
        {
            textUI.alpha = 1f;
        }
        alpha = 0f;
    }
}
