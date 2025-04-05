using UnityEngine;
using TMPro;
using Unity.Netcode;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject player1Indicator;
    [SerializeField] private GameObject player2Indicator;
    [SerializeField] private TextMeshProUGUI connectionStatusText;

    private void Awake()
    {
        player1Indicator.SetActive(false);
        player2Indicator.SetActive(false);
        connectionStatusText.text = "";
    }

    private void Start()
    {
        OnlineGameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        OnlineGameManager.Instance.OnPlayerDisconnected += GameManager_OnPlayerDisconnected;

        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectedLocally;
    }

    private void GameManager_OnGameStarted(object sender, System.EventArgs e)
    {
        connectionStatusText.text = "Connected";

        if (OnlineGameManager.Instance.GetLocalPlayerType() == OnlineGameManager.PlayerType.Host)
            player1Indicator.SetActive(true);
        else
            player2Indicator.SetActive(true);
    }

    private void GameManager_OnPlayerDisconnected(object sender, System.EventArgs e)
    {
        ShowDisconnected();
    }

    private void OnClientDisconnectedLocally(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            ShowDisconnected();
        }
    }

    private void ShowDisconnected()
    {
        connectionStatusText.text = "Disconnected";
        player1Indicator.SetActive(false);
        player2Indicator.SetActive(false);
    }
}
