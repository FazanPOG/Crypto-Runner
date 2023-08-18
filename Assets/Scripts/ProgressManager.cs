using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    private const string BITCOIN_COUNTER_KEY = "BitcoinCounter";
    private const string LEVEL_KEY = "Level";

    //BitcoinCounter
    public static void SaveBitcoinCounter(int bitcoinCounter) 
    {
        PlayerPrefs.SetInt(BITCOIN_COUNTER_KEY, bitcoinCounter);
        PlayerPrefs.Save();
    }
    public static int LoadBitcoinCounter()
    {
        return PlayerPrefs.GetInt(BITCOIN_COUNTER_KEY, 0);
    }

    //Level
    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(LEVEL_KEY, level);
        PlayerPrefs.Save();
    }
    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt(LEVEL_KEY, 1);
    }

    //Hats
    public static void SaveIsHatOpened(string hatName, int isHatOpened)
    {
        PlayerPrefs.SetInt(hatName, isHatOpened);
        PlayerPrefs.Save();
    }
    public static int LoadIsHatOpened(string hatName)
    {
        return PlayerPrefs.GetInt(hatName, 0);
    }
}
