using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlighter : MonoBehaviour
{
    public Button[] buttons; // Array de botões para adicionar borda
    private Incineracao Incineracao;
    private RaioDeSubmissao Submissao;
    private RaioUnicChGerador Tempestade;
    private CirculoDeFogo Espiral;
    private Explosion Explosao;
    private GameObject Mago;

    void Start()
    {
        Mago = GameObject.Find("Mago");
        Tempestade = Mago.transform.Find("PowersIndependentes").transform.Find("RaioUnicoCh").GetComponent<RaioUnicChGerador>();
        Incineracao = Mago.transform.Find("incineracaoI_-1").GetComponent<Incineracao>();
        Submissao = Mago.transform.Find("RaioDeSubPai").transform.Find("RaioDeSubmissao2").GetComponent<RaioDeSubmissao>();
        Espiral = Mago.transform.Find("hd_restoration_result_image_0").GetComponent<CirculoDeFogo>();
        Explosao = Mago.transform.Find("explosion_-1").GetComponent<Explosion>();

        if(Espiral.level <= 0.7){
            if(Explosao.level <= 0.7){
                if(Submissao.level <= 0.7){
                    if(Incineracao.level <= 0.7){
                        AddOutline(buttons[1]);
                    }
                    else{
                        AddOutline(buttons[3]);
                    }
                }
                else{
                    if(Submissao.level <= 0.9){
                        AddOutline(buttons[0]);
                    }
                    else{
                        AddOutline(buttons[3]);
                    }
                }
            }
            else{
                if(Explosao.level <= 0.9){
                    AddOutline(buttons[4]);
                }
                else{
                    if(Submissao.level <= 0.7){
                        AddOutline(buttons[4]);
                    }
                    else{
                        AddOutline(buttons[0]);
                    }
                }
            }
        }
        else{
            if(Espiral.level <= 0.9){
                AddOutline(buttons[2]);
            }
            else{
                if(Submissao.level <= 0.7){
                    if(Incineracao.level <= 0.7){
                        AddOutline(buttons[2]);
                    }
                    else{
                        AddOutline(buttons[3]);
                    }
                }else{
                    if(Submissao.level <= 0.9){
                        AddOutline(buttons[0]);
                    }else{
                        AddOutline(buttons[2]);
                    }
                }
            }
        }
    }

    void AddOutline(Button button)
    {
        Outline outline = button.gameObject.AddComponent<Outline>();
        outline.effectColor = Color.green; // Cor da borda
        outline.effectDistance = new Vector2(9, 9); // Espessura da borda
    }
}