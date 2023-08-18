using TMPro;
using UnityEngine;

public class BitcoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bitcoinCounterText;


    private void Start()
    {
        BitcoinManager.Instance.OnBitcoinCounterChangedValue += BitcoinCounter_OnBitcoinCounterChangedValue;
        UpdateVisual();
    }

    private void BitcoinCounter_OnBitcoinCounterChangedValue()
    {
        UpdateVisual();
    }

    private void OnDestroy()
    {
        BitcoinManager.Instance.OnBitcoinCounterChangedValue -= BitcoinCounter_OnBitcoinCounterChangedValue;
    }

    private void UpdateVisual()
    {
        bitcoinCounterText.text = BitcoinManager.Instance.GetBitcoinCounter().ToString();
    }
}
