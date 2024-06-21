using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WonTransition : MonoBehaviour
{
    public float delayBeforeLoading = 0.75f;  // Tempo em segundos antes de carregar a próxima cena
    public string sceneToLoad = "SampleScene";  // Nome da cena a ser carregada

    void Start()
    {
        // Inicia a coroutine de transição
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        // Aguarda o tempo especificado antes de carregar a próxima cena
        yield return new WaitForSeconds(delayBeforeLoading);

        // Carrega a próxima cena
        SceneManager.LoadScene(sceneToLoad);
    }
}