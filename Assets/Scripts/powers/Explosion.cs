using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Explosion : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public bool atividade;
    public int damage;
    private int d;
    public bool hitable;
    public int tempo_de_espera;
    public int count; // Contador de tempo
    void melhoria1(){// diminui tempo_de_espera - 4;
        tempo_de_espera = 34;
    }
    void melhoria2(){// diminui tempo_de_espera - 2;
        tempo_de_espera = 32;
    }
    void melhoria3(){// diminui tempo_de_espera - 2;
        tempo_de_espera = 30;
    }
    void melhoria4(){// diminui tempo_de_espera - 4;
        tempo_de_espera = 26;
    }
    void melhoria5(){// diminui tempo_de_espera - 2;
        tempo_de_espera = 24;
    }
    // Start is called before the first frame update
    void Start()
    {
        damage = 200;
        d = damage;
        hitable = false;
        animator.SetBool("ativ", false);
        StartCoroutine(CountToTen());
    }
/*
    void texto(){

            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(".\\Test.txt");
            //Write a line of text
            sw.WriteLine("Hello World!!");
            //Write a second line of text
            sw.WriteLine("From the StreamWriter class");
            //Close the file
            sw.Close();
    }*/
/*
    void ler(){
        //Pass the file path and file name to the StreamReader constructor
        StreamReader sr = new StreamReader(".\\Test.txt");
        //Read the first line of text
        line = sr.ReadLine();
        //Continue to read until you reach end of file
        while (line != null)
        {
            //write the line to console window
            Debug.Log(line);
            //Read the next line
            line = sr.ReadLine();
        }
        //close the file
        sr.Close();
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        if (atividade == true)
        {
        
            if (count == tempo_de_espera)
            {
                StartCoroutine(ativaI());
                
                StartCoroutine(finaliI());
            }
            //transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator ativaI(){
        audioSource.Play();
        yield return new WaitForSeconds(1.5f);
        
        hitable = true;
        SetVisibility(hitable);
    }

    IEnumerator finaliI(){
        yield return new WaitForSeconds(3f);
        hitable = false;
        SetVisibility(hitable);
    }
    IEnumerator CountToTen()
    {
        count = 0;
        
        while (true)
        {
            //Debug.Log("Contagem: " + count);
            count++;

            if (count > tempo_de_espera)
            {
                count = 0; // Reinicia a contagem quando atinge 10
            }

            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de continuar para o próximo loop
        }
    }

    void SetVisibility(bool visible)
    {
        if (visible == true)
        {
            animator.SetBool("ativ", true);
            damage = d;
        }else{
            animator.SetBool("ativ", false);
            damage = 0;
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && atividade == true && hitable == true)
        {
            enemy.TakeDamage(damage);
        }
    }
}
