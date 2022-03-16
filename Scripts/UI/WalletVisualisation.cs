using UnityEngine;
using TMPro;

public class WalletVisualisation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMoney;

    void Start()
    {
        WalletLogic.UpdateMoneyText += text => _textMoney.text = text.ToString();
    }   
}
