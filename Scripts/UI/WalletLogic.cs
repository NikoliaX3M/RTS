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
        _buildingGrid.CreateBuilding += quantityMoney => ReduceAmountMoney(quantityMoney);
        _buildingGrid.DestroyBuilding += quantityMoney => AddMoney(quantityMoney);
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
