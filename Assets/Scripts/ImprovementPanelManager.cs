using UnityEngine;
using UnityEngine.UI;

public class ImprovementPanelManager: MonoBehaviour
{
    public ImprovementSO improvementSO;
    public ShopManager shopManager;

    public Image improvementIconImage;
    public Text improvementDescriptionText;
    public Text improvementCostText;
    public Button buyButton;

    private bool _improvementState;

    public bool improvementState
    {
        get
        {
            return (_improvementState);
        }
        set
        {
            _improvementState = value;
        }
    }

    public void Start ()
    {
        string improvementTypeText = "ОШИБКА";
        switch(improvementSO.improvementsType)
        {
            case 0:
                improvementTypeText = "Увеличивает доход";
                break;
            case 1:
                improvementTypeText = "Сокращает время";
                break;
        }

        improvementIconImage.sprite = improvementSO.improvementsIcon;
        improvementDescriptionText.text = $"{improvementSO.improvementsName} " +
            $"\n{improvementTypeText} " +
            $"\n в {improvementSO.improvementsValue} раз";
        NumberFormatter.FormatAndRedraw(improvementSO.improvementsCost, improvementCostText);
    }

    public void RedrawThePanel ()
    {
        buyButton.interactable = shopManager.coins >= improvementSO.improvementsCost;
    }

    public void BuyImprovement ()
    {
        if(!improvementState)
        {
            if(shopManager.coins >= improvementSO.improvementsCost)
            {
                shopManager.coins -= improvementSO.improvementsCost;
                improvementState = true;
                shopManager.SaveImprovementsStates();
                _UpdateImprovementStateArray(this);
            }
        }
    }

    private void _UpdateImprovementStateArray (ImprovementPanelManager panelManager)
    {
        for(int i = 0; i < shopManager.improvementPanelArray.Length; i++)
        {
            if(shopManager.improvementPanelArray[i] == panelManager)
            {
                shopManager.improvementsStatesArray[i] = improvementState;
                shopManager.ApplyImprovementState(improvementSO.improvementsType, improvementSO.improvementsTargetIndex, i);
                shopManager.improvementsStatesArray = shopManager.improvementsStatesArray;
            }
        }
    }
}
