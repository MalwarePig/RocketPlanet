using System.Collections;
using System.Collections.Generic;
/* using UnityEditor.IMGUI.Controls; */
using UnityEngine;

public class ControlLanzamiento : MonoBehaviour
{
    //Sistema de lanzamiento
    private Rigidbody rb;
    private Camera mainCamera;
    Vector3 startPosition, ClampedPosition, dragVector,trayectoVector; //Vector de arrastre
    [SerializeField] float fuerza; 
    [SerializeField] float maxDistance;

    //Predicción de curva 
    public Vector3 currentVelocity;
    public float predictionTime = 1.0f; // Tiempo en segundos para la predicción
    public int numPoints = 50; // Número de puntos en la trayectoria 
    private LineRenderer lineRenderer; 
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPosition = transform.position; 

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
    }

    private void Update()
    {
        currentVelocity = rb.velocity;
        Vector3[] positions = new Vector3[numPoints];
        
        for (int i = 0; i < numPoints; i++)
        {
            float time = i * predictionTime / (numPoints - 1);
            positions[i] = CalculatePredictedPosition(time);
        }

        lineRenderer.SetPositions(positions);
    }

    private void OnMouseDrag()
    {
        Direccion();
    }

    private void Direccion(){
        Vector3 dragPosition = Input.mousePosition; //Posicion de clic en arrastre
        Ray ray = Camera.main.ScreenPointToRay(dragPosition);
        if (Physics.Raycast(ray, out RaycastHit info))
        {
            dragVector = info.point;
            dragVector.z = 0;

            ClampedPosition = dragVector;

            float dragDistance = Vector3.Distance(startPosition, dragVector);
            if (dragDistance > maxDistance)
            {
                ClampedPosition =
                    startPosition + (dragVector - startPosition).normalized * maxDistance;
            }

            transform.position = ClampedPosition;
        }
      
    }

    private void OnMouseUp() {
        Impulsar();
     }

    private void Impulsar()
    {
        rb.isKinematic = false;
        //Calcular trayectoria
        trayectoVector = startPosition - ClampedPosition;
        rb.AddForce(trayectoVector * fuerza);
        
    } 
    

    private Vector3 CalculatePredictedPosition(float time)
    {
        trayectoVector = startPosition - ClampedPosition;
        // Calcula y devuelve la posición predicha en el tiempo dado
        Vector3 predictedPosition = rb.position + trayectoVector * time;
        return predictedPosition;
    }
}
