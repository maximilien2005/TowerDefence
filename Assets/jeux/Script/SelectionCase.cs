using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionCase : MonoBehaviour
{
    [SerializeField] GameObject LastHitCaseDispo ;
    [SerializeField] Material materialSelection, MaterialTransparent;
    [SerializeField]Material SauvegardeMaterial;
    RaycastHit hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction*1000, Color.red);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {

            if (hit.transform != LastHitCaseDispo.transform && hit.transform.tag == "CaseDispo")
            {
                LastHitCaseDispo.transform.GetComponent<MeshRenderer>().material = SauvegardeMaterial;
                SauvegardeMaterial = hit.transform.GetComponent<MeshRenderer>().material;
                hit.transform.GetComponent<Renderer>().material = materialSelection;
                LastHitCaseDispo = hit.transform.gameObject;

                GetComponent<FollowMouse>().Follow(hit);
            }
        }
        
        
        
        
    }
}
