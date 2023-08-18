using UnityEngine;

public class PolicemanVisual : MonoBehaviour
{

    private const string IS_POLICEMAN_WON = "IsPolicemanWon";
    private const string IS_POLICEMAN_LOSED = "IsPolicemanLosed";

    [SerializeField] private Policeman policeman;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        policeman.OnPolicemanLosed += Policeman_OnPolicemanLosed;
        policeman.OnPolicemanWon += Policeman_OnPolicemanWon;

    }


    private void Policeman_OnPolicemanWon()
    {
        animator.SetTrigger(IS_POLICEMAN_WON);
    }

    private void Policeman_OnPolicemanLosed()
    {
        animator.SetTrigger(IS_POLICEMAN_LOSED);
    }
}
