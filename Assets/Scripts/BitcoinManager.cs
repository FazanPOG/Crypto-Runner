using System;
using UnityEngine;

public class BitcoinManager : MonoBehaviour
{
    public static BitcoinManager Instance {get; private set;}

    [SerializeField] private int bitcoinCounter;

    public event Action OnBitcoinCounterChangedValue;
    public event Action<Hat> OnBoughtHat;

    private void Awake()
    {
        Instance = this;

        bitcoinCounter = ProgressManager.LoadBitcoinCounter();
    }
    private void Start()
    {
        Bitcoin.OnBitcoinCollect += Bitcoin_OnBitcoinCollect;
        Hat.OnTriedBuyHat += Hat_OnTriedBuyHat;
        TotalRewardUI.OnTotalRewardCollected += TotalRewardUI_OnTotalRewardCollected;
    }

    private void OnDestroy()
    {
        Bitcoin.OnBitcoinCollect -= Bitcoin_OnBitcoinCollect;
        Hat.OnTriedBuyHat -= Hat_OnTriedBuyHat;
        TotalRewardUI.OnTotalRewardCollected -= TotalRewardUI_OnTotalRewardCollected;
    }

    private void TotalRewardUI_OnTotalRewardCollected(int totalReward)
    {
        bitcoinCounter += totalReward;

        ProgressManager.SaveBitcoinCounter(bitcoinCounter);
        OnBitcoinCounterChangedValue?.Invoke();
    }

    private void Hat_OnTriedBuyHat(Hat hat)
    {
        int cost = hat.GetCost();
        if (bitcoinCounter >= cost) 
        {
            bitcoinCounter -= cost;

            ProgressManager.SaveBitcoinCounter(bitcoinCounter);
            OnBitcoinCounterChangedValue?.Invoke();
            OnBoughtHat?.Invoke(hat);
        }
    }

    private void Bitcoin_OnBitcoinCollect(Bitcoin obj)
    {
        bitcoinCounter++;

        ProgressManager.SaveBitcoinCounter(bitcoinCounter);
        OnBitcoinCounterChangedValue?.Invoke();
    }


    public int GetBitcoinCounter() 
    {
        return bitcoinCounter;
    }
}
