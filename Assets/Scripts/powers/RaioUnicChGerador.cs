using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioUnicChGerador : MonoBehaviour
{
    public bool atividade;
    public int count;
    public int quantidadeRaios;
    public int tempo_de_espera;
    public GameObject objetoEspecifico; // Objeto próximo ao qual o clone será posicionado
    public GameObject objetoOriginal; // Objeto que sera clonado
    public float distanciaMaxima = 5f; // Distância máxima do objeto específico onde o clone pode aparecer
    public bool hitable;
    public AudioSource audioSource;
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

    void melhoria1(){// aumenta + 5 raios
        quantidadeRaios = 15;
        level = 1;
    }
    void melhoria2(){// distanciaMaxima * 2 
        distanciaMaxima = 20;
        level = 2;
    }
    void melhoria3(){// aumenta * 2 raios
        quantidadeRaios = 30;
        level = 3;
    }
    void melhoria4(){// diminui - 5 tempo_de_espera
        tempo_de_espera = 19;
        level = 4;
    }
    void melhoria5(){// diminui - 5 tempo_de_espera
        tempo_de_espera = 10;
        level = 5;
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

    void CloneAleatorioProximo() {
        {
        // Gera uma posição aleatória dentro de um círculo com raio 'distanciaMaxima'
        Vector2 posicaoAleatoria = Random.insideUnitCircle * distanciaMaxima;
        posicaoAleatoria += (Vector2)objetoEspecifico.transform.position; // Adiciona a posição do objeto específico

        GameObject clone = Instantiate(objetoOriginal, posicaoAleatoria, Quaternion.identity);
    }
    }
    // Start is called before the first frame update
    void Start()
    {
        hitable = true;
        StartCoroutine(CountToTen());
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (atividade && count == tempo_de_espera && hitable == true) {
            hitable = false;
            StartCoroutine(TimeRaioClone());
            
        }
    }

    IEnumerator TimeRaioClone(){
        for (int i = 0; i < quantidadeRaios; i++)
            {
                CloneAleatorioProximo();
            }
            StartCoroutine(son());
        yield return new WaitForSeconds(1f);
        hitable = true;
    }

    IEnumerator son(){
        audioSource.Play();
        yield return new WaitForSeconds(7f);
        audioSource.Stop();
    }
}
