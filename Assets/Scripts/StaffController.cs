using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Posicao : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private GameObject player;
    public GameObject shoot;
    private AudioSource audioSource;

    public float speed = 20f;
    public Text dano;
    public Text dano1;
    public Text dano2;

public int increaseAmount = 5; // Valor pelo qual o dano será aumentado
    public int shootDamage;
    public int shootDamage1;
    public int shootDamage2;

    void Start()
    {
        shootDamage = 10;
        shootDamage1 = 10;
        shootDamage2 = 10;

        audioSource = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        dano.text = shootDamage.ToString();
        dano1.text = shootDamage1.ToString();
        dano2.text = shootDamage2.ToString();

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector3 direction = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(pause.instance.jogoPausado == false){ //travar o cajado no pause
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 45);

        transform.position = player.transform.position;

        if(Input.GetMouseButtonDown(0) && pause.instance.jogoPausado == false){
            PlaySound(audioSource.clip);
            GameObject iShoot = Instantiate(shoot, transform.position, Quaternion.identity);

            Shoot shootScript = iShoot.GetComponent<Shoot>();
            if (shootScript != null) {
                shootScript.damage = shootDamage; 
            }

            Rigidbody2D rb = iShoot.GetComponent<Rigidbody2D>();

            rb.gravityScale = 0f;

            rb.velocity = direction.normalized * speed;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            iShoot.transform.rotation = rotation;
        }
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

public void IncreaseDamage()
    {
        shootDamage += increaseAmount;
    }


public void IncreaseDamage1()
    {
        shootDamage1 += increaseAmount;
    }


public void IncreaseDamage2()
    {
        shootDamage2 += increaseAmount;
    }

}

