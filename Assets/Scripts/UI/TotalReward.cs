using UnityEngine;

public class TotalReward : MonoBehaviour
{

    private int allRewardsValue;

    private void Awake()
    {
        VictoryBonusUI.OnRewardCollect += VictoryBonusUI_OnRewardCollect;
    }

    private void OnDestroy()
    {
        VictoryBonusUI.OnRewardCollect -= VictoryBonusUI_OnRewardCollect;
    }

    private void VictoryBonusUI_OnRewardCollect(VictoryRewardUI victoryRewardUI)
    {
        allRewardsValue += victoryRewardUI.GetRewardValue();
    }

    public int GetAllRewardsValue() 
    {
        return allRewardsValue;
    }
    public int GetAllRewardValueWithVideoMultiplier() 
    {
        return allRewardsValue * 3;
    }
}
