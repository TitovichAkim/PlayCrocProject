using UnityEngine;
using UnityEngine.UI;

public class ManagerPanelManager : MonoBehaviour
{
    public ManagersSO managerSO;
    public ShopManager shopManager;

    public Image managersIconImage;
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
        managersIconImage.sprite = managerSO.managersIcon;
        managersDescriptionText.text = $"{managerSO.managersName} " +
            $"\n�������������� ������� {managerSO.managersName}";
        NumberFormatter.FormatAndRedraw(managerSO.managersCost, managerCostText);
    }
    public void RedrawThePanel ()
    {
        if(shopManager.coins >= managerSO.managersCost)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    public void BuyManager ()
    {
        if (!managerState)
        {
            if(shopManager.coins >= managerSO.managersCost)
            {
                shopManager.coins -= managerSO.managersCost;
                managerState = true;
                ActivateManager();
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
                shopManager.managersStatesArray[i] = managerState;
                //shopManager.productPanelsArray[i].manager = managerState;
                shopManager.managersStatesArray = shopManager.managersStatesArray;
            }
        }
    }
}
