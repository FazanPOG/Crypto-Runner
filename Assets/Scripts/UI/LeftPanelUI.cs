using UnityEngine;

public class LeftPanelUI : MonoBehaviour
{

    private void Start()
    {
        Show();

        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged()
    {
        GameManager.GameState gameState = GameManager.Instance.GetGameState();

        if (gameState != GameManager.GameState.WaitingToStartGame && gameState != GameManager.GameState.Pause) 
        {
            Hide();
        }
        else 
        {
            Show();
        }
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
