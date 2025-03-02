using UnityEngine;


public class MoveMonster : MonoBehaviour
{

    [SerializeField] float vitesse;
    [SerializeField] int degats, IntervalDegatRandom;
    int index = 0;
     Vector3 TargetPosition;
    //[SerializeField] List<GameObject> ListChemain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        degats = Random.Range((degats - IntervalDegatRandom), (degats + IntervalDegatRandom + 1));
        TargetPosition = GenerationMaps.ListeChemain[index].transform.position;
        transform.forward = new Vector3(transform.position.x - TargetPosition.x, 0, transform.position.z - TargetPosition.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Mathf.Abs(transform.position.x - TargetPosition.x) <= 0.1f && Mathf.Abs(transform.position.z - TargetPosition.z) <= 0.1f)
        {
            index += 1;
            if (index == GenerationMaps.ListeChemain.Count)
            {
                Pv.ChangemementVie(degats);
                transform.gameObject.SetActive(false);
            }
            else {

                TargetPosition = GenerationMaps.ListeChemain[index].transform.position;
                transform.forward = new Vector3( TargetPosition.x - transform.position.x, 0, TargetPosition.z - transform.position.z );
            }
        }
        transform.position += transform.forward * vitesse * Time.deltaTime;

    }
}
