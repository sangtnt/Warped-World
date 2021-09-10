using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField]
    public bool moveWhenStart;
    [SerializeField]
    float duration;
    [SerializeField]
    PathType pathType;
    [SerializeField]
    PathMode pathMode;
    [SerializeField]
    Ease ease;
    [SerializeField]
    Vector3[] path;
    private void Reset()
    {
        gameObject.tag = "MovementPlatform";
    }
    private void Start()
    {
        if (moveWhenStart)
        {
            StartMove();
        }
    }
    public void StartMove()
    {
        transform.DOPath(path, duration, pathType, pathMode, 10, Color.green)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease);
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
