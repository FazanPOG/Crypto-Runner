using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HackerLevelUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI hackerLevelText;


    private void Start()
    {
        HackerLevelManager.Instance.OnHackerLevelChanged += HackerLevelManager_OnHackerLevelChanged;
    }

    private void HackerLevelManager_OnHackerLevelChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        int hackerLevel = HackerLevelManager.Instance.GetHackerLevel();
        hackerLevelText.text = "сп: " + hackerLevel;
    }
}
