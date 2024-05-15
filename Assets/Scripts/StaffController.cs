using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posicao : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private GameObject player;
    public GameObject shoot;
    private AudioSource audioSource;

    public float speed = 20f; // You can adjust the initial value as needed

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Assegura que está no plano 2D.

        Vector3 direction = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calcula o ângulo em graus.
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 45); // Ajusta a rotação. Subtraia 90 se necessário para alinhar com a frente do seu objeto.]

        transform.position = player.transform.position;

        if(Input.GetMouseButtonDown(0)){
            PlaySound(audioSource.clip);
            GameObject iShoot = Instantiate(shoot, transform.position, Quaternion.identity);

            Rigidbody2D rb = iShoot.GetComponent<Rigidbody2D>();

            rb.gravityScale = 0f;

            rb.velocity = direction.normalized * speed;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            iShoot.transform.rotation = rotation;
        }
    }
/*
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
    }*/

    public void PlaySound(AudioClip clip)
{
    if (clip != null)
    {
        // Cria uma nova instância de AudioSource
        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        
        // Atribui o clip à nova instância de AudioSource
        newAudioSource.clip = clip;
        
        // Toca o som
        newAudioSource.Play();
        
        // Destroi a instância de AudioSource após o término do som
        Destroy(newAudioSource, clip.length);
    }
    else
    {
        //Debug.LogWarning("Áudio não definido.");
    }
}


}
