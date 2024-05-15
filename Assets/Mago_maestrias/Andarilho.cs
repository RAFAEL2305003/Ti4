using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public Animator animator;
    public SpriteRenderer sp;

    public BarraDeVida barra;
    public float vida = 100;

    void Start()
    {
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        barra.colocarVidaMaxima(vida);
    }

    /*private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy") && hitable){
            StartCoroutine(ExecutarAcaoComDelay());
        }
    }

    IEnumerator ExecutarAcaoComDelay()
    {
        hitable = false;
        vida -= 10.0f;
        barra.alterarVida(vida);
        yield return new WaitForSeconds(0.5f);
        hitable = true;
    }*/

    void Update()
    {   
        bool isMoving = false;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;
        int left = Input.GetKey(KeyCode.A) ? 1 : 0;
        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int down = Input.GetKey(KeyCode.S) ? 1 : 0;

        if(Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene("menu");
        }

        if (right - left != 0)
        {
            Vector3 newPosition = transform.position + transform.right * Time.deltaTime * speed * (right - left);
            if(newPosition.x < 35.6 && newPosition.x > -35.7){
                transform.position += transform.right * Time.deltaTime * speed * (right - left);
                animator.Play("Move");
                isMoving = true;
                if (right - left == 1) sp.flipX = false;
                else sp.flipX = true;
            }

        }

        if(up - down != 0)
        {
            Vector3 newPosition = transform.position + transform.up * Time.deltaTime * speed * (up - down);
            if(newPosition.y < 32.5 && newPosition.y > -23){
                transform.position += transform.up * Time.deltaTime * speed * (up - down);
                animator.Play("Move");
                isMoving = true;
            }
        }
        if(!isMoving)
        {
            animator.Play("Idle");
        }
    }

    public void TakeDamage(int amount)
    {
        vida -= 10.0f;
        barra.alterarVida(vida);
    }
}
