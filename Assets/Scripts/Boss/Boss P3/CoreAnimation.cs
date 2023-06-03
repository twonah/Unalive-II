using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAnimation : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private CoreHP coreHP;

    private float HPAfterTakeDamage;
    private float HPBeforeTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        HPAfterTakeDamage = coreHP.currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        HPBeforeTakeDamage = coreHP.currentHP;

        HurtAnimation();
    }

    private void HurtAnimation()
    {
        if (HPBeforeTakeDamage != HPAfterTakeDamage)
        {
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        _anim.SetBool("IsTakingDamage", true);
        yield return new WaitForSeconds(0.25f);
        _anim.SetBool("IsTakingDamage", false);
        HPAfterTakeDamage = HPBeforeTakeDamage;
    }
}
