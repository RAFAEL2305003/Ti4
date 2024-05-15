using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bau : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    public Sprite bauSombra;
    public Sprite bauAberto;


    public GameObject objetoPai; // Referência ao objeto pai

    // Função para mudar a sprite do objeto filho
    public void MudarSprite(string nomeObjetoFilho, Sprite novaSprite)
    {
        // Acessa o objeto filho pelo nome
        Transform objetoFilho = objetoPai.transform.Find(nomeObjetoFilho);

        if (objetoFilho != null)
        {
            // Acessa o componente SpriteRenderer do objeto filho
            SpriteRenderer spriteRenderer = objetoFilho.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // Muda a sprite do objeto filho
                spriteRenderer.sprite = novaSprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer não encontrado no objeto filho.");
            }
        }
        else
        {
            Debug.LogError("Objeto filho não encontrado.");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.CompareTag("Player")){
            PlaySound2();
            StartCoroutine(sonns());
            StartCoroutine(ExecutarAcaoComDelay());
        }
    }

    IEnumerator sonns()
    {
        yield return new WaitForSeconds(0.2f);
        MudarSprite("TX Shadow Chest", bauSombra);
        spriteRenderer.sprite = bauAberto;
    }

    IEnumerator ExecutarAcaoComDelay()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    public void AutoExtermin(){
        StartCoroutine(AutoElimine());
    }

    IEnumerator AutoElimine()
    {
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }

    public void PlaySound2()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            //Debug.Log("baus");
        }
        else
        {
            //Debug.LogWarning("AudioSource ou áudio não definidos.");
        }
    }
}
