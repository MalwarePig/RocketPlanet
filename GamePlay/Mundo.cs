using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mundo : MonoBehaviour
{
    [SerializeField]
    private float velocidadRotacion = 0;
    [SerializeField] private GameObject Planeta;
    [SerializeField] public List<GameObject> Planetas;
    [SerializeField] float rotationSpeed = 1.0f; // Velocidad de rotación del Skybox

    void Awake()
    {
        Debug.Log("Nivel" + PlayerPrefs.GetInt("Nivel"));
        Planetas[PlayerPrefs.GetInt("Nivel")].SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        Planetas[PlayerPrefs.GetInt("Nivel")].transform.Rotate(new Vector3(0,-velocidadRotacion,0));
        GirarSkyBox();
    }

    void GirarSkyBox(){
         RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
    
}

















