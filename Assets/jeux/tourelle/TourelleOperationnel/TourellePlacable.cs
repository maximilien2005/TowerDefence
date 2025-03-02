using System.Collections.Generic;
using UnityEngine;

public class TourellePlacable : MonoBehaviour
{
    public List<GameObject> collisionActuelle = new List<GameObject>();
    [SerializeField] Material Transparent, Collision;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != null && (collision.transform.CompareTag("Chemain") == true || collision.transform.CompareTag("Chateaux"))){

            collisionActuelle.Add(collision.transform.gameObject);
            ChangementCouleur(Collision);
            if (this.enabled == true) { FollowMouse.TourellePlacable = false; }
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag != null && (collision.transform.CompareTag("Chemain") == true || collision.transform.CompareTag("Chateaux")))
        {
            Debug.Log(collisionActuelle.Contains(collision.transform.gameObject));
            collisionActuelle.Remove(collision.gameObject);
            if (collisionActuelle.Count == 0)
            {
                if (this.enabled == true) {
                    FollowMouse.TourellePlacable = true;
                }
                ChangementCouleur(Transparent);            }
        }
        
        
        
    }

    void ChangementCouleur(Material material)
    {
        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = material;
        }
    }

}
