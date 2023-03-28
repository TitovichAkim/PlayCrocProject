using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text numbersOfCoinsFloatText;
    public Text numbersOfCoinsstringText;
    public Text upgradesNumberText;

    public ProductPanelManager[] productPanelsArray;
    public ManagerPanelManager[] managerPanelsArray;

    [SerializeField]private bool[] _managersStatesArray;

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
        }
    }
    public bool[] managersStatesArray
    {
        get
        {
            return (_managersStatesArray);
        }
        set
        {
            _managersStatesArray = value;
            Debug.Log("„то-то случилось");
            _SaveManagersStates();

        }
    }

    public void Start ()
    {
        coins = PlayerPrefs.GetFloat("Coin");
        _managersStatesArray = new bool[managerPanelsArray.Length];
        _RedrawUpgradeButtons();
        _LoadManagersState();
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
            managerPanel.RedrawThePanel();
        }
    }

    private void _SaveManagersStates ()
    {
        for(int i = 0; i < managersStatesArray.Length; i++)
        {
            int state = 0;
            if (managersStatesArray[i])
            {
                state = 1;
                Debug.Log("—охран€ю в €чейку " + i);
                if (i < productPanelsArray.Length)
                {
                    productPanelsArray[i].manager = true;
                }
            }
            PlayerPrefs.SetInt($"{managerPanelsArray[i].managerSO.managersName}.Manager", state);
        }
    }

    private void _LoadManagersState ()
    {
        for (int i=0; i < managersStatesArray.Length; i++)
        {
            int state = PlayerPrefs.GetInt($"{managerPanelsArray[i].managerSO.managersName}.Manager");
            if (state == 1 && i < managerPanelsArray.Length)
            {
                managerPanelsArray[i].managerState = true;
            }
        }
    }
}
