using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Objeto : MonoBehaviour
{
    /*Menu continue*/
    private bool isShowing = false; //Panel visible?
    public GameObject menuContinue; // Assign in inspector panel
    [SerializeField] public ParticleSystem ParticulasChoque;
    [SerializeField] public ParticleSystem ParticulasPlaneta;

     /*Interface*/
    [SerializeField] GameObject ControlBarra;
    [SerializeField] Text txtContadorLunas;

    //Contar Lunas activas
    [SerializeField] private Transform ListaLunas;

  
    // Start is called before the first frame update
    void Start()
    {
       CentroG.planeta.objetos.Add (GetComponent<Rigidbody>());//Ser atraido por centro de gravedad
       ParticulasChoque.Stop();
       ParticulasPlaneta.Stop();
    }
 
    void Awake()
    {
          ParticulasChoque.Stop();
          ParticulasPlaneta.Stop();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            ControlBarra.GetComponent<BarraVida>().RestarVida();
            StartCoroutine(Eliminarlunas(other.gameObject)); 
            
            //Destroy(other.gameObject);
        }else if (other.gameObject.CompareTag("Finish"))
        { 
            ParticulasPlaneta.Play();
            other.gameObject.SetActive(false); 
            MostrarMenuContinue();
        }else if (other.gameObject.CompareTag("Bonus"))
        { 
            ControlBarra.GetComponent<BarraVida>().SumarVida();
            ParticulasChoque.Play();
            other.gameObject.SetActive(false); 
            Debug.Log("+Up");
        }
    }

    IEnumerator Eliminarlunas(GameObject other){ 
        ParticulasChoque.Play();
         yield return new WaitForSeconds(1.0f);
         other.gameObject.SetActive(false);
         ContarHijosActivos();
    }

    IEnumerator EliminarPlaneta(GameObject other){ 
         yield return new WaitForSeconds(5.0f);
         other.gameObject.SetActive(false);
         MostrarMenuContinue();
    }


    void MostrarMenuContinue() //Muestra panel de menu continue
    {
        isShowing = !isShowing;
        menuContinue.SetActive(isShowing);
    }

     void ContarHijosActivos(){
        int vivos = 0;
        foreach (Transform child in ListaLunas)
        {
            // Accede a los componentes y propiedades de cada hijo si es necesario
            Debug.Log(child.gameObject.activeSelf);
            if(child.gameObject.activeSelf == true){
                vivos++;
            }
        }
        txtContadorLunas.text = vivos.ToString();
    }
 
     
}
