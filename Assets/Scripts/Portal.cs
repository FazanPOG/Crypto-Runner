using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    public static event Action OnPortalTriggered;

    private void OnTriggerEnter(Collider other)
    {
        OnPortalTriggered?.Invoke();
        foreach(var obj in gameObjects) 
        {
            obj.gameObject.SetActive(false);
        }
    }
}
