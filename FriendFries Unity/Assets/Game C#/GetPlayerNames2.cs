﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script can get player names from the player input boxes and saves those names to a PlayerClass array.
//requires script "GetNumPlayers" to work
//Script also randomly picks a single unpicked player to display every scene.

public class GetPlayerNames2 : MonoBehaviour {

	public Text currPlayerText;
	public bool showText;

	public bool getNames;
	public InputField[] getPlayerName;

	public static PlayerClass[] players;
	public int currNum;

	public static int numPlayingPlayers;

	bool missingName;
	bool sameName;
	public static bool gotProblem;

	public void Start()
	{	
		missingName = true;
		sameName = true;
		gotProblem = true; //assume problem unless otherwise

		showText = true;
		numPlayingPlayers = GetNumPlayers.numPlayers;

		//Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
		//Passes the main input field into the method when "LockInput" is invoked
		if (getNames) {
			showText = false;

			numPlayingPlayers = GetNumPlayers.numPlayers;
			players = new PlayerClass[10];

			getPlayerName [0].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [0], 0);
			});

			getPlayerName [1].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [1], 1);
			});

			getPlayerName [2].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [2], 2);
			});

			getPlayerName [3].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [3], 3);
			});

			getPlayerName [4].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [4], 4);
			});

			getPlayerName [5].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [5], 5);
			});

			getPlayerName [6].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [6], 6);
			});

			getPlayerName [7].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [7], 7);
			});

			getPlayerName [8].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [8], 8);
			});

			getPlayerName [9].onEndEdit.AddListener (delegate {
				LockInput (getPlayerName [9], 9);
			});
		}

	}

	// Checks if there is anything entered into the input field.
	void LockInput(InputField input, int arrayPlayerNo) {
		int actualPlayerNo = arrayPlayerNo + 1;

		if (input.text.Length > 0) {
			Debug.Log(input.text + " has been entered");
			players [arrayPlayerNo] = new PlayerClass (input.text, arrayPlayerNo);
			Debug.Log ("Player " + actualPlayerNo + "'s name is: " + players[arrayPlayerNo].getName());

		} else if (input.text.Length == 0) {
			Debug.Log("Input Empty");
			players [arrayPlayerNo] = new PlayerClass ("", arrayPlayerNo); //assigns null to empty input fields

		}
	}

	//Update checks for name problems and picks a random player each scene
	void Update() {
		
		if (!showText) {
			missingName = checkPlayers ();
			if (!missingName) {
				sameName = checkSameName ();
			}

			if (!missingName && !sameName) {
				gotProblem = false;
			}
		}

		if (showText) {
			showText = false;
			currNum = pickRandomNumber ();
			Debug.Log ("Number picked " + currNum);

			//repicks player if player was choosen before
			while (players [currNum].getPicked()) {
				currNum = pickRandomNumber ();
				Debug.Log ("Number picked " + currNum);

			}

			//displays picked player
			Debug.Log ("Current player is: " + players[currNum].getName());
			currPlayerText.text = "Player " + players[currNum].getName();
			players [currNum].isPicked ();

		}
	}

	//------------------------------------------------------------------------------//
	//Functions used to check for problems with names

	//checks if the number of name inputs = the number of players
	bool checkPlayers(){
		for (int i = 0; i < numPlayingPlayers; i++) {
			if (players [i].getName().Equals("")) {
				Debug.Log ("missing name");
				return true;
			} 
		}

		return false;
	}

	//checks if any of the players have the same name
	bool checkSameName(){
		for (int x = 0; x < numPlayingPlayers; x++) { //loop through players
			for (int y = x + 1; y < numPlayingPlayers; y++) { //loop through other players
				//Debug.Log("Player" + x +":" + players[x].getName());
				//Debug.Log("Player" + y +": " + players[y].getName());

				if (players[x].getName().Equals (players[y].getName())) {
					Debug.Log ("same name");
					return true;
				}

			}
		}

		return false;
	}

	public bool checkIfProblem(){
		return gotProblem;
	}

	//-------------------------------------------------------------------------------------//
	//Functions used for picking a player each scene

	public int pickRandomNumber(){
		int randomNumPicked = Random.Range (0, numPlayingPlayers);
		Debug.Log ("Random Picked Number: " + randomNumPicked);
		return randomNumPicked;
	}
		
	public void printPlayerList(){
		for(int i=0; i < numPlayingPlayers; i++){
			Debug.Log ("Player " + players [i].getNumber () + " is " + players [i].getName ());
		}
	}

}

//----------------------------------------------------------------------------------------//

public class PlayerClass {

	private string playerName;
	private int playerNumber;
	private bool hasBeenPicked;

	public PlayerClass (string name, int num) {
		Debug.Log ("Creating new player: " + name);
		playerName = name;
		playerNumber = num;
		hasBeenPicked = false;
	}

	public void isPicked(){
		hasBeenPicked = true;
	}

	public string getName(){
		return playerName;
	}

	public int getNumber(){
		return playerNumber;
	}

	public bool getPicked(){
		return hasBeenPicked;
	}
}