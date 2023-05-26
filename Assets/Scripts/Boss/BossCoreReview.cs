using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreReview : MonoBehaviour
{
    [SerializeField] private GameObject BossCore;
    [SerializeField] private Transform Boss;
    [SerializeField] private Transform CoreReviewPos;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float ScaleSpeed;
    [SerializeField] private Vector3 CoreMaximumScale;
    [SerializeField] private Vector3 CoreOriginalScale;

    [SerializeField] public bool IsReviewCore = true;
    [SerializeField] public bool IsCoreOut = false;

    private float currentScale = 0f;

    // Start is called before the first frame update
    void Start()
    {
        BossCore.SetActive(false);
        BossCore.transform.position = Boss.transform.position;
        IsReviewCore = false;
    }

    // Update is called once per frame
    void Update()
    {
        CoreReview();
    }

    private void CoreReview()
    {
        if(IsReviewCore)
        {
            BossCore.SetActive(true);

            currentScale += ScaleSpeed * Time.deltaTime;
            currentScale = Mathf.Clamp01(currentScale);

            BossCore.transform.position = Vector2.MoveTowards(BossCore.transform.position, new Vector2(CoreReviewPos.position.x, CoreReviewPos.position.y), MoveSpeed * Time.deltaTime);
            BossCore.transform.localScale = Vector3.Lerp(BossCore.transform.localScale, CoreMaximumScale, currentScale);

            IsCoreOut = true;
        }
        else
        {
            currentScale += ScaleSpeed * Time.deltaTime;
            currentScale = Mathf.Clamp01(currentScale);

            BossCore.transform.position = Vector2.MoveTowards(BossCore.transform.position, new Vector2(Boss.position.x, Boss.position.y), MoveSpeed * Time.deltaTime);
            BossCore.transform.localScale = Vector3.Lerp(BossCore.transform.localScale, CoreOriginalScale, currentScale);

            if(BossCore.transform.position == Boss.position)
            {
                BossCore.SetActive(false);
                IsCoreOut = false;
            }
        }

    }
}
