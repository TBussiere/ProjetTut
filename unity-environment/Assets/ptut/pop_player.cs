using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pop_player : MonoBehaviour {

    public static int playerCount;

	public GameObject mainPlayerPrefab;
    public GameObject otherPlayerPrefab;

    public Transform spawnPoint;

	// Use this for initialization
	void Start () {
		//playerCount = 0;
		//print ("bbbbbbbbbbbbbbbbbbbb");
	}
	public playerControler[] players;
	public OtherPlayerControler[] otherplayers;
	// Update is called once per frame
	void Update () {
		players = FindObjectsOfType<playerControler>();
		//print (players.Length);

		//print (otherplayers.Length);



		if (players.Length == 0)
        {
			//print ("aaaaaaaaaaaaaaaaaaaaa");
            //var temp = Instantiate(mainPlayerPrefab);//, spawnPoint.position,spawnPoint.rotation
            //temp.transform.position = new Vector3(6.4f, 6.4f);
		//	Debug.Log(temp.transform.position);
        }/*
		otherplayers = FindObjectsOfType<OtherPlayerControler>();
		if (otherplayers.Length == 0)//spawn enemies 14.3 0.65
        {
			//print ("otherplayer");
			//var temp2 = Instantiate(otherPlayerPrefab);
            //temp2.transform.position = new Vector3(14.3f, 0.65f);
           // Debug.Log(temp2.transform.position);
        }*/
	}


    public static void removePlayer(GameObject o)
    {
        if (o != null)
        {
            playerCount--;
            GameObject.Destroy(o);
        }
    }
    public static void addPlayerCount()
    {
        playerCount++;
		print (playerCount);
    }
}
