using Unity.VisualScripting;
using UnityEngine;

public class AttaqueVehicule : MonoBehaviour
{
    [SerializeField] int degats, nbdeTir;
    float time;
    [SerializeField] float cooldown;
    FollowTarget Cible;

    private void Start()
    {
        
        Cible = GetComponent<FollowTarget>();
    }
    private void OnEnable()
    {
        time = cooldown + 1;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > cooldown) {
            time = 0;
            Debug.Log("Piou");
            if (Cible.targetList.Count > 0 && Cible.targetList[0] != null )
            {
                Cible.targetList[0].GetComponent<LifeMonstre>().ChangementVie(degats * nbdeTir);
            }
                


        }


        
    }
}
