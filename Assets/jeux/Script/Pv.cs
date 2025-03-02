using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Pv : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text vieAffichage;
    [SerializeField] int vie;


    static public Slider SliderLife;
    static TMP_Text AffichageVie;
    static int vieGameObject ;
    static int vieMax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        vieMax = vie;
        vieGameObject = vie;
        SliderLife = slider;
        AffichageVie = vieAffichage;
    }

    [ContextMenu("sdmlwdkjxgvlqsdk")]
    void degat()
    {
        ChangemementVie(Random.Range(1, 11));
    }
    public static void ChangemementVie(int degats)
    {
        vieGameObject -= degats;
        if (SliderLife != null) {
            SliderLife.value = (float)vieGameObject / (float)vieMax;
            AffichageVie.text = SliderLife.value * 100 + " %";
        }
        if (vieGameObject <= 0) {

            Application.Quit();
        }

    }


}
