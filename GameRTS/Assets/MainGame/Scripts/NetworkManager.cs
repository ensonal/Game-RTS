using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("Connection Status")]
    public Text connectionStatusText;
    
    [Header("Login Panel")]
    public InputField playerNameInput;
    public GameObject loginPanel;
    
    [Header("Game Options Panel")]
    public GameObject gameOptionsPanel;
    
    [Header("Create Room Panel")]
    public GameObject createRoomPanel;
    public InputField roomNameInputField;
    public InputField maxPlayerInputField;

    [Header("Inside Room Panel")] 
    public GameObject insideRoomPanel;
    public Text roomInfoText;
    public GameObject playerListPrefab;
    public GameObject playerListContent;
    public GameObject startGameButton;
    
    [Header("Room List Panel")]
    public GameObject roomListPanel;
    public GameObject roomListEntryPrefab;
    public GameObject roomListParentGameObject;
    
    [Header("Join Random Room Panel")]
    public GameObject joinRandomRoomPanel;

    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListGameObjects;
    private Dictionary<int, GameObject> playerListGameObjects;
    
    #region Methods For Unity
    
    void Start()
    {
        /*loginPanel.SetActive(true);
        gameOptionsPanel.SetActive(false); */
        ActivatePanel(loginPanel.name);
        
        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListGameObjects = new Dictionary<string, GameObject>();
        
        PhotonNetwork.AutomaticallySyncScene = true;

    }
    
    void Update()
    {
        connectionStatusText.text = "Connection Status: " + PhotonNetwork.NetworkClientState;
    }
    #endregion
    
    #region Methods For UI
    public void OnLoginButtonClicked()
    {
        string playerName = playerNameInput.text;
        if(!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Player Name is invalid");
        }
    }

    public void OnCreateRoomButtonClicked()
    {
        string roomName = roomNameInputField.text;
        if(string.IsNullOrEmpty(roomName))
        {
            roomName = "Room " + Random.Range(1000, 10000);
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte) int.Parse(maxPlayerInputField.text);
        
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void OnCancelButtonClicked()
    {
        ActivatePanel(gameOptionsPanel.name);
    }

    public void OnShowRoomListButtonClicked()
    {
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        ActivatePanel(roomListPanel.name);
    }

    public void OnBackButtonClicked()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        ActivatePanel(gameOptionsPanel.name);
    }

    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    public void OnStartGameButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Environment_Free");
        }
    }
    #endregion
    
    #region Methods For Photon
    public override void OnConnected()
    {
        Debug.Log("Connected to Internet");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected to Photon");
        ActivatePanel(gameOptionsPanel.name);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created.");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
        ActivatePanel(insideRoomPanel.name);

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }
        
        roomInfoText.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name + " " +
                            "Players/Max.Players: " + 
                            PhotonNetwork.CurrentRoom.PlayerCount + "/" +
                            PhotonNetwork.CurrentRoom.MaxPlayers;

        if (playerListGameObjects == null)
        {
            playerListGameObjects = new Dictionary<int, GameObject>();
        }
        

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerListGameObject = Instantiate(playerListPrefab);
            playerListGameObject.transform.SetParent(playerListContent.transform);
            playerListGameObject.transform.localScale = Vector3.one;
            
            playerListGameObject.transform.Find("PlayerNameText").GetComponent<Text>().text = player.NickName;
            
            if(player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(true);
            }
            else
            {
                playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(false);
            }
            
            playerListGameObjects.Add(player.ActorNumber, playerListGameObject);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        roomInfoText.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name + " " +
                            "Players/Max.Players: " + 
                            PhotonNetwork.CurrentRoom.PlayerCount + "/" +
                            PhotonNetwork.CurrentRoom.MaxPlayers;
        
        GameObject playerListGameObject = Instantiate(playerListPrefab);
        playerListGameObject.transform.SetParent(playerListContent.transform);
        playerListGameObject.transform.localScale = Vector3.one;
            
        playerListGameObject.transform.Find("PlayerNameText").GetComponent<Text>().text = newPlayer.NickName;
            
        if(newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(true);
        }
        else
        {
            playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(false);
        }
            
        playerListGameObjects.Add(newPlayer.ActorNumber, playerListGameObject);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        roomInfoText.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name + " " +
                            "Players/Max.Players: " + 
                            PhotonNetwork.CurrentRoom.PlayerCount + "/" +
                            PhotonNetwork.CurrentRoom.MaxPlayers;
        
        Destroy(playerListGameObjects[otherPlayer.ActorNumber].gameObject);
        playerListGameObjects.Remove(otherPlayer.ActorNumber);

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
        
    }
    public override void OnLeftRoom()
    {
        ActivatePanel(gameOptionsPanel.name);

        foreach (GameObject playerListGameObject in playerListGameObjects.Values)
        {
            Destroy(playerListGameObject);
        }
        
        playerListGameObjects.Clear();
        playerListGameObjects = null;
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListView();
        
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if(!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if(cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList.Remove(room.Name);
                }
            }
            else
            {
                if(cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList[room.Name] = room;
                }
                else
                {
                    cachedRoomList.Add(room.Name, room);
                }
            }
            //cachedRoomList.Add(room.Name, room);
        }

        foreach (RoomInfo room in cachedRoomList.Values)
        {
            GameObject roomListEntryGameObject = Instantiate(roomListEntryPrefab);
            roomListEntryGameObject.transform.SetParent(roomListParentGameObject.transform);
            roomListEntryGameObject.transform.localScale = Vector3.one;
            
            roomListEntryGameObject.transform.Find("RoomNameText").GetComponent<Text>().text = room.Name;
            roomListEntryGameObject.transform.Find("RoomPlayersText").GetComponent<Text>().text = room.PlayerCount + " / " + room.MaxPlayers;
            roomListEntryGameObject.transform.Find("JoinRoomButton").GetComponent<Button>().onClick.AddListener(()=> OnJoinRoomButtonClicked(room.Name));
            
            roomListGameObjects.Add(room.Name, roomListEntryGameObject);
            
        }
    }
    public override void OnLeftLobby()
    {
        ClearRoomListView();
        cachedRoomList.Clear();
    }
    
    #endregion

    #region Private Methods

    void OnJoinRoomButtonClicked(string _roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        PhotonNetwork.JoinRoom(_roomName);
    }

    void ClearRoomListView()
    {
        foreach (var roomListGameObject in roomListGameObjects.Values)
        {
            Destroy(roomListGameObject);
        }
        
        roomListGameObjects.Clear();
    }
    

    #endregion
    
    #region Public Methods

    public void ActivatePanel(string panelToBeActivated)
    {
        loginPanel.SetActive(panelToBeActivated.Equals(loginPanel.name));
        gameOptionsPanel.SetActive(panelToBeActivated.Equals(gameOptionsPanel.name));
        createRoomPanel.SetActive(panelToBeActivated.Equals(createRoomPanel.name));
        insideRoomPanel.SetActive(panelToBeActivated.Equals(insideRoomPanel.name));
        roomListPanel.SetActive(panelToBeActivated.Equals(roomListPanel.name));
        joinRandomRoomPanel.SetActive(panelToBeActivated.Equals(joinRandomRoomPanel.name));
    }
    #endregion
}
