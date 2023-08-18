using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    public enum PlayerState
    {
        Idle,
        Running,
        Fighting,
        Death,
        Victory,
    }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveSpeedStraight = 5f;
    [SerializeField] private Transform playerVictorySpawnPoint;


    private float fightingTimer;
    private float fightingTimerMax = 1f;

    private GameManager.GameState gameState;
    private PlayerState playerState;
    private bool canMove = false;
    private bool isPC;


    private Vector2 previousTouchPosition;


    public static Player Instance { get; private set; }

    public event Action OnPlayerStateChanged;

    private void Awake()
    {
        playerState = PlayerState.Idle;

        Instance = this;
    }

    private void Start()
    {
        isPC = GameManager.Instance.IsPC();

        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        Policeman.OnPolicemanTriggered += Policeman_OnPolicemanTriggered;
        Finish.OnFinish += Finish_OnFinish;
        Portal.OnPortalTriggered += Portal_OnPortalTriggered;
        Victory.OnVictory += Victory_OnVictory;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= GameManager_OnGameStateChanged;
        Policeman.OnPolicemanTriggered -= Policeman_OnPolicemanTriggered;
        Finish.OnFinish -= Finish_OnFinish;
        Portal.OnPortalTriggered -= Portal_OnPortalTriggered;
        Victory.OnVictory -= Victory_OnVictory;
    }

    private void Victory_OnVictory()
    {
        playerState = PlayerState.Victory;
        SoundManager.Instance.PlayVictorySound(transform.position);

        OnPlayerStateChanged?.Invoke();
    }

    private void Portal_OnPortalTriggered()
    {
        transform.position = playerVictorySpawnPoint.position;
        moveSpeed = 25;
        moveSpeedStraight = 5;
    }

    private void Finish_OnFinish(Finish obj)
    {
        moveSpeed = 14f;
        moveSpeedStraight = 4f;
    }

    private void Policeman_OnPolicemanTriggered(Policeman policeman)
    {
        int policemanLevel = policeman.GetPolicemanLevel();
        int playerLevel = HackerLevelManager.Instance.GetHackerLevel();

        if (playerLevel >= policemanLevel)
        {
            playerState = PlayerState.Fighting;

            OnPlayerStateChanged?.Invoke();
        }

        if (playerLevel < policemanLevel)
        {
            playerState = PlayerState.Death;

            OnPlayerStateChanged?.Invoke();
        }
    }


    private void GameManager_OnGameStateChanged()
    {
        gameState = GameManager.Instance.GetGameState();

        switch (gameState) 
        {
            case GameManager.GameState.WaitingToStartGame:
                canMove = false;
                break;

            case GameManager.GameState.PlayingGame:
                playerState = PlayerState.Running;

                OnPlayerStateChanged?.Invoke();

                canMove = true;
                break;

            case GameManager.GameState.Pause:
                canMove = false;
                break;

            case GameManager.GameState.GameOver:
                canMove = false;
                break;

        }
    }

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        Movement();
        switch (playerState)
        {
            case PlayerState.Idle:
                canMove = false;
                break;

            case PlayerState.Running:
                canMove = true;
                if (transform.position.y < -1f) 
                {
                    playerState = PlayerState.Death;
                    OnPlayerStateChanged?.Invoke();
                }
                break;

            case PlayerState.Fighting:
                canMove = false;
                fightingTimer += Time.deltaTime;
                if (fightingTimer > fightingTimerMax) 
                {
                    playerState = PlayerState.Running;
                    OnPlayerStateChanged?.Invoke();

                    fightingTimer = 0f;
                }
                break;

            case PlayerState.Death:
                Debug.Log("Player.Player Died");
                canMove = false;
                break;

            case PlayerState.Victory:
                canMove = false;
                break;
        }
    }

    private void Movement() 
    {
        if (canMove) 
        {
            transform.position += Vector3.left * moveSpeedStraight * Time.deltaTime;
            if (isPC == true) 
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    float mouseDir = Input.GetAxisRaw("Mouse X");
                    Vector3 moveDir = new Vector3(0f, 0f, mouseDir);
                    transform.position += moveDir * Time.deltaTime * moveSpeed;
                }
            }

            if (isPC == false) 
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Moved)
                    {
                        Vector2 deltaPosNormalized = touch.deltaPosition.normalized;
                        Vector3 moveDir = new Vector3(0f, 0f, -deltaPosNormalized.x);

                        moveSpeed = 6;
                        transform.position += moveDir * Time.deltaTime * moveSpeed;
                    }
                }
            }
            
        }
    }


    public PlayerState GetPlayerState() 
    {
        return playerState;
    }
}
