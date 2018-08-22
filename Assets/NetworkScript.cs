using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkScript : NetworkManager {

	void OnStartServer(){
		print ("On start server");
	}
	void OnStartClient(){
		print ("On start client");
	}
}
