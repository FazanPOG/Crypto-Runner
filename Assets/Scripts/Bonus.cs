using UnityEngine;

public class Bonus : MonoBehaviour
{

    public enum Symbols
    {
        Plus = 1,
        Minus,
        Multiply,
    }

    private int maxCalculateSymbols = 3;
    private int maxBonusNumberForPlus = 100;
    private int maxBonusNumberForMinus = 100;
    private int maxBonusNumberForMultiply = 5;


    public void GenerateBonus(out Symbols randomSymbolsOut, out int randomNumberOut) 
    {

        int randomSymbols = UnityEngine.Random.Range(1, maxCalculateSymbols + 1);
        randomSymbolsOut = (Symbols)randomSymbols;

        randomNumberOut = 0;

        switch (randomSymbolsOut)
        {
            case (Symbols.Plus):
                randomNumberOut = Random.Range(5, maxBonusNumberForPlus);
                break;

            case (Symbols.Minus):
                randomNumberOut = Random.Range(5, maxBonusNumberForMinus);
                break;

            case (Symbols.Multiply):
                randomNumberOut = Random.Range(2, maxBonusNumberForMultiply);
                break;

        }

    }
    
}
