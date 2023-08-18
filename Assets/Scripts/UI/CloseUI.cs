using System;
using UnityEngine;
using UnityEngine.UI;

public class CloseUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    public static event Action OnMarketClose;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(() => 
        {
            OnMarketClose?.Invoke();
        });
    }

}
