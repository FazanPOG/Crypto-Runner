using System;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour
{

    [SerializeField] private Button closeButton;

    public static event Action OnMarketClosed;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {

        closeButton.onClick.AddListener(() => 
        {
            OnMarketClosed?.Invoke();
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
