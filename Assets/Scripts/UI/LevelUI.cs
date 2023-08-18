using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private int level;

    private void Awake()
    {
        level = ProgressManager.LoadLevel();

        levelText.text = "спнбемэ " + level.ToString();
    }

    private void Start()
    {
        Victory.OnVictory += Victory_OnVictory;
    }

    private void Victory_OnVictory()
    {
        level++;
        ProgressManager.SaveLevel(level);
    }

    private void OnDestroy()
    {
        Victory.OnVictory -= Victory_OnVictory;
    }
}
