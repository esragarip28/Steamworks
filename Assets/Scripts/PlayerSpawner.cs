using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : NetworkBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject playerPrefab;

    void Start(){

        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnNetworkDespawn()
    {
        Debug.Log("on network spawn");
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneLoad;
    
    }

    private void SceneLoad(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        Debug.Log("scene load");
        Debug.Log(sceneName);
        Debug.Log(clientsCompleted.Count);
        Debug.Log(IsHost);
        if(IsHost && sceneName == "GameScene")
        {
            foreach(ulong id in clientsCompleted){

                GameObject  player = Instantiate(playerPrefab);
                player.GetComponent<NetworkObject>().SpawnAsPlayerObject(id, true);

            }
            

        }
    }
}
  