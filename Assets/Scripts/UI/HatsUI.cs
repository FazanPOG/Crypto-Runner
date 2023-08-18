using System;
using UnityEngine;

public class HatsUI : MonoBehaviour
{
    public static Hat currentHat {  get; private set; }
    public static Hat lastHat { get; private set; }

    [SerializeField] private Hat defaultHat;
    [SerializeField] private Hat[] hats;


    public event Action<Hat> OnHatOpened;
    public event Action<Hat> OnCurrentHatChange;


    private void Awake()
    {
        currentHat = defaultHat;
    }

    private void Start()
    {
        Hat.OnSelectedHat += Hat_OnSelectedHat;
        BitcoinManager.Instance.OnBoughtHat += BitcoinManager_OnBoughtHat;

        foreach (Hat hat in hats)
        {
            if (hat.IsOpened())
            {
                OnHatOpened?.Invoke(hat);
            }
        }
    }

    private void OnDestroy()
    {
        Hat.OnSelectedHat -= Hat_OnSelectedHat;
        BitcoinManager.Instance.OnBoughtHat -= BitcoinManager_OnBoughtHat;
    }

    private void BitcoinManager_OnBoughtHat(Hat hat)
    {
        lastHat = currentHat;
        currentHat = hat;

        OnCurrentHatChange?.Invoke(currentHat);
    }

    private void Hat_OnSelectedHat(Hat hat)
    {
        lastHat = currentHat;
        currentHat = hat;

        OnCurrentHatChange?.Invoke(currentHat);
    }

}
