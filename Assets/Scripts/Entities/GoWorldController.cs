﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoWorldUnity3D;

public class GoWorldController : MonoBehaviour {

    // Use this for initialization
    public GameObject Player;
    public GameObject ZomBear;
    public GameObject ZomBunny;
    public GameObject Hellephant;
    public GameObject Account;
    public Canvas StatusCanvas;
    public EntityStatus EntityStatus;

    public static string sIp;
    public static int sPort;

    void Start () {
        GameObject.DontDestroyOnLoad(gameObject);
        Debug.Log("Register Entity Type Account ...");
        GoWorld.RegisterEntity(typeof(Account));
        Debug.Log("Register Entity Type Player ...");
        GoWorld.RegisterEntity(typeof(Player));
        Debug.Log("Register Entity Type Monsters ...");
        GoWorld.RegisterEntity(typeof(Monster));
        Debug.Log("Connecting Serer ...");
        // GoWorldUnity3D.GoWorld.Connect("ec2-13-229-128-242.ap-southeast-1.compute.amazonaws.com", 15011);
        //GoWorldUnity3D.GoWorld.Connect("127.0.0.1", 14001);
        GoWorldUnity3D.GoWorld.Connect(sIp, sPort);
    }
	
	// Update is called once per frame
	void Update () {
        GoWorldUnity3D.GoWorld.Tick();
	}

    public EntityStatus GetEntityStatus()
    {
        var status = GameObject.Instantiate(EntityStatus);
        status.transform.SetParent(StatusCanvas.transform, true);
        return status;
    }

}
