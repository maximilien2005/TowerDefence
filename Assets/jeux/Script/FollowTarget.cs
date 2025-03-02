using NUnit.Framework;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowTarget : MonoBehaviour
{
    public List<GameObject> targetList = new List<GameObject>();
    [SerializeField] GameObject tourelle;
    [SerializeField] MonoBehaviour Attaque;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        
        if (targetList.Count > 0) {

            tourelle.transform.forward = new Vector3(targetList[0].transform.position.x - transform.position.x,0,targetList[0].transform.position.z - transform.position.z);
            
        }
            //Attaque.enabled = false;
        
         
    }


    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.transform.CompareTag("Monste") == true)
        {
            //Attaque.enabled = true;
            this.enabled = true;
            targetList.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit(Collider collision) {

        if (collision.transform.CompareTag("Monste") == true)
        {
            targetList.Remove(collision.gameObject);
        }
        if (targetList.Count > 0) { this.enabled = false; }


    }
    
}
