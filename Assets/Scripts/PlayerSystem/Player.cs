using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    float health;
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    float hurtForce;
    PlayerAction playerAction;
    [SerializeField]
    AudioClip HurtClip;

    private float currentHealth;
    Rigidbody2D rb;
    Animator animator;
    public int powerAmountCollected;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthBar.maxValue = health;
        healthBar.value = currentHealth;
        rb = GetComponent<Rigidbody2D>();
        playerAction = GetComponent<PlayerAction>();
        animator = GetComponent<Animator>();
        powerAmountCollected = 0;
    }
    void SetHealth()
    {
        healthBar.value = currentHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SetHealth();
        playerAction.isHurt = true;
        animator.SetBool("IsHurt", true);
        StartCoroutine(DelayHurt());
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
        }
    }
    IEnumerator DelayHurt()
    {
        yield return new WaitForSeconds(0.5f);
        playerAction.isHurt = false;
        animator.SetBool("IsHurt", false);
    }
    void PlayHurtEffect(GameObject GO)
    {
        Vector2 direction = gameObject.transform.position - GO.transform.position;
        rb.AddForce(direction.normalized * hurtForce);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().GetDamage());
            PlayHurtEffect(collision.gameObject);
            AudioManager.Instance.PlayClipOneShot(HurtClip);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Power"))
        {
            this.powerAmountCollected++;
            collision.gameObject.SetActive(false);
            if (powerAmountCollected == GameManager.Instance.currLevel.numOfPowersToWin)
            {
                GameManager.Instance.SetGameState(GameManager.GameState.CompleteLevel);
            }
        }
    }
}
