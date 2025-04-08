using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class OnlineGameManager : NetworkBehaviour
{
    public bool startPlaying;
    public static OnlineGameManager Instance { get; private set; }
    public int currentScore;
    public int currentCombo;
    public int scorePerNormalNote = 6;
    public int scorePerGoodNote = 8;
    public int scorePerPerfectNote = 10;

    public Text scoreText;
    public Text comboText;

    public float totalNotes;
    public float NormalHits;
    public float goodHits;
    public float PerfectHits;
    public float missedHits;

    public enum PlayerType {
        None,
        Host,
        Client,
    }

    private PlayerType localPlayerType;
    public event EventHandler OnGameStarted;
    public event EventHandler OnPlayerDisconnected;

    private bool gameStartTriggered = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        Instance = this;
        scoreText.text = "Score: 0";
        comboText.text = "Combo: 0";
    }

    public override void OnNetworkSpawn()
    {
        Debug.Log("Local client ID is " + NetworkManager.Singleton.LocalClientId);
        if (NetworkManager.Singleton.LocalClientId == 0)
        {
            localPlayerType = PlayerType.Host;
        }
        else if (NetworkManager.Singleton.LocalClientId == 1)
        {
            localPlayerType = PlayerType.Client;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectedCallback;
    }

    private void NetworkManager_OnClientConnectedCallback(ulong obj)
    {
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 2 && !gameStartTriggered)
        {
            gameStartTriggered = true;
            Debug.Log("Both players connected!");
            TriggerOnGameStartedRpc();
            StartCoroutine(StartGameAfterDelay(5f));
        }
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        Debug.Log("Game will start in " + delay + " seconds...");
        yield return new WaitForSeconds(delay);

        TriggerStartPlayingRpc();
        TriggerOnGameStartedRpc();
    }

    private void NetworkManager_OnClientDisconnectedCallback(ulong obj)
    {
        Debug.Log("A player disconnected.");
        TriggerOnPlayerDisconnectedRpc();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void TriggerOnPlayerDisconnectedRpc()
    {
        OnPlayerDisconnected?.Invoke(this, EventArgs.Empty);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void TriggerOnGameStartedRpc()
    {
        OnGameStarted?.Invoke(this, EventArgs.Empty);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void TriggerStartPlayingRpc()
    {
        Debug.Log("Received start playing RPC!");
        startPlaying = true;
    }

    public void NoteHit()
    {
        currentCombo++;
        scoreText.text = "Score: " + currentScore;
        comboText.text = "Combo: " + currentCombo;
    }

    public void NormalHit()
    {
        currentScore += scorePerNormalNote;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote;
        NoteHit();
    }

    public void NoteMissed()
    {
        comboText.text = "Combo: 0";
        currentCombo = 0;
    }

    public PlayerType GetLocalPlayerType()
    {
        return localPlayerType;
    }
}
