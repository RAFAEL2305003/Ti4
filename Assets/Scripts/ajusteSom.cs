using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Inicializa o valor do slider com o volume atual do AudioSource
        volumeSlider.value = audioSource.volume;

        // Adiciona um listener ao slider para chamar o método OnVolumeChange sempre que o valor mudar
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void OnVolumeChange(float value)
    {
        // Atualiza o volume do AudioSource com o valor do slider
        audioSource.volume = value;
    }
}