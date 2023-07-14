using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinAmountTMP;
    public void ShootButtonClick()
    {
        InputManager.instance.HandleShoot();
    }

    private void Start()
    {
        Game.instance.ownerCoinsAmountChangedEvent += OnOwnerCoinAmountChanged;
    }

    private void OnDestroy()
    {
        Game.instance.ownerCoinsAmountChangedEvent -= OnOwnerCoinAmountChanged;
    }

    public void OnOwnerCoinAmountChanged(int coinAmount)
    {
        coinAmountTMP.text = coinAmount.ToString();
    }
}
