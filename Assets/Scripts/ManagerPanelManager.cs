using UnityEngine;
using UnityEngine.UI;

public class ManagerPanelManager : MonoBehaviour
{
    public ManagersSO managerSO;
    public ShopManager shopManager;

    public Image managersIconImage;
    public Text managersDescriptionText;
    public Button buyButton;

    private int _managerState;

    public int managerState
    {
        get
        {
            return (_managerState);
        }
        set
        {
            _managerState = value;
            PlayerPrefs.SetInt($"{managerSO.managersName}.Manager", managerState);
            ActivateManager();
            //_UpdateManagersStateArray(this);
        }
    }

    public void Start ()
    {
        managersIconImage.sprite = managerSO.managersIcon;
        managersDescriptionText.text = $"{managerSO.managersName} " +
            $"\nАвтоматизирует продажу {managerSO.managersName} " +
            $"\n{managerSO.managersCost} $";

        managerState = PlayerPrefs.GetInt($"{managerSO.managersName}.Manager");
        BuyManager();
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
        if (managerState == 0)
        {
            if(shopManager.coins >= managerSO.managersCost)
            {
                shopManager.coins -= managerSO.managersCost;
                managerState = 1;
                ActivateManager();
            }
        }
    }
    public void ActivateManager ()
    {
        if (managerState == 1)
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
            shopManager.productPanelsArray[i].manager = managerState;
        }
    }
}
