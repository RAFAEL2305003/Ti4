using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioDeSubmissao : MonoBehaviour
{
    public AudioSource audioSource;
    public bool hitable = true;
    //public int count = 0;
    public bool atividade = false;
    public int damage;
    private int d;
    private Renderer rend;
    public float delayPorHit;
    public float visibleTime; // Tempo em segundos que o objeto fica visível 4
    public float invisibleTime; // Tempo em segundos que o objeto fica invisível 14


    private float timer; // Contador de tempo
    void melhoria1(){// aumenta visibleTime + 2;
        visibleTime = 6;
    }
    void melhoria2(){// diminui invisibleTime - 2;
        invisibleTime = 12;
    }
    void melhoria3(){// aumenta damage + 2;
        damage = 7;
        d = 7;
    }
    void melhoria4(){// aumenta visibleTime + 2;
        visibleTime = 8;
    }
    void melhoria5(){// diminui delayPorHit = 0.1;
        delayPorHit = 0.1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CountToTen());
        delayPorHit = 0.4f;
        d = damage;
        rend = GetComponent<Renderer>();
        timer = visibleTime; // Começa visível
        SetVisibility(false);
    }

    public void FixedUpdate() {

        if (atividade == true)
        {
            timer -= Time.deltaTime;
        
            if (timer <= 0f)
            {
                // Inverte a visibilidade
                SetVisibility(!rend.enabled);
                
                // Reinicia o timer baseado na visibilidade atual
                if (rend.enabled)
                {
                    timer = visibleTime;
                }
                else
                {
                    timer = invisibleTime;
                }
            }
            //transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
/*
    IEnumerator CountToTen()
    {
        count = 0;
        
        while (true)
        {
            //Debug.Log("Contagem: " + count);
            count++;

            if (count > 2)
            {
                count = 0; // Reinicia a contagem quando atinge 10
            }

            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de continuar para o próximo loop
        }
    }*/

    void SetVisibility(bool visible)
    {
        if (visible == true)
        {
            audioSource.Play();
            damage = d;
        }else{
            audioSource.Stop();
            damage = 0;
        }
        rend.enabled = visible;
    }/*

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }*/

    void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && hitable == true)
        {
            enemy.TakeDamage(damage);
            //StartCoroutine(ExecutarAcaoComDelay(enemy));
        }
    }

    IEnumerator ExecutarAcaoComDelay(Enemy enemy)
    {
        hitable = false;
        //Debug.Log("dano: ");
        enemy.TakeDamage(damage);
        //Destroy(gameObject);
        yield return new WaitForSeconds(delayPorHit);
        hitable = true;
    }
}
