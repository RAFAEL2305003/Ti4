using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void JogarJogo ()
    {
        SceneManager.LoadScene("SampleScene");
    }

        public void SairJogo ()
    {
       Debug.Log ("saiu do jogo");
       Application.Quit();
    }

    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
