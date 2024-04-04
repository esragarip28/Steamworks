using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyInfo : MonoBehaviour
{
    [SerializeField] public TMP_Text lobbyName, HostName, playerCount;
    public string LobbyNameText;
    public int playerCountText;
    public string hostNameText;

    void Start()
    {

        lobbyName.text = LobbyNameText;
        HostName.text = hostNameText;
        playerCount.text = playerCountText.ToString();

    }

    
}
