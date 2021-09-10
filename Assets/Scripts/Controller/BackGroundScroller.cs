using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    [SerializeField]
    Vector2 direction;
    [SerializeField]
    float scrollSpeed;

    Renderer bgRenderer;
    // Start is called before the first frame update
    void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += scrollSpeed * direction * Time.deltaTime;
    }
}
