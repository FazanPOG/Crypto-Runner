using UnityEngine;

public class PlayerHatVisual : MonoBehaviour
{
    [SerializeField] private HatsUI hatsUI;
    [SerializeField] private Transform playerHead;

    private GameObject lastHat;

    private void Awake()
    {
        lastHat = null;
    }

    private void Start()
    {
        hatsUI.OnCurrentHatChange += HatsUI_OnCurrentHatChange;
    }

    private void HatsUI_OnCurrentHatChange(Hat hat)
    {
        if(lastHat == null) 
        {
            lastHat = Instantiate(hat.GetHatPrefab(), playerHead);
        }
        else 
        {
            Destroy(lastHat);
            lastHat = Instantiate(hat.GetHatPrefab(), playerHead);
        }
    }
}
