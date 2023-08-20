using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public GameObject objectToCreate; // Prefab del objeto que deseas crear
    public float launchForce = 10f; // Fuerza con la que se lanzará el proyectil

    void Update()
    {
        // Detecta si se hace clic con el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Obtiene la posición del clic en pantalla
            Vector3 clickPosition = Input.mousePosition;

            // Convierte la posición del clic de pantalla a una posición en el mundo
            //  Vector3 worldClickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            clickPosition.z = 0; // Asegura que la posición z sea 0 para estar en el mismo plano

            Debug.Log(clickPosition);

            Ray ray = Camera.main.ScreenPointToRay(clickPosition);

            Debug.Log(clickPosition);

            if (Physics.Raycast(ray, out RaycastHit info))
            {
                Vector3 intantationPoint = info.point;
                // Crea el objeto en la posición del clic
                //Instantiate(objectToCreate, intantationPoint, Quaternion.identity);
                GameObject newBala = (GameObject)Instantiate(
                    objectToCreate,
                    intantationPoint,
                    Quaternion.identity
                );
                
                newBala.GetComponent<Rigidbody>().AddForce(transform.up * launchForce, ForceMode.Impulse);
            }

            //newBala.GetComponent<Rigidbody>().AddForce(0,5, 0, ForceMode.Impulse);
        }
    }
}
