using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSetting : MonoBehaviour {

    public UnityEngine.UI.InputField ipAddressInput;

    // Use this for initialization
    void Start () {
        ipAddressInput.ActivateInputField();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnIpAddress()
    {
        Debug.LogFormat("IpAdress:%s", ipAddressInput.text);
        var array = ipAddressInput.text.Split(':');
        if (array.Length < 1)
        {
            //showMessage("ipAddress is wrong format.");
            return;
        }

        string ip = array[0];
        int port = int.Parse(array[1]);
        //GoWorldUnity3D.GoWorld.Connect(ip, port);
        GoWorldController.sIp = ip;
        GoWorldController.sPort = port;

        SceneManager.LoadScene("Login");
    }
}
