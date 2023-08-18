using System;
using UnityEngine;

public class PolicemanTrigger : MonoBehaviour
{
    [SerializeField] private Policeman policeman;

    public event Action OnPolicemanTriggered;

    private void OnTriggerEnter(Collider other)
    {
        OnPolicemanTriggered?.Invoke();
    }
}
