using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Product")]
public class ProductsSO : ScriptableObject
{
    public Sprite icon;

    public string productName;
    public float productSaleCost;
    public float productUpCost;
    public float productionTime;
}
