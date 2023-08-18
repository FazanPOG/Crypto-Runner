using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VictoryBonusUIVisual : MonoBehaviour
{
    [SerializeField] private Button bonusTakeButton;
    [SerializeField] private Button bonusWatchVideoButton;

    private Player.PlayerState playerState;
    private Animation mAnimation;

    private void Start()
    {
        mAnimation = GetComponent<Animation>();

        bonusTakeButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonUISound();

            Hide();
        });

        bonusWatchVideoButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonUISound();

            Hide();
        });

        Player.Instance.OnPlayerStateChanged += Player_OnPlayerStateChanged;

        Hide();
    }

    private void Player_OnPlayerStateChanged()
    {
        playerState = Player.Instance.GetPlayerState();

        if (playerState == Player.PlayerState.Victory) 
        {
            gameObject.transform.localScale = new Vector3(0f ,0f ,0f);
            Show();
            StartCoroutine(AnimationDelay());
        }
        
    }

    IEnumerator AnimationDelay() 
    {
        yield return new WaitForSeconds(2);
        mAnimation.Play();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
