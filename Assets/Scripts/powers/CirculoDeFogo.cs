using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirculoDeFogo : MonoBehaviour
{
    public AudioSource audioSource;
    public bool hitable;
    //public int count = 0;
    public bool atividade;
    public int damage = 0;
    private int d;
    private Renderer rend;
    public float delayPorHit;
    public float visibleTime; // Tempo em segundos que o objeto fica visível
    public float invisibleTime; // Tempo em segundos que o objeto fica invisível
    public float rotationSpeed; // Velocidade de rotação em graus por segundo
    public int level;

    public void upar(){
        switch(level) 
        {
        case 0:
            melhoria1();
            break;
        case 1:
            melhoria2();
            break;
        case 2:
            melhoria3();
            break;
        case 3:
            melhoria4();
            break;
        case 4:
            melhoria5();
            break;
        default:
            break;
        }
    }

    void melhoria1(){// aumenta visibleTime + 5;
        visibleTime = 9;
        level = 1;
    }
    void melhoria2(){// diminui invisibleTime - 6;
        invisibleTime = 10;
        level = 2;
    }
    void melhoria3(){// aumenta damage + 4;
        damage = 14;
        d = 14;
        level = 3;
    }
    void melhoria4(){// aumenta visibleTime + 3;
        visibleTime = 12;
        level = 4;
    }
    void melhoria5(){// diminui delayPorHit = 0.1;
        delayPorHit = 0.1f;
        level = 5;
    }
    private float timer; // Contador de tempo
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CountToTen());
        delayPorHit = 0.3f;
        d = 8;
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
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
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
        //Debug.Log("dano: ");
        enemy.TakeDamage(damage);
        //Destroy(gameObject);
        yield return new WaitForSeconds(delayPorHit);
        hitable = true;
    }
}
