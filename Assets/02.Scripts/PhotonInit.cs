using UnityEngine;
using System.Collections;

public class PhotonInit : MonoBehaviour {

    public string version = "v1.0";

    void Awake()
    { 
        //连接photo Cloud
        PhotonNetwork.ConnectUsingSettings(version);
    }


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //正常连接PhotonCloud并进入大厅后调用的回调函数
    void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby");

        PhotonNetwork.JoinRandomRoom();

    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No Rooms!");

        PhotonNetwork.CreateRoom("MyRoom");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Enter Room!");

        CreateTank();

    }

    void CreateTank()
    {
        float pos = Random.Range(-100.0f,100.0f);

        PhotonNetwork.Instantiate("Tank",new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    }


    void OnGUI()
    { 
        //画面左上角出现连接过程日志
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

}
