using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinAmountTMP;
    [SerializeField] private LevelFinishedUIController levelFinishedUIController;

    [SerializeField] private GameObject inputGameUI;
    public void ShootButtonClick()
    {
        InputManager.instance.HandleShoot();
    }

    private void Start()
    {
        Game.instance.ownerCoinsAmountChangedEvent += OnOwnerCoinAmountChanged;
        Game.instance.levelFinishedEvent += OnLevelFinished;
    }

    private void OnDestroy()
    {
        Game.instance.ownerCoinsAmountChangedEvent -= OnOwnerCoinAmountChanged;
        Game.instance.levelFinishedEvent -= OnLevelFinished;
    }

    private void OnOwnerCoinAmountChanged(int coinAmount)
    {
        coinAmountTMP.text =  "COINS: " + coinAmount.ToString();
    }

    private void OnLevelFinished(Player winnerPlayer, int coinAmount)
    {
        inputGameUI.SetActive(false);
        levelFinishedUIController.ShowWinner(winnerPlayer, coinAmount);
    }
}
