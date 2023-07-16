using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyNameTMP;

    private Lobby _lobby;

    public void Initialize(Lobby lobby)
    {
        _lobby = lobby;
        lobbyNameTMP.text = lobby.Name;
    }

    public void OnButtonClick()
    {
        GameLobby.Instance.JoinLobbyWithId(_lobby.Id);
    }
}
