using System;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public static event Action OnVictory;

    private void OnTriggerEnter(Collider other)
    {
        OnVictory?.Invoke();
    }
}
