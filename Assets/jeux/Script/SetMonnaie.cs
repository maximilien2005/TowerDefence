using UnityEngine;

public class SetMonnaie : MonoBehaviour
{
    public int CoutAchat;

    private void OnEnable()
    {
        Monney.changementCout(CoutAchat);
    }
}
