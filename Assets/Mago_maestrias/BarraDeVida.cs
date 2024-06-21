using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider slider;
    public static BarraDeVida Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        slider = GetComponent<Slider>();
    }

    public void colocarVidaMaxima(float vida)
    {
        slider.maxValue = vida;
        slider.value = vida;
        slider.interactable = false;
    }

    public void alterarVida(float vida) 
    {
        slider.value = vida;
    }
}
