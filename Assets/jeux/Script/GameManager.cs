using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Mode{ spawnMap, Enjeux,Pause, PauseEntreVagye };
    public static Mode GameMod,lastGamemode;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setSiChangement();

        switch (GameMod) { 
        
            case Mode.spawnMap:

                break;
            
        
        }
    }
    void setSiChangement()
    {
        if (lastGamemode != GameMod)
        {
            lastGamemode = GameMod;
            switch (GameMod)
            {

                case Mode.spawnMap:
                    break;

                case Mode.Enjeux:

                    break;

            }
        }
    }
}
