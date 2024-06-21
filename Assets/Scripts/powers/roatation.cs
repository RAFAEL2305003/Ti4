using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roatation : MonoBehaviour
{
    public float rotationSpeed = 200f; // Velocidade de rotação em graus por segundo

    private float timer; // Contador de tempo
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CountToTen());

        //rend = GetComponent<Renderer>();
        //timer = visibleTime; // Começa visível
        //SetVisibility(false);
    }

    public void FixedUpdate() {

        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
