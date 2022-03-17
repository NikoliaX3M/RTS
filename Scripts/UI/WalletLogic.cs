using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WalletVisualisation))]
public class WalletLogic : MonoBehaviour
{
    static public int AmountMoney { get; private set; }
    static public event UnityAction<int> UpdateMoneyText;

    [SerializeField] private BuildingGrid _buildingGrid;

    void Start()
    {
        AmountMoney = 100000;
        UpdateMoneyText?.Invoke(AmountMoney);
        _buildingGrid.CreateBuilding += buildingProfile => ReduceAmountMoney(buildingProfile.Price);
        _buildingGrid.DestroyBuilding += buildingProfile => AddMoney(buildingProfile.Price);
    }

    private void ReduceAmountMoney(int quantityMoney)
    {
        if (quantityMoney > AmountMoney)
            throw new ArgumentOutOfRangeException();

        AmountMoney -= quantityMoney;
        UpdateMoneyText?.Invoke(AmountMoney);
    }

    private void AddMoney(int quantityMoney)
    {
        AmountMoney += quantityMoney;
        UpdateMoneyText?.Invoke(AmountMoney);
    }
}
