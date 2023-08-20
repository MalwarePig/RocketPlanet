using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cometa : MonoBehaviour
{
    [SerializeField] public Transform start;

    [SerializeField] public Transform end;

    [SerializeField] public List<GameObject> props;

    [SerializeField] private int velocidad = 4;
    
    private float altura = -1f;
    // Update is called once per frame
    void Update()
    {  
        Spawn();
    }

    void Spawn()
    {
        
        altura = Random.Range(1,8);
        props[0].gameObject.SetActive(true); 
        foreach (GameObject prop in props)
        {
            if (prop.transform.position.x <= end.position.x)
            {
                //Detectar si esta activado
                if(props[0].transform.GetChild(0).gameObject.activeSelf == false){
                    props[0].transform.GetChild(0).gameObject.SetActive(true);
                }
                prop.transform.position = new Vector3(start.position.x, altura, prop.transform.position.z);
            }
        }

        foreach (GameObject prop in props)
        { 
            prop.transform.Translate((-1 * Time.deltaTime * Vector3.right)*velocidad);
        }
    }
}
