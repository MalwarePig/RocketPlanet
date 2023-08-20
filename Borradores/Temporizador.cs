using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    public float currentTime = 60.0f; // Tiempo actual del temporizador
    public Text Cronometro;

    /*Menu continue*/
    private bool isShowing = false; //Panel visible?
    public GameObject menuContinue; // Assign in inspector panel

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Resta el tiempo del frame actual del tiempo total
        currentTime -= Time.deltaTime; 
        if (currentTime>0)
        {
           Cronometro.text = Mathf.RoundToInt(currentTime).ToString(); 
        }
       
        // Si el tiempo actual supera el tiempo total, el temporizador ha terminado
        if (currentTime == 0 && isShowing == false)
        { 
            MostrarMenuContinue();
        }
    }

    void MostrarMenuContinue() //Muestra panel de menu continue
    {
        isShowing = !isShowing;
        menuContinue.SetActive(isShowing);
    }
}
