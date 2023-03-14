using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProductPanelManager : MonoBehaviour
{
    public ProductsSO productSO;
    public ShopManager shopManager;

    public Image productIcon;
    public Image productBackground;
    public Image progressBar;

    public Button sellProductButton;

    public Text productLevelText;
    public Text coinsPerSecondText;
    public Text buyNumberText;
    public Text costFloatText;
    public Text costStringText;
    public Text timerText;

    public int manager;

    private int _productLevel;

    private float _timerStart;
    private bool _timer;
    
    public int productLevel
    {
        get
        {
            return (_productLevel);
        }
        set
        {
            _productLevel = value;
            PlayerPrefs.SetInt(productSO.productName, _productLevel);
            RedrawThePanel();
        }
    }

    private void Start ()
    {
        productIcon.sprite = productSO.icon;
        if (PlayerPrefs.GetInt(productSO.productName) != 0)
        {
            productLevel = PlayerPrefs.GetInt(productSO.productName);
            manager = PlayerPrefs.GetInt($"{productSO.productName}.Manager");
        }
    }
 
    private void Update ()
    {
        if (_timer)
        {
            float timer = productSO.productionTime - Mathf.FloorToInt((Time.time - _timerStart) % 60);
            progressBar.fillAmount = (Time.time - _timerStart) / productSO.productionTime;
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
            sellProductButton.interactable = false;
            productBackground.enabled = false;
            yield return new WaitForSeconds(productSO.productionTime);
            _SellProduct();
        } while(manager == 1);
    }

    private void _SellProduct ()
    {
        shopManager.coins += _productLevel * productSO.productSaleCost;
        _timer = false;
        sellProductButton.interactable = true;
        productBackground.enabled = true;
        timerText.text = $"{productSO.productionTime} сек.";
    }

    public void ProductLevelUp ()
    {
        if (shopManager.coins >= _productLevel * productSO.productUpCost)
        {
            shopManager.coins -= _productLevel * productSO.productUpCost;
            _productLevel++;
            RedrawThePanel();
        }
    }

    private void RedrawThePanel ()
    {
        productLevelText.text = productLevel.ToString();
        string coinsPerSecond = ((_productLevel * productSO.productSaleCost)/ productSO.productionTime).ToString("F2");
        coinsPerSecondText.text = $"{coinsPerSecond}/сек.";
        costFloatText.text = $"{_productLevel * productSO.productUpCost}";
        costStringText.text = "";
    }
}
