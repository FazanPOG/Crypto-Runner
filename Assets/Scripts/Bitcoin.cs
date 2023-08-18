using System;
using UnityEngine;

public class Bitcoin : MonoBehaviour
{
    public static event Action<Bitcoin> OnBitcoinCollect;

    private void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlayBitcoinSound(transform.position);
        OnBitcoinCollect?.Invoke(this);
        Destroy(this.gameObject);
    }
}
