using UnityEngine;
using UnityEngine.UI;

public class CurrentHatUI : MonoBehaviour
{
    [SerializeField] private Image currentHatIcon;
    [SerializeField] private HatsUI hatsUI;

    private void Start()
    {
        hatsUI.OnCurrentHatChange += HatsUI_OnCurrentHatChange;
    }

    private void HatsUI_OnCurrentHatChange(Hat hat)
    {
        currentHatIcon.sprite = hat.GetHatIcon().sprite;
    }
}
