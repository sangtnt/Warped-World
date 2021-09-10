using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Crab : Enemy
{
    [SerializeField]
    bool move;
    [SerializeField]
    float endPointX;
    [SerializeField]
    float duration;
    [SerializeField]
    Ease ease;

    Animator animator;
    private void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (move)
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("Speed", 1);
            transform.DOMoveX(endPointX, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .OnStepComplete(() => { transform.localScale = new Vector2(-transform.localScale.x, 1); })
                .SetEase(ease);
        }
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
