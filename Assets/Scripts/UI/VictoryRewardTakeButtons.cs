using UnityEngine;
using UnityEngine.UI;

public class VictoryRewardTakeButtons : MonoBehaviour
{
    [SerializeField] private Button takeButton;
    [SerializeField] private Button watchVideoButton;

    private void Awake()
    {
        if (GetRandomSwapOrNOt() > 0) 
        {
            Vector3 temp = takeButton.gameObject.transform.localPosition;
            takeButton.gameObject.transform.localPosition = watchVideoButton.gameObject.transform.localPosition;
            watchVideoButton.gameObject.transform.localPosition = temp;
        }
    }

    private int GetRandomSwapOrNOt() 
    {
        return Random.Range(0, 2);
    }
}
