using System.Collections.Generic;
using UnityEngine;

public class SpawnMonstre : MonoBehaviour
{
    float time;
    [SerializeField] float intervalSpawn;
    [SerializeField] GameObject[] typeDeMonstre;
    [SerializeField] Dictionary<string, GameObject[]> saveMonster;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameMod == GameManager.Mode.Enjeux)
        {
            time += Time.deltaTime;

            if (time > intervalSpawn) {
                intervalSpawn -= 0.1f;
                time = 0;
                spawnMonster();

            }

        }
    }

    void spawnMonster()
    {
        GameObject monstre = typeDeMonstre[Random.Range(0, typeDeMonstre.Length)];
        Instantiate(monstre, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

    }
}

