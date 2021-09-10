using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField]
    Vector2 jumForce;
    [SerializeField]
    GameObject groundCheck;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    float duration;

    private bool isJumping;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if (!IsGround())
        {
            isJumping = true;
        }
        if (IsGround() && isJumping)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            isJumping = false;
            animator.SetBool("IsJumping", false);
            StartCoroutine(DelayJump());
        }
    }
    IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(duration);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Jump();
    }
    void Jump()
    {
        rb.AddForce(jumForce);
        jumForce.x = -jumForce.x;
        animator.SetBool("IsJumping", true);
    }
    bool IsGround()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(
            groundCheck.transform.position,
            new Vector2(0.5f, 0.5f),
            0, groundLayer
        );
        return collider2Ds.Length > 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.transform.position, new Vector3(0.5f, 0.5f));
    }
}
