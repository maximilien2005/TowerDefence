using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    Vector3 lastCaseSelectionPosition;
    [SerializeField] GameObject[] tourelleDispo,TourellePrefab;
    [SerializeField] GameObject tourelleActuelle;
    [SerializeField] int NumeroTourelleActuelle;
    [SerializeField] int NiveauxTourMax;
    [SerializeField] Material TransparentMaterial; 

    AudioSource Sound;
    enum UtilisationCase {placementTourelle, info, autre};

    [SerializeField]UtilisationCase mode;

    //placementTourelle
    bool TourellePlacer;
    public static int coutActionActuel;
    public static bool TourellePlacable = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sound = GetComponent<AudioSource>(); 
        TourellePrefab = new GameObject[tourelleDispo.Length];
        setTourelleDispo(TransparentMaterial);
        if (mode == UtilisationCase.placementTourelle)
        {
            TourellePrefab[0].SetActive(true);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode) { 

            case UtilisationCase.placementTourelle:
                SetChange();
                TourellePlacement();
                break;
            case UtilisationCase.info:
                break;
            case UtilisationCase.autre:
                break;
        
        }
    }



    void setTourelleDispo(Material material)
    {
        for (int i = 0; i < NiveauxTourMax; i++)
        {
            TourellePrefab[i] = Instantiate(tourelleDispo[i],new Vector3(0,0,0) , tourelleDispo[i].transform.rotation);
            
            for (int j = 0; j< TourellePrefab[i].transform.childCount; j++)
            {
                TourellePrefab[i].transform.GetChild(j).gameObject.layer = 2;
            }
            TourellePrefab[i].layer = 2;
            Destroy(TourellePrefab[i].GetComponentInChildren<FollowTarget>());
            TourellePrefab[i].SetActive(false);


            foreach (MeshRenderer renderer in TourellePrefab[i].GetComponentsInChildren<MeshRenderer>())
            {
                renderer.material = material;
            }
            TourellePrefab[i].transform.GetComponent<MeshRenderer>().material = TransparentMaterial;
        }
        
    }
    public void Follow(RaycastHit hit)
    {
        lastCaseSelectionPosition = hit.transform.position;
        TourellePlacer = false;
        if (mode == UtilisationCase.placementTourelle) {
            TourellePrefab[NumeroTourelleActuelle].transform.position = lastCaseSelectionPosition;
        }
        
    }
    void SetChange()
    {
        int scroll = (int)(Input.GetAxisRaw("Mouse ScrollWheel")*10);
        if (scroll != 0)
        {
            NumeroTourelleActuelle += scroll;

            if (NumeroTourelleActuelle < 0)
            {
                TourellePrefab[0].SetActive(false);
                NumeroTourelleActuelle = NiveauxTourMax-1;
            }
            else if (NumeroTourelleActuelle >= NiveauxTourMax) {
                TourellePrefab[NiveauxTourMax-1].SetActive(false);
                NumeroTourelleActuelle = 0;
            }
            else
            {
                TourellePrefab[NumeroTourelleActuelle - scroll].SetActive(false);
            }
            TourellePrefab[NumeroTourelleActuelle].transform.position = lastCaseSelectionPosition;
            TourellePrefab[NumeroTourelleActuelle].SetActive(true);
            
        }

        
    }

    void TourellePlacement()
    {
        if (Input.GetMouseButtonDown(0) && TourellePlacer == false && Monney.cout <= Monney.argent && TourellePlacable == true)
        {
            TourellePlacer = true;
            Instantiate(tourelleDispo[NumeroTourelleActuelle], lastCaseSelectionPosition, tourelleDispo[NumeroTourelleActuelle].transform.rotation);
            Monney.changementMoney(-Monney.cout);
        }
        else if (Input.GetMouseButtonDown(0) && Monney.argent < coutActionActuel) {

            Sound.Play();
        }

    }
}
