using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentroG : MonoBehaviour
{

   public static CentroG planeta;
   public float FuerzaG;
   public List<Rigidbody> objetos;

   /// <summary>
   /// Awake is called when the script instance is being loaded.
   /// </summary>
   void Awake()
   {
       planeta = this;
   }

 
   void FixedUpdate()
   {
    foreach (Rigidbody objeto in objetos)
    {
        Vector3 direccionG = (objeto.transform.position - transform.position).normalized;
        Vector3 direccionObjeto = objeto.transform.up;
        objeto.AddForce(direccionG * FuerzaG * Time.fixedDeltaTime);
        Quaternion nRotacion = Quaternion.FromToRotation(direccionObjeto,direccionG)*objeto.transform.rotation;
        objeto.transform.rotation = Quaternion.Slerp(objeto.transform.rotation, nRotacion, 50*Time.fixedDeltaTime);
    }
    
       
   }


    
}
