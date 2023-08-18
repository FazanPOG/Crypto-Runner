using UnityEngine;
using UnityEngine.UI;

public class SoundButtonUI : MonoBehaviour
{
    [SerializeField] private Button soundButton;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Sprite soundOnSprite;

    private bool soundToggle = true;


    private void Start()
    {
        soundButton.onClick.AddListener(() => 
        {
            if (soundToggle == true) 
            {
                soundButton.image.sprite = soundOffSprite;
                AudioListener.volume = 0f;
                soundToggle = false;
            }
            else 
            {
                soundButton.image.sprite = soundOnSprite;
                AudioListener.volume = 1f;
                soundToggle = true;
            }

        });
    }
}
