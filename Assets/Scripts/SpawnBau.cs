using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bau : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject Inimigo1Goblin;
    public float spawnRate = 1f; 
    public Vector2 spawnAreaSize; // Tamanho da área de spawn nulu

    private float nextSpawnTime = 10;
    private float nextSpawnGoblinTime = 10;

    private void Start() {
        nextSpawnTime = Time.time + 10;
        nextSpawnGoblinTime = Time.time + 10;
    }

    private void FixedUpdate()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnItem();
            SpawnItem();
            SpawnItem();
            nextSpawnTime = Time.time + 20 ;
        }
        if (Time.time >= nextSpawnGoblinTime)
        {   
            forDeSpawGoblin(10);
            nextSpawnGoblinTime = Time.time + 5 ;
        }
    }

    private void forDeSpawGoblin(int enymis){
        for (int i = 0; i < 10; i++){
            SpawnGoblin();
        }
    }

    private void SpawnItem()
    {
        // Calcula uma posição aleatória dentro da área de spawn
        float randomX = Random.Range(-35, 35);
        float randomY = Random.Range(-23, 32);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Instancia o item na posição calculada
        GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);

        // Verifica se o objeto instanciado tem o componente ScriptObjetoInstanciado
        Bau scriptObjetoInstanciado = newItem.GetComponent<Bau>();

        if (scriptObjetoInstanciado != null)
        {
            // Chama a função do objeto instanciado
            scriptObjetoInstanciado.AutoExtermin();
        }
        else
        {
            Debug.LogError("O objeto instanciado não possui o script ScriptObjetoInstanciado.");
        }
    }

    private void SpawnGoblin()
    {
        // Calcula uma posição aleatória dentro da área de spawn
        float randomX = Random.Range(-35, 35);
        float randomY = Random.Range(-23, 32);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Instancia o item na posição calculada
        Instantiate(Inimigo1Goblin, spawnPosition, Quaternion.identity);
    }

}
