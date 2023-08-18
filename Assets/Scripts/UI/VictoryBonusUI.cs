using System;
using UnityEngine;
using YG;

public class VictoryBonusUI : MonoBehaviour
{

    private bool hasFreeRewards = true;

    private int numberOfRewards;
    private int numberOfCollectedRewards = 0;
    private int numberOfRewardsMin = 1;
    private int numberOfRewardsMax = 3;

    public static event Action<VictoryRewardUI> OnRewardCollect;
    public static event Action<VictoryRewardUI> OnHasNotFreeRewards;

    private void Awake()
    {
        numberOfRewards = GenerateNumberOfRewards();
    }

    private void Start()
    {
        VictoryRewardUI.OnFreeRewardTaked += VictoryRewardUI_OnFreeRewardTaked;
        VictoryRewardUI.OnVideoRewardTaked += VictoryRewardUI_OnVideoRewardTaked;
    }

    private void OnDestroy()
    {
        VictoryRewardUI.OnFreeRewardTaked -= VictoryRewardUI_OnFreeRewardTaked;
        VictoryRewardUI.OnVideoRewardTaked -= VictoryRewardUI_OnVideoRewardTaked;
    }

    private void VictoryRewardUI_OnVideoRewardTaked(VictoryRewardUI victoryVideoReward)
    {
        //WATCH VIDEO
        YandexGame.RewVideoShow(0);
        Debug.Log("WATCH VIDEO");

        OnRewardCollect?.Invoke(victoryVideoReward);
    }

    private void VictoryRewardUI_OnFreeRewardTaked(VictoryRewardUI victoryFreeReward)
    {
        if (numberOfCollectedRewards < numberOfRewards) 
        {
            OnRewardCollect?.Invoke(victoryFreeReward);

            if (numberOfCollectedRewards + 1 == numberOfRewards)
            {
                hasFreeRewards = false;

                OnHasNotFreeRewards?.Invoke(victoryFreeReward);

            }

            numberOfCollectedRewards++;
        }
    }

    public bool HasFreeRewards() 
    {
        return hasFreeRewards;
    }

    private int GenerateNumberOfRewards() 
    {
        return UnityEngine.Random.Range(numberOfRewardsMin, numberOfRewardsMax);
    }
    public int GetNumberOfRewards() 
    {
        return numberOfRewards;
    }
}
