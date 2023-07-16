using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelFinishedUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameTMP;
    [SerializeField] private TextMeshProUGUI playerCoinAmountTMP;

    [SerializeField] private GameObject levelFinishUIGameObject;
    public void ShowWinner(Player winnerPlayer, int coinAmount)
    {
        levelFinishUIGameObject.SetActive(true);
        playerNameTMP.text = winnerPlayer.PlayerName + " WIN!";
        playerCoinAmountTMP.text = coinAmount + " coins";
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
