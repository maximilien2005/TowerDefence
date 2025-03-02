using System.Collections;
using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField] float vittesse = 35;
    [SerializeField] Vector3 positionChute = new Vector3 (0,50,0);
    

    private void OnEnable()
    {
        StartCoroutine(falling());
    }
    
    IEnumerator falling()
    {
        Vector3 objectif = transform.position;
        transform.position += positionChute;
        
        for (int i = 0; transform.position.y > objectif.y; i++)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, objectif, Time.deltaTime * vittesse);
            yield return new WaitForFixedUpdate();
        }
        transform.position = objectif;
    }

}
