using System.Collections.Generic;
using UnityEngine;

public class LobbyUIController : MonoBehaviour
{

    private string _lobbyName = "NAME";
    
    public List<GameObject> _lobbyButtonList = new List<GameObject>();

    private void Awake()
    {
        foreach (var lobbyButton in _lobbyButtonList)
        {
            lobbyButton.SetActive(false);
        }
    }

    public void UpdateLobbyName(string lobbyName)
    {
        _lobbyName = lobbyName;
    }

    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
    }

    public void StartHost()
    {
        GameLobby.Instance.CreateLobby(_lobbyName);
    }
    
    public void StartClient()
    {
        GameLobby.Instance.JoinLobby();
    }

    public void UpdateLobbyList()
    {
        GameLobby.Instance.UpdateLobbiesList();

        var lobbyList = GameLobby.Instance.LobbiesList;

        foreach (var lobbyButton in _lobbyButtonList)
        {
            lobbyButton.SetActive(false);
        }

        var lobbyButtonInc = 0;
        foreach (var lobby in lobbyList)
        {
            if (_lobbyButtonList.Count < lobbyButtonInc) break;
            _lobbyButtonList[lobbyButtonInc].SetActive(true);
            _lobbyButtonList[lobbyButtonInc].GetComponent<LobbyButton>().Initialize(lobby);
            lobbyButtonInc++;
        }
    }
}
