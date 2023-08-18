using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryRewardUI : MonoBehaviour
{
    [SerializeField] private Image cameraIcon;
    [SerializeField] private GameObject victoryReward;
    [SerializeField] private VictoryBonusUI victoryBonusUI;

    private TextMeshProUGUI victoryRewardText;
    private Button victoryRewardUIButton;

    private bool isCollected = false;
    private int rewardValue;
    private int maxRewardValue = 100;

    public bool isFreeReward = true;

    public static event Action<VictoryRewardUI> OnFreeRewardTaked;
    public static event Action<VictoryRewardUI> OnVideoRewardTaked;

    private void Awake()
    {
        victoryRewardUIButton = GetComponentInChildren<Button>();
        victoryRewardText = victoryReward.GetComponentInChildren<TextMeshProUGUI>();

        rewardValue = GenerateReward();
    }

    private void Start()
    {
        victoryRewardUIButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonUISound();

            if (victoryBonusUI.HasFreeRewards()) 
            {
                OnFreeRewardTaked?.Invoke(this);
            }
            else 
            {
                OnVideoRewardTaked?.Invoke(this);
            }
        });

        VictoryBonusUI.OnRewardCollect += VictoryBonusUI_OnRewardCollect;
        VictoryBonusUI.OnHasNotFreeRewards += VictoryBonusUI_OnHasNotFreeRewards;
    }

    private void OnDestroy()
    {
        VictoryBonusUI.OnRewardCollect -= VictoryBonusUI_OnRewardCollect;
        VictoryBonusUI.OnHasNotFreeRewards -= VictoryBonusUI_OnHasNotFreeRewards;
    }

    private void VictoryBonusUI_OnHasNotFreeRewards(VictoryRewardUI victoryRewardUI)
    {
        if (!isCollected) 
        {
            cameraIcon.gameObject.SetActive(true);
        }
    }

    private void VictoryBonusUI_OnRewardCollect(VictoryRewardUI victoryRewardUI)
    {
        victoryRewardUI.isCollected = true;
        victoryRewardUI.cameraIcon.gameObject.SetActive(false);
        victoryRewardUI.victoryReward.SetActive(true);
        victoryRewardUI.victoryRewardText.text = victoryRewardUI.rewardValue.ToString();
        victoryRewardUI.victoryRewardUIButton.gameObject.SetActive(false);
    }

    public int GetRewardValue() 
    {
        return rewardValue;
    }

    private int GenerateReward() 
    {
        int value = UnityEngine.Random.Range(10, maxRewardValue);
        value /= 10;
        value *= 10;
        return value;
    }
}
