using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState 
    {
        WaitingToStartGame,
        PlayingGame,
        Pause,
        GameOver,
        Victory,
    }

    [SerializeField] private EventSystem eventSystem;

    public static GameManager Instance { get; private set; }

    private GameState gameState;
    private Player.PlayerState playerState;
    private int currentLevel = 1;
    private bool isMobilePlatform;
    private bool isPC;

    public event Action OnGameStateChanged;

    [DllImport("__Internal")]
    private static extern bool IsDesktopDeviceInfo();

    private void Awake()
    {
        isPC = IsDesktopDeviceInfo();

        if (isPC == false)
        {
            isMobilePlatform = true;
        }
        else
        {
            isMobilePlatform = false;
        }

        Instance = this;

        gameState = GameState.WaitingToStartGame;
    }

    private void Start()
    {
        Player.Instance.OnPlayerStateChanged += Player_OnPlayerStateChanged;
        MarketButtonUI.OnMarketOpened += MarketButtonUI_OnMarketOpened;
        MarketUI.OnMarketClosed += MarketUI_OnMarketClosed;
        TotalRewardUI.OnTotalRewardCollected += TotalRewardUI_OnTotalRewardCollected;
    }

    private void TotalRewardUI_OnTotalRewardCollected(int obj)
    {
        gameState = GameState.Victory;
        OnGameStateChanged?.Invoke();
    }

    private void MarketUI_OnMarketClosed()
    {
        gameState = GameState.WaitingToStartGame;
        OnGameStateChanged?.Invoke();
    }

    private void MarketButtonUI_OnMarketOpened()
    {
        gameState = GameState.Pause;
        OnGameStateChanged?.Invoke();
    }

    private void Player_OnPlayerStateChanged()
    {
        playerState = Player.Instance.GetPlayerState();
    }

    private void OnDestroy()
    {
        Player.Instance.OnPlayerStateChanged -= Player_OnPlayerStateChanged;
        MarketButtonUI.OnMarketOpened -= MarketButtonUI_OnMarketOpened;
        MarketUI.OnMarketClosed -= MarketUI_OnMarketClosed;
        TotalRewardUI.OnTotalRewardCollected -= TotalRewardUI_OnTotalRewardCollected;
    }


    private void Update()
    {
        switch (gameState) 
        {
            case GameState.WaitingToStartGame:

                if (isMobilePlatform)
                {
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began && !eventSystem.IsPointerOverGameObject(touch.fingerId))
                        {
                            gameState = GameState.PlayingGame;
                            OnGameStateChanged?.Invoke();
                        }
                    }
                }

                if (!isMobilePlatform)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) && !eventSystem.IsPointerOverGameObject())
                    {
                        gameState = GameState.PlayingGame;
                        OnGameStateChanged?.Invoke();
                    }
                }
                
                break;

            case GameState.PlayingGame:
                Time.timeScale = 1f;
                if (playerState == Player.PlayerState.Death) 
                {
                    gameState = GameState.GameOver;
                    OnGameStateChanged?.Invoke();
                }
                break;

            case GameState.Pause:
                break;

            case GameState.GameOver:
                StartCoroutine(DeathDelay());
                break;

            case GameState.Victory:
                currentLevel++;
                SceneManager.LoadScene(0, LoadSceneMode.Single);
                gameState = GameState.WaitingToStartGame;
                OnGameStateChanged?.Invoke();
                break;
        }
    }

    IEnumerator DeathDelay() 
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public int GetCurrentLevel() 
    {
        return currentLevel;
    }

    public bool IsGamePause()
    {
        return gameState == GameState.Pause;
    }

    public bool IsGameWaitingToStart()
    {
        return gameState == GameState.WaitingToStartGame;
    }

    public bool IsGamePlaying() 
    {
        return gameState == GameState.PlayingGame;
    }

    public GameState GetGameState() 
    {
        return gameState;
    }

    public bool IsPC() 
    {
        return isPC;
    }
}
