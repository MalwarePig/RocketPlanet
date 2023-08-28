using System.Collections;
using System.Collections.Generic;
/* using JetBrains.Rider.Unity.Editor; */
using UnityEngine;
using UnityEngine.UI;

public class ControlPlayer : MonoBehaviour
{
    GameObject newPlanet;
    private Rigidbody rb;
    private Rigidbody rbBlackHole;
    public GameObject objectToCreate; // Prefab del objeto que deseas crear
    public GameObject BlackHole; // Prefab del objeto que deseas crear
    public float launchForce = 5f; // Fuerza con la que se lanzará el proyectil
    Vector3 startPosition, dragVector; //Vector de arrastre 
    private LineRenderer lineRenderer;
    public float predictionTime = 1.0f; // Tiempo en segundos para la predicción
    public int numPoints = 50; // Número de puntos en la trayectoria 
    public Vector3 currentVelocity; 

    //Contar Lunas activas
    [SerializeField] private Transform ListaLunas; 
    [SerializeField] Text txtContadorLunas;
 
 void Start()
    {
        Debug.Log("Planeta Actual: " + PlayerPrefs.GetInt("Nivel"));
        rb = objectToCreate.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        rbBlackHole = BlackHole.GetComponent<Rigidbody>();
        rbBlackHole.isKinematic = true;

        startPosition = transform.position;

        lineRenderer = BlackHole.GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
         DibuarPrediccion();
    }
//Obtener primera coordenada
    private void OnMouseDown() {
        // Detecta si se hace clic con el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Obtiene la posición del clic en pantalla
            Vector3 clickPosition = Input.mousePosition;  
            // Convierte la posición del clic de pantalla a una posición en el mundo 
            clickPosition.z = 0; // Asegura que la posición z sea 0 para estar en el mismo plano
            Ray ray = Camera.main.ScreenPointToRay(clickPosition); 

            if (Physics.Raycast(ray, out RaycastHit info))
            {
                startPosition = info.point;  
                rbBlackHole.position = startPosition;
            }  
        }
    } 

     private void OnMouseDrag()
    {
        lineRenderer.enabled = true;
        Direccion(); 
    }

    private void Direccion(){
        Vector3 dragPosition = Input.mousePosition; //Posicion de clic en arrastre
        Ray ray = Camera.main.ScreenPointToRay(dragPosition);
        if (Physics.Raycast(ray, out RaycastHit info))
        {
            dragVector = info.point;
            dragVector.z = 0;
 
            //float dragDistance = Vector3.Distance(startPosition, dragVector);
            transform.position = dragVector; 
        }
    }

    private void OnMouseUp() {
        Impulsar();
        lineRenderer.enabled = false;
     }

    private void Impulsar()
    {
        rb.isKinematic = false;
        //Calcular trayectoria
        Vector3 trayectoVector = startPosition - dragVector;
        newPlanet = (GameObject)Instantiate(objectToCreate,startPosition,Quaternion.identity);
        newPlanet.GetComponent<Rigidbody>().AddForce(trayectoVector * launchForce);
        newPlanet.GetComponent<MeshRenderer>().enabled = true; 
        newPlanet.GetComponent<SphereCollider>().isTrigger = false; 
        // Asigna el objeto hijo al objeto padre
        newPlanet.transform.SetParent(ListaLunas);
        ContarHijosActivos();
    }

    private Vector3 CalculatePredictedPosition(float time)
    {
        Vector3 trayectoVector = startPosition - dragVector;
        // Calcula y devuelve la posición predicha en el tiempo dado
        Vector3 predictedPosition = rbBlackHole.position + trayectoVector * time;
        return predictedPosition;
    }

    void DibuarPrediccion(){
        currentVelocity = rb.velocity;
        Vector3[] positions = new Vector3[numPoints];
        
        for (int i = 0; i < numPoints; i++)
        {
            float time = i * predictionTime / (numPoints - 1);
            positions[i] = CalculatePredictedPosition(time);
        }
        lineRenderer.SetPositions(positions);
    } 

    
     void ContarHijosActivos(){
        int vivos = 0;
        foreach (Transform child in ListaLunas)
        {
            // Accede a los componentes y propiedades de cada hijo si es necesario
            Debug.Log(child.gameObject.activeSelf);
            if(child.gameObject.activeSelf){
                vivos++;
            }
        }
        txtContadorLunas.text = vivos.ToString();
    }
}
