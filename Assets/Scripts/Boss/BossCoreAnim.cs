using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreAnim : MonoBehaviour
{
    [SerializeField] private Animator _bossCoreAnim;
    [SerializeField] private BossCoreReview B_CoreReview;
    [SerializeField] private HitPoints B_HP;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CoreAnimation();
    }

    private void CoreAnimation()
    {
        if(B_CoreReview.IsCoreOut)
        {
            _bossCoreAnim.SetBool("IsShow",true);
        }
        else
        {
            _bossCoreAnim.SetBool("IsShow", false);
        }

        if(B_HP._IsTakingDamage)
        {
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        _bossCoreAnim.SetBool("IsTakingDamage", true);
        yield return new WaitForSeconds(0.25f);
        _bossCoreAnim.SetBool("IsTakingDamage", false);
        B_HP._IsTakingDamage = false;
    }
}
