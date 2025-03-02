using TMPro;
using UnityEngine;

public class Monney : MonoBehaviour
{
    [SerializeField] int ArgentStart;
    [SerializeField] TMP_Text argentText;
    [SerializeField] TMP_Text argentCout;

    public static int argent;
    public static int cout;
    public static TMP_Text TextMonnaie;
    public static TMP_Text monneyCout;



    private void OnEnable()
    {
        monneyCout = argentCout;
        argent = ArgentStart;
        TextMonnaie = argentText;
        changementMoney(0);
        

    }
    public static void changementMoney(int argentsRecu)
    {
        argent += argentsRecu;
        TextMonnaie.text = argent +"";
    }
    public static void changementCout(int Cout) {
        
        cout = Cout;
        monneyCout.text = "- " + Cout;
    
    }

}
