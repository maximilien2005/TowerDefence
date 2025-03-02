using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerationMaps : MonoBehaviour
{
    //debug
    [SerializeField] Material debugRaycasthit, MegaExclude, chemainMaterial;

    //donnerSauvegarder 
    GameObject Tuiles;
    [SerializeField] GameObject[] tuilesExclus;
    int TuilesExclusIndex = 0;

    List<GameObject> ListeTuiles = new List<GameObject>();
    public static List<GameObject> ListeChemain = new List<GameObject>();

    //Spawn tuiles
    [SerializeField] float espace;
    float angle;
    [SerializeField] Transform angleRef;
    [SerializeField] int gridRadius = 2;
    [SerializeField] GameObject Tuile;
    [SerializeField] float DeplacementHauteur = 2, DeplacementLongueur = 1.75f;
    [SerializeField] int NbCote = 6;
    // Spawn Monstre ET Chateaux
    [SerializeField] GameObject SpawnMonstre, Chateaux, chemain;
    [SerializeField] int IndexExclusionLayer;
    [SerializeField] int MinimumChemain;


    void Start()
    {
        angle = 360 / NbCote;
        StartCoroutine(SpawnTuiles());

    }


    IEnumerator SpawnTuiles()
    {
        Vector3 VectorStart;
        
        ListeTuiles.Add(Instantiate(Tuile, new Vector3(0, 0, 0), Tuile.transform.rotation));
        for (int i = 0; i < gridRadius; i++)
        {
            angleRef.rotation = Tuile.transform.rotation;
            VectorStart = new Vector2(-DeplacementHauteur / 2 * i, DeplacementLongueur * i);

            //nouveaux Radius
            for (int cote = 0; cote < NbCote; cote++)
            {
                yield return new WaitForSeconds(0);
                for (int nbSurLaLigne = 0; nbSurLaLigne < i; nbSurLaLigne++)
                {
                    VectorStart += (espace + DeplacementLongueur) * angleRef.right;
                    ListeTuiles.Add(Instantiate(Tuile, new Vector3(VectorStart.x, 0, VectorStart.y), Tuile.transform.rotation));
                }
                angleRef.rotation *= Quaternion.Euler(0, angle, 0);

            }


        }
        yield return new WaitForSeconds(1);
        StartCoroutine(SpawnObjectifAndSpawnMonster());
    }
    IEnumerator SpawnObjectifAndSpawnMonster() {
        int randomIndexMonstre = RandomIndexTuilesMap();
        ListeTuiles[randomIndexMonstre].layer = IndexExclusionLayer;
        SpawnMonstre = Instantiate(SpawnMonstre, ListeTuiles[randomIndexMonstre].transform.position, SpawnMonstre.transform.rotation);

        //Verifier que se n'est pas la meme case et spawn Base Joueur
        int randomIndexChateaux = RandomIndexTuilesMap();
        for (int i = 0; ListeTuiles[randomIndexChateaux].layer != IndexExclusionLayer; i++)
        {
            randomIndexChateaux = RandomIndexTuilesMap();
        }
        ListeTuiles[randomIndexChateaux].layer = IndexExclusionLayer;
        Chateaux = Instantiate(Chateaux, new Vector3(ListeTuiles[randomIndexChateaux].transform.position.x, 0, ListeTuiles[randomIndexChateaux].transform.position.z), SpawnMonstre.transform.rotation);

        yield return new WaitForSeconds(2f);
        SpawnChemain();
    }


    void SpawnChemain()
    {

        ListeChemain.Add(SpawnMonstre);
        angleRef.rotation = chemain.transform.rotation;

        List<Vector3> possibiliter = new List<Vector3>();
        RaycastHit hit;
        Vector3 PositionRaycast, LastPosition = SpawnMonstre.transform.position;
        PositionRaycast = new Vector3(SpawnMonstre.transform.position.x, 0, SpawnMonstre.transform.position.z) + new Vector3(-DeplacementHauteur / 2, 0, DeplacementLongueur);


        //spawnFirstElement 6 choix
        for (int i = 0; i < NbCote; i++)
        {


            if (Physics.Raycast(PositionRaycast + new Vector3(0, 50, 0), -Vector3.up, out hit))
            {

                if (hit.transform.tag == "CaseDispo" && hit.transform.gameObject.layer != 3)
                {
                    hit.transform.gameObject.layer = 3;
                    hit.transform.gameObject.GetComponent<Renderer>().material = debugRaycasthit;
                    possibiliter.Add(new Vector3(hit.transform.position.x, 0, hit.transform.position.z));
                }
            }
            PositionRaycast += (espace + DeplacementLongueur) * angleRef.right;
            angleRef.rotation *= Quaternion.Euler(0, 0, angle);
        }
        //choisir un chemain
        int random = Random.Range(0, possibiliter.Count);
        ListeChemain.Add(Instantiate(chemain, possibiliter[random], chemain.transform.rotation));




        // spawn Next Element
        for (int objectif = 0; objectif < 10000; objectif++)
        {
            Debug.Log("1");
            //set devant
            possibiliter = new List<Vector3>();
            PositionRaycast = ListeChemain[ListeChemain.Count - 1].transform.position;

            angleRef.forward = ListeChemain[ListeChemain.Count - 1].transform.position - ListeChemain[ListeChemain.Count - 2].transform.position;


            //3RaycastPour3Position
            if (Physics.Raycast(PositionRaycast + DeplacementHauteur / 2 * angleRef.forward + new Vector3(0, 1, 0) + DeplacementLongueur * angleRef.right, -Vector3.up, out hit))
            {
                if (hit.transform.tag == "CaseDispo" && hit.transform.gameObject.layer != 3 && hit.transform.gameObject.layer != 6)
                {
                    hit.transform.gameObject.layer = 3;
                    hit.transform.gameObject.GetComponent<Renderer>().material = debugRaycasthit;
                    possibiliter.Add(new Vector3(hit.transform.position.x, 0, hit.transform.position.z));
                }
                else if (hit.transform.CompareTag("Chateaux") == true)
                {

                    if (ListeChemain.Count >= MinimumChemain) {
                        break;
                    }


                }
            }
            if (Physics.Raycast(PositionRaycast + DeplacementHauteur / 2 * angleRef.forward + new Vector3(0, 1, 0) - DeplacementLongueur * angleRef.right, -Vector3.up, out hit))
            {
                if (hit.transform.tag == "CaseDispo" && hit.transform.gameObject.layer != 3 && hit.transform.gameObject.layer != 6)
                {
                    hit.transform.gameObject.layer = 3;
                    hit.transform.gameObject.GetComponent<Renderer>().material = debugRaycasthit;
                    possibiliter.Add(new Vector3(hit.transform.position.x, 0, hit.transform.position.z));
                }
                else if (hit.transform.CompareTag("Chateaux") == true)
                {
                    if (ListeChemain.Count >= MinimumChemain) {
                        break;
                    }

                }
            }

            // RaycastDevant
            if (Physics.Raycast(PositionRaycast + DeplacementHauteur * angleRef.forward + new Vector3(0, 1, 0), -Vector3.up, out hit))
            {
                if (hit.transform.tag == "CaseDispo" && hit.transform.gameObject.layer != 3 && hit.transform.gameObject.layer != 6)
                {
                    hit.transform.gameObject.layer = 3;
                    hit.transform.gameObject.GetComponent<Renderer>().material = debugRaycasthit;
                    possibiliter.Add(new Vector3(hit.transform.position.x, 0, hit.transform.position.z));
                }
                else if (hit.transform.CompareTag("Chateaux") == true)
                {
                    if (ListeChemain.Count >= MinimumChemain) {
                        break;
                    }
                }
            }

            if (possibiliter.Count == 0)
            {
                if (ListeChemain.Count > 1) // Éviter une erreur si ListeChemain n'a qu'un seul élément
                {
                    // Désactiver et supprimer la dernière position du chemin
                    ListeChemain[ListeChemain.Count - 1].SetActive(false);
                    Destroy(ListeChemain[ListeChemain.Count - 1]);
                    ListeChemain.RemoveAt(ListeChemain.Count - 1);


                    Vector3 gauche = PositionRaycast - DeplacementHauteur / 2 * angleRef.forward + new Vector3(0, 1, 0) - DeplacementLongueur * angleRef.right;
                    Vector3 droite = PositionRaycast - DeplacementHauteur / 2 * angleRef.forward + new Vector3(0, 1, 0) + DeplacementLongueur * angleRef.right;
                    Debug.DrawRay(PositionRaycast, Vector2.up, Color.yellow);
                    Debug.DrawRay(gauche, -Vector2.up, Color.green);
                    Debug.DrawRay(droite, -Vector2.up, Color.white);
                    // Vérification de la position actuelle

                    if (Physics.Raycast(PositionRaycast + new Vector3(0, 1, 0), -Vector3.up, out hit))
                    {
                        ExcludeTuiles(hit.transform.gameObject);
                    }
                    // Vérifier les deux chemins adjacents

                    if (Physics.Raycast(droite, -Vector3.up, out hit))
                    {
                        if (hit.transform.gameObject.layer == 3) // Vérifie si c'est un chemin non exploré
                        {
                            hit.transform.gameObject.layer = 0; // Marquer comme accessible
                            hit.transform.gameObject.GetComponent<Renderer>().material = chemainMaterial;
                        }
                    }
                    if (Physics.Raycast(gauche, -Vector3.up, out hit))
                    {
                        if (hit.transform.gameObject.layer == 3) // Vérifie si c'est un chemin non exploré
                        {
                            hit.transform.gameObject.layer = 0; // Marquer comme accessible
                            hit.transform.gameObject.GetComponent<Renderer>().material = chemainMaterial;
                        }
                    }

                    // Définir la dernière position sur laquelle on revient
                    LastPosition = ListeChemain[ListeChemain.Count - 1].transform.position;
                }
            }
            else
            {
                random = Random.Range(0, possibiliter.Count);
                LastPosition = PositionRaycast;
                ListeChemain.Add(Instantiate(chemain, possibiliter[random], chemain.transform.rotation));

            }
        }
        
        ListeChemain.Add(Chateaux);
        GameManager.GameMod = GameManager.Mode.Enjeux;

        
    }


    int RandomIndexTuilesMap()
    {
        // Sélectionne d'abord une ligne aléatoire
        int randomLigne = Random.Range(0, gridRadius);
        //nombre d'elementAvant la ligne
        int Element = 0;
        for (int value = 1; value < randomLigne; value++)
        {
            Element += value * NbCote;
        }
        //prendre un element de la ligne aleatoir
        Element += Random.Range(0, NbCote * randomLigne);

        return Element;
    }

    void ExcludeTuiles(GameObject tuilesAExclure){


        if (tuilesExclus[TuilesExclusIndex] != null)
        {
            tuilesExclus[TuilesExclusIndex].transform.gameObject.layer = 0;
            tuilesExclus[TuilesExclusIndex].transform.gameObject.GetComponent<Renderer>().material = chemainMaterial;
        }
        tuilesExclus[TuilesExclusIndex] = tuilesAExclure;
        tuilesExclus[TuilesExclusIndex].transform.gameObject.layer = 6;
        tuilesExclus[TuilesExclusIndex].transform.gameObject.GetComponent<Renderer>().material = MegaExclude;
        
        TuilesExclusIndex++;
        if (TuilesExclusIndex == tuilesExclus.Length -1) { 
            TuilesExclusIndex = 0;
        }
    }

}





