using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioUnicoChuva : MonoBehaviour
{
    Animator animator;

    public bool hitable;
    public float delayPorHit;
    
    //public bool delays;
    public int damage;
    public float distanciaMaxima = 5f; // Distância máxima do objeto específico onde o clone pode aparecer

    void melhoria5(){// diminui delayPorHit = 0.1
        delayPorHit = 0.1f;
    }
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    IEnumerator Raios(){
        animator.SetBool("ativar", true);
        //animator.SetTrigger("New Trigger");
        yield return new WaitForSeconds(1f);
        animator.SetBool("ativar",false);
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && hitable == true)
        {
            StartCoroutine(ExecutarAcaoComDelay(enemy));
        }
    }
    IEnumerator ExecutarAcaoComDelay(Enemy enemy)
    {
        hitable = false;
        enemy.TakeDamage(damage);
        yield return new WaitForSeconds(delayPorHit);
        hitable = true;
    }
    

    
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("ativar", false);
        hitable = true;
        Vector3 novaEscala = transform.localScale * 3f;
        transform.localScale = novaEscala;
        StartCoroutine(Raios());
        
    }
}