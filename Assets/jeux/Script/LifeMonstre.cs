using UnityEngine;

public class LifeMonstre : MonoBehaviour
{
    [SerializeField]int intervalVie;
    public int vie;
    int lootMoney;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        vie = Random.Range(vie-intervalVie, vie+intervalVie+1);
        lootMoney = vie;
    }

    public void ChangementVie(int degats)
    {
        vie -= degats;
        if (vie <= 0) {
            Monney.changementMoney(lootMoney);
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
