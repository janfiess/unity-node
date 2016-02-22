
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class TestSocketIO : MonoBehaviour{
	private SocketIOComponent socket;

	public void Start() {
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		// node2unity
		node2unity ();
	

		// unity2node
		InvokeRepeating ("unity2node", 1.0f, 3.0f);
		
		// other status messages
		otherStatusMessages ();
	}
	

	// node2unity
	private void node2unity(){
		socket.On("node2unity", (data) =>    {
			// Debug.Log (string.Format ("[name: {0}, data: {1}]", data.name, data.data));

			string msg1=data.data["data1"].ToString();
			msg1 = msg1.Substring(1, msg1.Length-2);
			Debug.Log("node2unity msg1: " + msg1);
			if(msg1 == "Fabi") Debug.Log ("[Log] Correct msg1 reveived !!");

			string msg2=data.data["data2"].ToString();
			msg2 = msg2.Substring(1, msg2.Length-2);
			Debug.Log("node2unity msg2: " + msg2);
			if(msg2 == "Frech") Debug.Log ("[Log] Correct msg2 reveived !!");
		});
	}
	
	// unity2node
	private void unity2node(){
		Dictionary<string, string> data = new Dictionary<string, string>();
		data["email"] = "e@mai.l";
		data["pass"] = "password";
		socket.Emit("unity2node", new JSONObject(data));
	}


	// other status messages
	private void otherStatusMessages(){
		socket.On("open", TestOpen);
		socket.On("error", TestError);
		socket.On("close", TestClose);
	}

	public void TestOpen(SocketIOEvent e){
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}

	public void TestError(SocketIOEvent e){
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}
	
	public void TestClose(SocketIOEvent e){	
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}

}
