using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private SoundButtonUI soundButtonUI;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HackerLevelManager.Instance.OnArchBonus += HackerLevelManager_OnArchBonus;
        HackerLevelManager.Instance.OnArchBonusBad += HackerLevelManager_OnArchBonusBad;
    }


    private void HackerLevelManager_OnArchBonusBad(Arch arch)
    {
        PlaySound(audioClipRefsSO.bonusArchBad, arch.transform.position);
    }

    private void HackerLevelManager_OnArchBonus(Arch arch)
    {
        PlaySound(audioClipRefsSO.bonusArch, arch.transform.position);
    }

    public void PlayVictorySound(Vector3 playerPosition) 
    {
        PlaySound(audioClipRefsSO.victory, playerPosition);
    }
    public void PlayHitSound(Vector3 policemanPosition) 
    {
        PlaySound(audioClipRefsSO.hit, policemanPosition);
    }

    public void PlayBitcoinSound(Vector3 bitcoinPosition) 
    {
        PlaySound(audioClipRefsSO.bitcoin, bitcoinPosition);
    }
    public void PlayButtonUISound() 
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        PlaySound(audioClipRefsSO.buttonUI, cameraPosition, 0.4f);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
