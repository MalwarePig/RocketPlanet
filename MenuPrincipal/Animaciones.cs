using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animaciones : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 0;
    [SerializeField] public List<GameObject> props;
    [SerializeField] float rotationSpeed = 1.0f; // Velocidad de rotación del Skybox
    // Start is called before the first frame update
    
     void Update()
    {
        foreach (var Planeta in props)
        {
            Planeta.transform.Rotate(new Vector3(0,-velocidadRotacion,0));
        }
        
        GirarSkyBox();
    }

    void GirarSkyBox(){
         RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
