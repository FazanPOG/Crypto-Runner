using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    private void Awake()
    {
        Show();
    }

    private void Start()
    {
        MarketButtonUI.OnMarketOpened += MarketButtonUI_OnMarketOpened;
        MarketUI.OnMarketClosed += MarketUI_OnMarketClosed;
        Player.Instance.OnPlayerStateChanged += Player_OnPlayerStateChanged;
    }

    private void OnDestroy()
    {
        MarketButtonUI.OnMarketOpened -= MarketButtonUI_OnMarketOpened;
        MarketUI.OnMarketClosed -= MarketUI_OnMarketClosed;
    }

    private void MarketUI_OnMarketClosed()
    {
        Show();
    }

    private void MarketButtonUI_OnMarketOpened()
    {
        Hide();
    }

    private void Player_OnPlayerStateChanged()
    {
        Player.PlayerState playerState = Player.Instance.GetPlayerState();
        if (playerState == Player.PlayerState.Running) 
        {
            Hide();
        }
    }

    private void Show() 
    {
        gameObject.SetActive(true);
    }

    private void Hide() 
    {
        gameObject.SetActive(false);
    }
}
