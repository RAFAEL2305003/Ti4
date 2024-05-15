using UnityEngine;
using System.Collections;
// Exemplo de uma subclasse de inimigo
public class Goblin : Enemy
{
    public PlayerController player;
    public float moveSpeed = 20f;
    private Transform target;
    public int orcHealth = 20;
    public SpriteRenderer sp;
    public float avoidanceDistance = 1.5f;
    public Animator animator;
    private AudioSource audioSource;

    public bool hitable = true;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        // Definindo a vida inicial do Orc
        health = orcHealth;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sp = GetComponent<SpriteRenderer>();
        damage = 10;
    }

    void Update(){
        if (target != null)
        {
            //animator.Play("Goblin");
            Vector2 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * moveSpeed;

            if(direction.x >= 0){
                sp.flipX = false;
            }
            else {
                sp.flipX = true;
            }
        }

    }

    private void OnCollisionStay2D(Collision2D other) {
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
        PlaySound();
        yield return new WaitForSeconds(0.5f);
        hitable = true;
    }

    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            //Debug.LogWarning("AudioSource ou áudio não definidos.");
        }
    }

    public virtual void DoDamage(int amount) 
    {
        player.TakeDamage(amount);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
                animator.Play("AttackGoblin");
                //Debug.Log("Colisão detectada com outro sprite!");
            }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            animator.Play("Goblin");
            //Debug.Log("Colisão detectada com outro sprite!");
        }
    }

    protected override void Die(){

        base.Die();
    }
}
