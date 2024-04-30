using Godot;
using System;
using System.Numerics;


public partial class turnSystem : Node
{
	public int turn = 1; 
	public string[] players_name = {"yihan", "willman"}; //DEBUG TEST NAMES  
	private int subturn = 0; 			//turn order of players in array players_name

	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}


	private void _turn_button_pressed()
	//function which truggers every time turn button is pressed
	//incrments subturn for number if time equal to amount of players
	//then increments turn counter 
	{
		//checks if there have been a number of subturns equal to number of players 
		if(subturn + 1 > players_name.Length - 1 ) { //Length -1 is for 0 index
			//increments turn and resets subturn to 0
			++turn; 
			subturn = 0; 

		} else {
			//increments subturn
			++subturn;
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	
}
