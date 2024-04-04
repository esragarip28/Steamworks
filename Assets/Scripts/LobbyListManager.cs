using Steamworks;
using Steamworks.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class LobbyListManager : MonoBehaviour
{

    public static LobbyListManager instance;
    private Dictionary<string, GameObject> LobbyList = new Dictionary<string, GameObject>();
    [SerializeField] private GameObject LobbyPrefab;
    [SerializeField] private GameObject LobbyListField;
    private void AddLobbyToDictionary(string LobbyID,string lobbyName, string HostName,int playerCount)
    {
        LobbyInfo lobbyInfo = Instantiate(LobbyPrefab, LobbyListField.transform).GetComponent<LobbyInfo>();
        lobbyInfo.LobbyNameText = lobbyName;
        lobbyInfo.playerCountText = playerCount;
        lobbyInfo.hostNameText = HostName;
        LobbyList.Add(LobbyID, lobbyInfo.gameObject);

    }

    private void Awake()
    {
        if( instance== null ) { instance = this;


            getLobbyList();
        }


    }

    private async void getLobbyList()
    {

        LobbyQuery query = SteamMatchmaking.LobbyList;
        Steamworks.Data.Lobby[] lobbyList = await SteamMatchmaking.LobbyList.RequestAsync();

        foreach (var lobby in lobbyList)
        {
            //Debug.Log("Lobby ID: " + lobby.Id + ", Members: " + lobby.MemberCount);
            AddLobbyToDictionary(lobby.Id.ToString(), lobby.Id.ToString(), lobby.Owner.Name, lobby.MemberCount);

        }



    }


    public void RefreshList()
    {

        ClearLobbyList();
        getLobbyList();

    }

    public void ClearLobbyList()
    {


        LobbyList.Clear();
        GameObject[] lobbylist = GameObject.FindGameObjectsWithTag("LobbyRow");
        foreach (GameObject lobby in lobbylist)
        {
            Destroy(lobby);
        }
        Debug.Log("clearing lobby list");


    }


}
