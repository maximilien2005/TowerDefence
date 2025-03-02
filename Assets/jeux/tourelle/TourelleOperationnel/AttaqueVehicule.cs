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

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > cooldown) {
            time = 0;
            if (Cible.targetList.Count > 0)
            {
                Cible.targetList[0].GetComponent<LifeMonstre>().vie -= degats * nbdeTir;
            }
                


        }


        
    }
}
