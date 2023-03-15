using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text numbersOfCoinsFloatText;
    public Text numbersOfCoinsstringText;

    public ProductPanelManager[] productPanelsArray;

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

            numbersOfCoinsFloatText.text = _coins.ToString("F3");
            numbersOfCoinsstringText.text = "";
            PlayerPrefs.SetFloat("Coin", _coins);
            _RedrawUpgradeButtons();
        }
    }

    public void Start ()
    {
        coins = PlayerPrefs.GetFloat("Coin");
        _RedrawUpgradeButtons();
    }

    private void _RedrawUpgradeButtons ()
    {
        foreach(ProductPanelManager prodMan in productPanelsArray)
        {
            prodMan.RedrawUpgradeButton();
        }

    }
}
