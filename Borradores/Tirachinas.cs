using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tirachinas : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    Vector3 startPosition,
        ClampedPosition; //Vector de arrastre

    [SerializeField]
    float fuerza;

    [SerializeField]
    float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPosition = transform.position;
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
            Vector3 dragVector = info.point;
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
        Vector3 trayectoVector = startPosition - ClampedPosition;
        rb.AddForce(trayectoVector * fuerza);
    }
}
