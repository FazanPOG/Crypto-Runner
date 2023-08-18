using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static event Action<Finish> OnFinish;

    private void OnTriggerEnter(Collider other)
    {
        OnFinish?.Invoke(this);
    }
}
