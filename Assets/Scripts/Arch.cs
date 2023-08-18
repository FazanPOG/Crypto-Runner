using System;
using UnityEngine;

public class Arch : MonoBehaviour
{

    [SerializeField] private Bonus bonus;

    public Transform bonusCenter;

    public Bonus.Symbols symbols { get; private set; }
    public int randomNumber { get; private set; }

    public static event Action<Arch> OnPlayerTakedBonus;

    private void Awake()
    {
        SetBonus();
    }


    private void OnTriggerEnter(Collider other)
    {
        OnPlayerTakedBonus?.Invoke(this);
    }

    private void SetBonus()
    {

        bonus.GenerateBonus(out Bonus.Symbols symbolsTemp, out int randomNumberTemp);

        symbols = symbolsTemp;
        randomNumber = randomNumberTemp;
    }
}
