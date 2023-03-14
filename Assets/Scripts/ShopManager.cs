using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text numbersOfCoinsFloatText;
    public Text numbersOfCoinsstringText;

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

            numbersOfCoinsFloatText.text = _coins.ToString();
            numbersOfCoinsstringText.text = "";
            PlayerPrefs.SetFloat("Coin", _coins);
        }
    }

    public void Start ()
    {
        coins = PlayerPrefs.GetFloat("Coin");
    }
}
