using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TotalRewardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalRewardText;
    [SerializeField] private Button bonusTakeButton;
    [SerializeField] private Button bonusWatchVideoButton;
    [SerializeField] private Button takeTotalRewardButton;
    [SerializeField] private Button watchVideoTotalRewardButton;
    [SerializeField] private TotalReward totalReward;

    private bool isWatchVideo = false;

    public static event Action<int> OnTotalRewardCollected;


    private void Start()
    {

        bonusTakeButton.onClick.AddListener(() => 
        {
            Show();

            totalRewardText.text = totalReward.GetAllRewardsValue().ToString();
        });

        bonusWatchVideoButton.onClick.AddListener(() =>
        {
            //WATCH VIDEO
            YandexGame.RewVideoShow(0);
            Debug.Log("Watching Video....");

            isWatchVideo = true;

            totalRewardText.text = totalReward.GetAllRewardValueWithVideoMultiplier().ToString();
            takeTotalRewardButton.GetComponentInChildren<TextMeshProUGUI>().text = "Забрать";
            HideWatchButton();

            Show();
        });

        takeTotalRewardButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonUISound();

            if (isWatchVideo == true) 
            {
                OnTotalRewardCollected?.Invoke(totalReward.GetAllRewardValueWithVideoMultiplier());
            }
            else 
            {
                OnTotalRewardCollected?.Invoke(totalReward.GetAllRewardsValue());
            }
        });

        watchVideoTotalRewardButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonUISound();

            YandexGame.RewVideoShow(0);
            Debug.Log("Watching Video....");

            isWatchVideo = true;
            totalRewardText.text = totalReward.GetAllRewardValueWithVideoMultiplier().ToString();
            takeTotalRewardButton.GetComponentInChildren<TextMeshProUGUI>().text = "Забрать";
            HideWatchButton();
            //NEXT LEVEL
        });


        Hide();
    }

    private void HideWatchButton() 
    {
        watchVideoTotalRewardButton.gameObject.SetActive(false);
        takeTotalRewardButton.gameObject.transform.localPosition = new Vector3(0, takeTotalRewardButton.gameObject.transform.localPosition.y, takeTotalRewardButton.gameObject.transform.localPosition.z);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
