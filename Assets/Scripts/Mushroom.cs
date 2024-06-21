using UnityEngine;
using System.Collections;
// Exemplo de uma subclasse de inimigo
public class Mushroom : Enemy
{
    public PlayerController player;
    public float moveSpeed = 3f;
    private Transform target;
    public int mushHealth = 50;
    public SpriteRenderer sp;
    public float avoidanceDistance = 1.5f;
    public Animator animator;

    public bool hitable = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = mushHealth;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sp = GetComponent<SpriteRenderer>();
        damage = 10;
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * moveSpeed;

            if (direction.x >= 0)
            {
                sp.flipX = false;
            }
            else
            {
                sp.flipX = true;
            }
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        player = other.gameObject.GetComponent<PlayerController>();
        if (other.gameObject.CompareTag("Player") && hitable)
        {
            StartCoroutine(ExecutarAcaoComDelay());
        }
    }

    IEnumerator ExecutarAcaoComDelay()
    {
        hitable = false;
        DoDamage(damage);
        yield return new WaitForSeconds(0.5f);
        hitable = true;
    }

    public virtual void DoDamage(int amount)
    {
        player.TakeDamage(amount);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.Play("MushroomAttack");
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.Play("MushroomRun");
        }
    }

    protected override void Die()
    {

        base.Die();
    }
}
