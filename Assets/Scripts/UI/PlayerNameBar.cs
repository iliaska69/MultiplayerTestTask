using TMPro;
using UnityEngine;

public class PlayerNameBar : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI playerNameTMP;
    void Awake()
    {
        player.playerNameChangedEvent += OnPlayerNameChanged;
    }

    private void OnDestroy()
    {
        player.playerNameChangedEvent -= OnPlayerNameChanged;
    }

    private void OnPlayerNameChanged(string newPlayerName)
    {
        playerNameTMP.text = newPlayerName;
    }
}
