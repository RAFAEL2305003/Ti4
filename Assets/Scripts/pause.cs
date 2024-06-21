using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public static pause instance;
    public bool jogoPausado;
    public GameObject Painel_menu;
    public GameObject vida;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        vida.SetActive(true);
        Painel_menu.SetActive(false);
        Time.timeScale = 1f; //velocidade do jogo
        jogoPausado = false; // jogo começa despausado
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){ //pause
            Pausar();
        }
    }

    private void Pausar ()
    {
        if (jogoPausado == false)
            {
                Time.timeScale = 0f;
                jogoPausado = true;
                Painel_menu.SetActive(true);
                vida.SetActive(false);

            }
            else
            {
                Time.timeScale = 1f;
                jogoPausado = false;
                Painel_menu.SetActive(false);
                vida.SetActive(true);

            }
    }
}
