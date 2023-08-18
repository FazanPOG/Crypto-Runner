using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bonusText;

    [SerializeField] private Color positiveTextBonusColor;
    [SerializeField] private Color negativeTextBonusColor;

    [SerializeField] private Arch arch;
    [SerializeField] private Transform bonusCenter;

    [SerializeField] private GameObject[] positiveBonusDevices;
    [SerializeField] private GameObject[] negativeBonusDevices;

    private const string SYMBOL_PLUS = "+";
    private const string SYMBOL_MINUS = "-";
    private const string SYMBOL_MULTIPLY = "X";


    private void Start()
    {
        Arch.OnPlayerTakedBonus += Arch_OnPlayerTakedBonus;

        UpdateVisual();
    }

    private void Arch_OnPlayerTakedBonus(Arch obj)
    {
        obj.bonusCenter.gameObject.SetActive(false);
    }

    private void UpdateVisual() 
    {
        Bonus.Symbols randomSymbol = arch.symbols;
        int randomNumber = arch.randomNumber;

        switch (randomSymbol)
        {
            case Bonus.Symbols.Plus:
                Instantiate(positiveBonusDevices[Random.Range(0, positiveBonusDevices.Length)], bonusCenter);

                bonusText.color = positiveTextBonusColor;
                bonusText.text = SYMBOL_PLUS + randomNumber.ToString();
                break;

            case Bonus.Symbols.Minus:
                Instantiate(negativeBonusDevices[Random.Range(0, negativeBonusDevices.Length)], bonusCenter);

                bonusText.color = negativeTextBonusColor;
                bonusText.text = SYMBOL_MINUS + randomNumber.ToString();
                break;

            case Bonus.Symbols.Multiply:
                Instantiate(positiveBonusDevices[Random.Range(0, positiveBonusDevices.Length)], bonusCenter);

                bonusText.color = positiveTextBonusColor;
                bonusText.text = SYMBOL_MULTIPLY + randomNumber.ToString();
                break;

        }
    }
}
