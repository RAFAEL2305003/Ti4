using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Incineracao : MonoBehaviour
{
    private Camera mainCamera;

    private GameObject player;
    public AudioSource audioSource;
    public Animator animator;
    public bool hitable;
    public bool visivel;
    public bool atividade;
    public int damage;
    private int d;
    private Renderer rend;
    public float delayPorHit;
    private float timer; // Contador de tempo
    public float visibleTime; // Tempo em segundos que o objeto fica visível 4
    public float invisibleTime; // Tempo em segundos que o objeto fica invisível 18
    void melhoria1(){// aumenta visibleTime + 2;
        visibleTime = 6;
    }
    void melhoria2(){// diminui invisibleTime - 2;
        invisibleTime = 16;
    }
    void melhoria3(){// aumenta damage + 2;
        damage = 12;
        d = 12;
    }
    void melhoria4(){// aumenta visibleTime + 1;
        visibleTime = 7;
    }
    void melhoria5(){// diminui delayPorHit = 0.2;
        delayPorHit = 0.2f;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main;
    }

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        hitable = true;
        visivel = true;
        d = damage;
        delayPorHit = 0.4f;
        rend = GetComponent<Renderer>();
        timer = visibleTime; // Começa visível
        //SetVisibility(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame

    public void FixedUpdate() {

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Assegura que está no plano 2D.

        Vector3 direction = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calcula o ângulo em graus.
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 0); // Ajusta a rotação. Subtraia 90 se necessário para alinhar com a frente do seu objeto.]

        transform.position = player.transform.position;

        if (atividade == true)
        {
            timer -= Time.deltaTime;
        
            if (timer <= 0f)
            {
                // Inverte a visibilidade
                SetVisibility(!visivel);
                
                // Reinicia o timer baseado na visibilidade atual
                if (visivel)
                {
                    timer = visibleTime;
                }
                else
                {
                    timer = invisibleTime;
                }
            }
            //transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }else{
            audioSource.Stop();
            animator.SetBool("ativ", false);
            animator.SetBool("Fiml", true);
            damage = 0;
        }
        
    }

    void SetVisibility(bool visible)
    {
        if (visible == true)
        {
            audioSource.Play();
            animator.SetBool("ativ", true);
            animator.SetBool("Fiml", false);
            damage = d;
        }else{
            audioSource.Stop();
            animator.SetBool("ativ", false);
            animator.SetBool("Fiml", true);
            damage = 0;
        }
        visivel = visible;
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
        //Debug.Log("dano: ");
        enemy.TakeDamage(damage);
        //Destroy(gameObject);
        yield return new WaitForSeconds(delayPorHit);
        hitable = true;
    }
}
