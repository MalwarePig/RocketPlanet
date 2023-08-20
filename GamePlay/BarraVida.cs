using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BarraVida : MonoBehaviour
{
    [SerializeField] public Image BarraDeVida; 
    [SerializeField] public float vidaActual = 100f;  
    [SerializeField] float vidaMax = 100f; 

    /*Menu continue*/
    private bool isShowing = false; //Panel visible?
    public GameObject menuContinue; // Assign in inspector panel

    void Start()
    {
        Debug.Log("Estado: " + vidaActual);
        BarraDeVida.fillAmount = vidaActual / vidaMax;
    }
        
   public void RestarVida()
    { 
        vidaActual = vidaActual - 12.5f;
        BarraDeVida.fillAmount = vidaActual / vidaMax;
        Debug.Log("Estado: " + vidaActual);
        if(vidaActual <= 0 ){
            MostrarMenuContinue();
            GetComponent<Temporizador>().enabled = false;
        }
    } 

    public void SumarVida()
    { 
        vidaActual = vidaActual + 25f;
        BarraDeVida.fillAmount = vidaActual / vidaMax;
        Debug.Log("Estado: " + vidaActual);
    } 

    void MostrarMenuContinue() //Muestra panel de menu continue
    {
        isShowing = !isShowing;
        menuContinue.SetActive(isShowing);
    }
}
