using UnityEngine;
using System.Collections;

public class NetworkConnetion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Connect();
	}

    void Connect() {
        PhotonNetwork.ConnectUsingSettings("v1.0.0");
    }

    void OnGUI() {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnConnectedToMaster() {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed() {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom() {
        Debug.Log("OnJoinedRoom");
    }

}
