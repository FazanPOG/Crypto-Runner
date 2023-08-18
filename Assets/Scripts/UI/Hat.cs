using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hat : MonoBehaviour
{
    [SerializeField] private HatsSO hatsSO;

    [SerializeField] private Hat defaultHat;

    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Image selectedImage;

    private bool isOpened = false;
    private Image hatIcon;
    private TextMeshProUGUI costText;
    private HatsUI hatsUI;

    public static event Action<Hat> OnTriedBuyHat;
    public static event Action<Hat> OnSelectedHat;

    private void Awake()
    {
        hatsUI = GetComponentInParent<HatsUI>();
        hatIcon = GetComponentInChildren<Image>();
        costText = buyButton.GetComponentInChildren<TextMeshProUGUI>();

        hatIcon.color = Color.black;

        costText.fontSize = 28;
        costText.text = hatsSO.cost.ToString();

        statsText.text = "+" + hatsSO.bonusLevel.ToString() + " ”–";

        if (ProgressManager.LoadIsHatOpened(GetHatName()) == 1) 
        {
            isOpened = true;
        }
    }

    private void Start()
    {
        SetDefaultHat();

        hatsUI.OnCurrentHatChange += HatsUI_OnCurrentHatChange;
        BitcoinManager.Instance.OnBoughtHat += BitcoinManager_OnBoughtHat;

        buyButton.onClick.AddListener(() => 
        {
            SoundManager.Instance.PlayButtonUISound();

            OnTriedBuyHat?.Invoke(this);
        });
        selectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonUISound();

            OnSelectedHat?.Invoke(this);
        });

        if (isOpened == true) 
        {
            hatIcon.color = Color.white;
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            selectedImage.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        hatsUI.OnCurrentHatChange -= HatsUI_OnCurrentHatChange;
        BitcoinManager.Instance.OnBoughtHat -= BitcoinManager_OnBoughtHat;
    }

    private void BitcoinManager_OnBoughtHat(Hat hat)
    {
        hat.isOpened = true;

        ProgressManager.SaveIsHatOpened(hat.GetHatName(), 1);
        //сохран€ю, что шл€па куплена

        hat.hatIcon.color = Color.white;
        hat.buyButton.gameObject.SetActive(false);
        hat.selectButton.gameObject.SetActive(false);
        hat.selectedImage.gameObject.SetActive(true);
    }

    private void HatsUI_OnCurrentHatChange(Hat currentHat)
    {
        Hat lastHat = HatsUI.lastHat;

        currentHat.buyButton.gameObject.SetActive(false);
        currentHat.selectButton.gameObject.SetActive(false);
        currentHat.selectedImage.gameObject.SetActive(true);

        lastHat.buyButton.gameObject.SetActive(false);
        lastHat.selectButton.gameObject.SetActive(true);
        lastHat.selectedImage.gameObject.SetActive(false);
    }


    public string GetHatName() 
    {
        return hatsSO.hatName;
    }
    public bool IsOpened() 
    {
        return isOpened;
    }
    public int GetCost() 
    {
        return hatsSO.cost;
    }

    public Image GetHatIcon() 
    {
        return hatIcon;
    }

    public int GetBonusLevel() 
    {
        return hatsSO.bonusLevel;
    }

    public GameObject GetHatPrefab() 
    {
        return hatsSO.prefab;
    }

    private void SetDefaultHat() 
    {
        defaultHat.buyButton.gameObject.SetActive(false);
        defaultHat.selectButton.gameObject.SetActive(false);
        defaultHat.selectedImage.gameObject.SetActive(true);
        defaultHat.hatIcon.color = Color.white;
    }

}
