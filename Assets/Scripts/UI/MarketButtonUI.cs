using System;
using UnityEngine;
using UnityEngine.UI;

public class MarketButtonUI : MonoBehaviour
{

    [SerializeField] private Button marketButton;
    [SerializeField] private GameObject marketUI;

    public static event Action OnMarketOpened;

    private void Start()
    {
        marketButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonUISound();

            marketUI.SetActive(true);
            OnMarketOpened?.Invoke();

            gameObject.SetActive(false);
        });

        MarketUI.OnMarketClosed += MarketUI_OnMarketClosed;
    }

    private void MarketUI_OnMarketClosed()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        MarketUI.OnMarketClosed -= MarketUI_OnMarketClosed;
    }
}
