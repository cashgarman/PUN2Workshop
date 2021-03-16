using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _statusText;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private Button _playButton;

    private void Awake()
    {
        // Clear the status text
        _statusText.text = "";

        // Photon config
        PhotonNetwork.AutomaticallySyncScene = true;
        
        // Restore the saved player name if there is one
        _nameInput.text = PlayerPrefs.GetString("playername", "");
        
        // Disable the inputs until connected to the master server
        SetInputsActive(false);
    }

    private void Start()
    {
        // Connect to the master server
        PhotonNetwork.ConnectUsingSettings();
    }

    private void SetStatus(string message)
    {
        if(_statusText)
            _statusText.text = message;
        Debug.Log(message);
    }

    public void OnClick_Play()
    {
        // Make sure a valid play name was entered
        if (string.IsNullOrEmpty(_nameInput.text))
        {
            SetStatus("Please enter a player name");
            return;
        }
        
        // Disable the inputs
        SetInputsActive(false);

        // Store the player name for next time
        PlayerPrefs.SetString("playername", _nameInput.text);
        
        // Attempt to join a random room
        PhotonNetwork.NickName = _nameInput.text;
        PhotonNetwork.JoinRandomRoom();
        SetStatus("Joining random room...");
    }

    private void SetInputsActive(bool active)
    {
        _playButton.interactable = active;
        _nameInput.interactable = active;
    }

    public override void OnConnectedToMaster()
    {
        SetStatus("Connected to master");
        SetInputsActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SetStatus($"Disconnection: {cause.ToString()}");
    }

    public override void OnJoinedRoom()
    {
        SetStatus("Joined main room");
        
        // If we're the master client
        if (PhotonNetwork.IsMasterClient)
        {
            // Load the game level
            PhotonNetwork.LoadLevel("Game");   
        }
    }

    public override void OnCreatedRoom()
    {
        SetStatus("Created main room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        SetStatus($"Create room failed: {message}");

        // Reenable the inputs
        SetInputsActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        SetStatus($"Join random room failed: {message}");
        
        // There was no random room to join, so create one
        PhotonNetwork.JoinOrCreateRoom("main", new RoomOptions
        {
            MaxPlayers = 20,
        }, TypedLobby.Default);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
