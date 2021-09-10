using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField]
    float runSpeed;
    [SerializeField]
    float jumpPower;
    [SerializeField]
    float maxVelocity;
    [Header("Check Ground")]
    [SerializeField]
    GameObject groundCheck;
    [SerializeField]
    LayerMask groundLayer;
    [Header("Check On Wall")]
    [SerializeField]
    GameObject wallCheck;
    [SerializeField]
    LayerMask wallLayer;
    [SerializeField]
    float slideSpeed;
    [Header("Gun")]
    [SerializeField]
    GameObject gun;
    [HideInInspector]
    public bool isHurt;

    [Header("Audio Clip")]
    [SerializeField]
    AudioClip RunClip;
    [SerializeField]
    AudioClip JumpClip;
    [SerializeField]
    AudioClip SlideClip;
    [SerializeField]
    AudioClip ShootClip;

    private Animator animator;
    private Joystick joystick;
    private Rigidbody2D rb;
    private bool isJumping;
    private bool isCrouch;
    private bool isHeadUp;
    private Vector2 facingDir;
    private bool canShoot;
    private bool onPointerDown;
    private bool isSliding;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ResetShootingState();
        UpdateFacingDir();
        canShoot = true;
        onPointerDown = false;
        isSliding = false;
        isHurt = false;
    }
    void ResetShootingState()
    {
        isCrouch = false;
        animator.SetBool("IsCrouch", isCrouch);
        isHeadUp = false;
        animator.SetBool("IsHeadUp", isHeadUp);
        UpdateFacingDir();
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsGround()&&!isSliding)
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
        else if(IsGround()&&isJumping)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
        IsOnWall();
    }
    private void FixedUpdate()
    {
        if (isHurt)
        {
            return;
        }
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Run();
        LimitVelocity();
        // Make Shoot
        if (onPointerDown && canShoot && (!isJumping||isSliding))
        {
            Shoot();
            canShoot = false;
            StartCoroutine(ResetCanShootState());
        }
    }
    void UpdateFacingDir()
    {
        facingDir = new Vector2(transform.localScale.x, 0);
    }
    public void Shoot()
    {
        GameObject bullet = PoolManager.Instance.GetGameObjectByPoolType(PoolType.WaveFormBullet);
        bullet.transform.position = gun.transform.position;
        if (isSliding)
        {
            bullet.GetComponent<BulletController>().SetDirection(facingDir * new Vector2(-1, 1));
        }
        else
        {
            bullet.GetComponent<BulletController>().SetDirection(facingDir);
        }
        AudioManager.Instance.PlayClipOneShot(ShootClip);
        animator.SetBool("IsShooting", true);
        canShoot = false;
    }
    public void OnPointerDown()
    {
        onPointerDown = true;
    }
    public void OnPointerUp()
    {
        onPointerDown = false;
        animator.SetBool("IsShooting", false);
    }
    void Run()
    {
        // Calculate moveForce
        float moveForce = joystick.Horizontal * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(moveForce));
        if (joystick.Horizontal == 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            ResetShootingState();
            Flip(joystick.Horizontal > 0 ? false : true);
            rb.AddForce(new Vector2(moveForce, 0));
        }
    }
    void Flip(bool isFlip)
    {
        if (isFlip)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }
    void LimitVelocity()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            Vector2 vel = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
            vel.y = rb.velocity.y;
            rb.velocity = vel;
        }
    }
    public void Jump()
    {
        if (IsGround()&&!isJumping&&!isSliding)
        {
            rb.AddForce(jumpPower * Vector2.up);
            AudioManager.Instance.PlayClipOneShot(JumpClip);
            ResetShootingState();
        }
    }
    bool IsGround()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(
            groundCheck.transform.position, 
            new Vector2(0.5f, 0.5f),
            0, groundLayer
        );
        CheckPlatform(collider2Ds);
        return collider2Ds.Length > 0;
    }
    void CheckPlatform(Collider2D[] collider2Ds)
    {
        transform.parent = null;
        foreach (Collider2D collider in collider2Ds)
        {
            GameObject platform = collider.gameObject;
            if (platform.CompareTag("MovementPlatform"))
            {
                gameObject.transform.SetParent(platform.transform);
                PlatformMovement platformMovement = platform.GetComponent<PlatformMovement>();
                MakePlatformStart(platformMovement.moveWhenStart, platformMovement);
            }
        }
    }
    void MakePlatformStart(bool stateMovement, PlatformMovement platformMovement)
    {
        if (!stateMovement)
        {
            platformMovement.StartMove();
            platformMovement.moveWhenStart = true;
        }
    }
    public void Crouch()
    {
        isCrouch = !isCrouch;
        animator.SetBool("IsCrouch", isCrouch); 
        if (isCrouch)
        {
            isHeadUp = false;
            animator.SetBool("IsHeadUp", isHeadUp);
            UpdateFacingDir();
        }
    }
    public void HeadUp()
    {
        isHeadUp = !isHeadUp;
        animator.SetBool("IsHeadUp", isHeadUp);
        if (isHeadUp)
        {
            facingDir = Vector2.up;
                isCrouch = false;
            animator.SetBool("IsCrouch", isCrouch);
        }
        else
        {
            UpdateFacingDir();
        }
    }
    IEnumerator ResetCanShootState()
    {
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
    }
    void IsOnWall()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(
            wallCheck.transform.position,
            0.2f,
            wallLayer
        );
        if (collider2Ds.Length > 0 && rb.velocity.y < 1 && !IsGround()&&Mathf.Abs(joystick.Horizontal)>0)
        {
            Vector2 vel = rb.velocity;
            vel.y = -slideSpeed;
            rb.velocity = vel;
            animator.SetBool("IsSliding", true);
            isSliding = true;
            animator.SetBool("IsJumping", false);

        }
        else
        {
            animator.SetBool("IsSliding", false);
            isSliding = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.transform.position, new Vector3(0.5f, 0.5f));
        Gizmos.DrawSphere(wallCheck.transform.position, 0.2f);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void PlayRunClip()
    {
        AudioManager.Instance.PlayClipOneShot(RunClip);
    }
}
