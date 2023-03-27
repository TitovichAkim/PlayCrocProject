using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text numbersOfCoinsFloatText;
    public Text numbersOfCoinsstringText;
    public Text upgradesNumberText;

    public ProductPanelManager[] productPanelsArray;
    public ManagerPanelManager[] managerPanelsArray;
    public int[] managersStates;
    private int[] _upgradeNumbers = {1, 10, 25, 100};
    private int _upgradeIndex = 0;

    private float _coins;


    public float coins
    {
        get
        {
            return (_coins);
        }
        set
        {
            _coins = value;
            NumberFormatter.FormatAndRedraw(_coins, numbersOfCoinsFloatText, numbersOfCoinsstringText);
            PlayerPrefs.SetFloat("Coin", _coins);
            _RedrawUpgradeButtons();
            _DeactivatingTheManagersPanel();
        }
    }

    public void Start ()
    {
        coins = PlayerPrefs.GetFloat("Coin");
        _RedrawUpgradeButtons();
    }

    public void UpgradeIndexUp ()
    {
        _upgradeIndex++;
        if (_upgradeIndex > 3)
        {
            _upgradeIndex = 0;
        }
        upgradesNumberText.text = $"x {_upgradeNumbers[_upgradeIndex]}";
        foreach(ProductPanelManager prodMan in productPanelsArray)
        {
            prodMan.upgradesNumber = _upgradeNumbers[_upgradeIndex];
        }
    }
    private void _RedrawUpgradeButtons ()
    {
        foreach(ProductPanelManager prodMan in productPanelsArray)
        {
            prodMan.RedrawUpgradeButton();
        }
        foreach(ManagerPanelManager managerPanel in managerPanelsArray)
        {
            managerPanel.ActivateManager();
        }
    }

    private void _DeactivatingTheManagersPanel ()
    {
        for(int i = 0; i < managerPanelsArray.Length; i++)
        {
            if (managersStates[i] == 1)
            {
                managerPanelsArray[i].gameObject.SetActive(false);
            }
        }
    }
}
