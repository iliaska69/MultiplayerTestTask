using Unity.Netcode;
using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        SceneLoader.LoadNetworkScene(SceneLoader.Scene.GameScene);
    }


    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
