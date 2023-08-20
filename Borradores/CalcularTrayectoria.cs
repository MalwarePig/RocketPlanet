using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcularTrayectoria : MonoBehaviour
{
       //Predicción de curva 
    Vector3 startPosition;
    private Rigidbody rb;
    public Vector3 currentVelocity;
    public float predictionTime = 1.0f; // Tiempo en segundos para la predicción
    public int numPoints = 50; // Número de puntos en la trayectoria 
    private LineRenderer lineRenderer;
    Vector3 dragVector;

   void Start()
    { 
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

    
    private Vector3 CalculatePredictedPosition(float time)
    {
        Vector3 trayectoVector = startPosition - dragVector;
        // Calcula y devuelve la posición predicha en el tiempo dado
        Vector3 predictedPosition = rb.position + trayectoVector * time;
        return predictedPosition;
    }

     private void Direccion(){
        Vector3 dragPosition = Input.mousePosition; //Posicion de clic en arrastre
        Ray ray = Camera.main.ScreenPointToRay(dragPosition);
        if (Physics.Raycast(ray, out RaycastHit info))
        {
            dragVector = info.point;
            dragVector.z = 0;
 
            
        }
      
    }
}
