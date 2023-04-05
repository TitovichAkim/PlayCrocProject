using UnityEngine;
using UnityEngine.UI;

public class ManagerPanelManager : MonoBehaviour
{
    public ManagersSO managerSO;
    public ShopManager shopManager;

    public Image managersIconImage;
    public Text managerNameText;
    public Text managersDescriptionText;
    public Text managerCostText;
    public Button buyButton;

    private bool _managerState;

    public bool managerState
    {
        get
        {
            return (_managerState);
        }
        set
        {
            _managerState = value;
            ActivateManager();
        }
    }

    public void Start ()
    {
        LocalisationTexts();
        managersIconImage.sprite = managerSO.managersIcon;
        managersDescriptionText.text = $"{managerSO.managersName} " +
            $"\nАвтоматизирует продажу {managerSO.managersName}";
        NumberFormatter.FormatAndRedraw(managerSO.managersCost, managerCostText);
        
    }
    public void RedrawThePanel ()
    {
        buyButton.interactable = shopManager.coins >= managerSO.managersCost;
    }

    public void BuyManager ()
    {
        if (!managerState)
        {
            if(shopManager.coins >= managerSO.managersCost)
            {
                shopManager.coins -= managerSO.managersCost;
                managerState = true;
                shopManager.SaveManagersStates();
            }
        }
    }
    public void ActivateManager ()
    {
        if (managerState)
        {
            this.gameObject.SetActive(false);
            _UpdateManagersStateArray(this);
        }
    }

    private void _UpdateManagersStateArray (ManagerPanelManager panelManager)
    {
    for(int i = 0; i < shopManager.managerPanelsArray.Length; i++)
    {
        if (shopManager.managerPanelsArray[i] == panelManager)
        {
            shopManager.productPanelsArray[i].manager = true;
            shopManager.managersStatesArray[i] = managerState;
            shopManager.managersStatesArray = shopManager.managersStatesArray;
        }
        }
    }

    private void LocalisationTexts ()
    {
        Localizator.LocalizedText(managerNameText, $"ManagerName.{managerSO.managersName}");
    }
}
