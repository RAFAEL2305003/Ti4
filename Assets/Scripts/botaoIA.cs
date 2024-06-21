using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlighter : MonoBehaviour
{
    public Button[] buttons; // Array de botões para adicionar borda

    void Start()
    {
        foreach (Button button in buttons)
        {
            AddOutline(button);
        }
    }

    void AddOutline(Button button)
    {
        Outline outline = button.gameObject.AddComponent<Outline>();
        outline.effectColor = Color.green; // Cor da borda
        outline.effectDistance = new Vector2(9, 9); // Espessura da borda
    }
}