using UnityEngine;
using UnityEngine.UI;

public class ManagerPanelManager : MonoBehaviour
{
    public ManagersSO managerSO;
    public ShopManager shopManager;

    public Image managersIconImage;
    public Text managersDescriptionText;
    public Button buyButton;

    public void Start ()
    {
        managersIconImage.sprite = managerSO.managersIcon;
        managersDescriptionText.text = $"{managerSO.managersName} " +
            $"\nАвтоматизирует продажу {managerSO.managersName} " +
            $"\n{managerSO.managersCost} $";

        RedrawThePanel();
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
}
