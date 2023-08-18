using System;
using UnityEngine;

public class HackerLevelManager : MonoBehaviour
{
    [SerializeField] private Policeman policeman;
    [SerializeField] private HatsUI hatsUI;

    private int hackerLevel;

    public static HackerLevelManager Instance { get; private set; }


    public event Action OnHackerLevelChanged;
    public event Action<Arch> OnArchBonus;
    public event Action<Arch> OnArchBonusBad;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        Bitcoin.OnBitcoinCollect += Bitcoin_OnBitcoinCollect;
        Arch.OnPlayerTakedBonus += Arch_OnPlayerTakedBonus;
        Policeman.OnPolicemanTriggered += Policeman_OnPolicemanTriggered;
        hatsUI.OnCurrentHatChange += HatsUI_OnCurrentHatChange;
    }

    private void HatsUI_OnCurrentHatChange(Hat hat)
    {
        hackerLevel = hat.GetBonusLevel();

        OnHackerLevelChanged?.Invoke();
    }

    private void OnDestroy()
    {
        Bitcoin.OnBitcoinCollect -= Bitcoin_OnBitcoinCollect;
        Arch.OnPlayerTakedBonus -= Arch_OnPlayerTakedBonus;
        Policeman.OnPolicemanTriggered -= Policeman_OnPolicemanTriggered;
    }



    private void Policeman_OnPolicemanTriggered(Policeman policeman)
    {
        int playerLevel = HackerLevelManager.Instance.GetHackerLevel();
        int policemanLevel = policeman.GetPolicemanLevel();

        if (playerLevel >= policemanLevel) 
        {
            hackerLevel += policeman.GetPolicemanLevel();
            OnHackerLevelChanged?.Invoke();
        }

    }


    private void Arch_OnPlayerTakedBonus(Arch arch)
    {
        Bonus.Symbols randomSymbols = arch.symbols;
        int randomNumber = arch.randomNumber;
        if (hackerLevel >= 0)
        {
            switch (randomSymbols)
            {
                case (Bonus.Symbols.Plus):
                    OnArchBonus?.Invoke(arch);
                    hackerLevel += randomNumber;
                    break;

                case (Bonus.Symbols.Minus):
                    OnArchBonusBad?.Invoke(arch);
                    if (hackerLevel <= randomNumber)
                    {
                        hackerLevel = 0;
                    }
                    else
                    {
                        hackerLevel -= randomNumber;
                    }
                    break;

                case (Bonus.Symbols.Multiply):
                    OnArchBonus?.Invoke(arch);
                    hackerLevel *= randomNumber;
                    break;
            }
        }

        OnHackerLevelChanged?.Invoke();
    }

   

    private void Bitcoin_OnBitcoinCollect(Bitcoin obj)
    {
        hackerLevel++;
        OnHackerLevelChanged?.Invoke();
    }



    public int GetHackerLevel() 
    {
        return hackerLevel;
    }
}
