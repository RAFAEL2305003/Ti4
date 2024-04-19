using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPrincipal : MonoBehaviour
{
    public void IniciarJogo ()
    {
        SceneManager.LoadScene(1);
    }

    public void Sair ()
    {
        Debug.Log("saiu do jogo");
        Application.Quit();
    }
    
}
