using System;
using UnityEngine;

public class Policeman : MonoBehaviour
{

    [SerializeField] private PolicemanTrigger policemanTrigger;
    [SerializeField] private PolicemanLevelUI policemanLevelUI;

    private bool isPolicemanWin = false;
    private bool isPolicemanLose = false;

    private int policemanLevel;
    private int playerLevel;

    public static event Action<Policeman> OnPolicemanTriggered;
    public event Action OnPolicemanWon;
    public event Action OnPolicemanLosed;

    private void Start()
    {
        policemanTrigger.OnPolicemanTriggered += PolicemanTrigger_OnPolicemanTriggered;

        policemanLevel = policemanLevelUI.GetPolicemanLevel();
    }

    private void PolicemanTrigger_OnPolicemanTriggered()
    {
        OnPolicemanTriggered?.Invoke(this);

        SoundManager.Instance.PlayHitSound(transform.position);

        playerLevel = HackerLevelManager.Instance.GetHackerLevel();

        if (policemanLevel > playerLevel)
        {
            isPolicemanWin = true;

            OnPolicemanWon?.Invoke();

        }
        if (policemanLevel <= playerLevel)
        {
            isPolicemanLose = true;

            OnPolicemanLosed?.Invoke();

        }
    }


    public int GetPolicemanLevel() 
    {
        return policemanLevel;
    }

    public bool IsPolicemanWin() 
    {
        return isPolicemanWin;
    }

    public bool IsPolicemanLose()
    {
        return isPolicemanLose;
    }
}
