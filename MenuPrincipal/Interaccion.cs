using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaccion : MonoBehaviour
{
    [SerializeField] Transform cubo;
    private int Planeta = 0;

    public void Right(){
        if(Planeta<4){
            Planeta++;
        } 
        cubo.Rotate(new Vector3(0,-90,0));
    }

    public void Left(){
        if(Planeta>0){
            Planeta--;
        }
        
        cubo.Rotate(new Vector3(0,90,0));
    }
    
    public void Play()
    {
        PlayerPrefs.SetInt("Nivel",Planeta);
        SceneManager.LoadScene("Game");
    }
}