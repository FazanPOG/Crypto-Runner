using TMPro;
using UnityEngine;

public class PolicemanLevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI policemanLevelText;

    private int randomPolicemanLevel;
    private int playerLevel;

    private void Awake()
    {
        randomPolicemanLevel = Random.Range(10, 300);
        randomPolicemanLevel /= 10;
        randomPolicemanLevel *= 10;
    }
    private void Start()
    {
        policemanLevelText.text = "сп: " + randomPolicemanLevel;

        HackerLevelManager.Instance.OnHackerLevelChanged += HackerLevelManager_OnHackerLevelChanged;
    }

    private void HackerLevelManager_OnHackerLevelChanged()
    {
        playerLevel = HackerLevelManager.Instance.GetHackerLevel();

        if (playerLevel >= randomPolicemanLevel) 
        {
            policemanLevelText.color = Color.green;
        }

        if (playerLevel < randomPolicemanLevel)
        {
            policemanLevelText.color = Color.red;
        }
    }


    public int GetPolicemanLevel() 
    {
        return randomPolicemanLevel;
    }
}
