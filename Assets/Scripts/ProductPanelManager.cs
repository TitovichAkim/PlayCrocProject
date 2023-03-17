using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class ProductPanelManager : MonoBehaviour
{
    public ProductsSO productSO;
    public ShopManager shopManager;

    public Image productIcon;
    public Image productBackground;
    public Image progressBar;

    public Button sellProductButton;
    public Button upgradeButton;

    public Text productLevelText;
    public Text coinsPerSecondText;
    public Text buyNumberText;
    public Text costFloatText;
    public Text costStringText;
    public Text timerText;

    public int manager;

    private int _productInvestments;
    private float _productRevenue;
    private float _upgradeCost;

    private float _timerStart;
    private bool _timer;
    
    public int productInvestments
    {
        get
        {
            return (_productInvestments);
        }
        set
        {
            _productInvestments = value;
            PlayerPrefs.SetInt($"{productSO.productName}productInvestments", _productInvestments);
            RedrawThePanel();
        }
    }

    private void Start ()
    {
        productIcon.sprite = productSO.icon;
        manager = PlayerPrefs.GetInt($"{productSO.productName}.Manager");
        if (PlayerPrefs.GetInt($"{productSO.productName}productInvestments") == 0 && productSO.productName == "TarotCards")
        {
            productInvestments = 1;
        }
        else
        {
            productInvestments = PlayerPrefs.GetInt($"{productSO.productName}productInvestments");
            if(productInvestments == 0)
            {
                productBackground.enabled = false;
                sellProductButton.interactable = false;
            }
        }
        RedrawUpgradeButton();
    }
 
    private void Update ()
    {
        if (_timer)
        {
            float timer = productSO.initialTime - Mathf.FloorToInt((Time.time - _timerStart) % 60);
            progressBar.fillAmount = (Time.time - _timerStart) / productSO.initialTime;
            timerText.text = $"{timer} сек.";
        }
    }

    public void HireManager ()
    {
        manager = 1;
        PlayerPrefs.SetInt($"{productSO.productName}.Manager", manager);
    }
    public void StartSellProduct ()
    {
        StartCoroutine("SellProduct");
    }
    public IEnumerator SellProduct ()
    {
        do
        {
            _timerStart = Time.time;
            _timer = true;
            productBackground.enabled = false;
            sellProductButton.interactable = false;
            yield return new WaitForSeconds(productSO.initialTime);
            _SellProduct();
        } while(manager == 1);
    }

    private void _SellProduct ()
    {
        shopManager.coins += _productRevenue;
        _timer = false;
        productBackground.enabled = true;
        sellProductButton.interactable = true;
        timerText.text = $"{productSO.initialTime} сек.";
    }

    public void ProductLevelUp ()
    {
        if (shopManager.coins >= _upgradeCost)
        {
            if (productInvestments == 0)
            {
                productBackground.enabled = true;
                sellProductButton.interactable = true;
            }
            shopManager.coins -= _upgradeCost;
            productInvestments++;
            RedrawThePanel();
        }
    }

    private void RedrawThePanel ()
    {
        productLevelText.text = productInvestments.ToString();
        timerText.text = $"{productSO.initialTime} сек.";

        _productRevenue = _productInvestments * productSO.initialRevenue;
        NumberFormatter.FormatAndRedraw(_productRevenue, coinsPerSecondText);

        _upgradeCost = productSO.initialCost * Mathf.Pow(productSO.costMultiplier, productInvestments);
        NumberFormatter.FormatAndRedraw(_upgradeCost, costFloatText, costStringText);
    }

    public void RedrawUpgradeButton ()
    {
        if (shopManager.coins >= _upgradeCost) 
        {
            upgradeButton.interactable = true;
        } 
        else
        {
            upgradeButton.interactable = false;
        }
    }
}
