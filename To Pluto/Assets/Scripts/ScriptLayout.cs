using UnityEngine;

public class ScriptLayout
{
//GameManager - Instance
	
	//Menu Manager
		// Save
		// Load
		// HS
		// quit

	//Current state
		// Paused
		// Playing
		// Speed? -Not at this time

	//Game Phase
		// Keeps track of what phase the game is on, triggering types of resources that are spawned.


//Asteriod Clicker - Player clicks on asteriods and it turns it into ship inventory on fuel/resource
		// maybe add a Claw object grabbing this at a later date.
		// Level of resource doesnt matter with this?
		// must be within ship range to click

	
//Checkpoint Scriptable Object
		// Name  ex. Pluto
		// Position ex. 5,906,400,000 km
		// bool reached
			//Triggers display text saying we've made it to this checkpoint, or game over
			//triggers achievement potentially

//**********SCRIPT WRITTEN
							//Resource Scriptable Object
								// Level - determines level required for tractor beams, miners, and engine to interact with, what type of fuel and rare resource to produce.
								// Fuel - see Fuel Class below
								// Rare resource - required for upgrades.

							//Resource Spawner - spawns resource at random spot at a certain percentage.
								// Spawn location - picks random spot in the height of the attached game object to spawn an asteroid
								// Pick resource, every set amount of time (+/- so somewhat random looking) resources are spawned. Depending on the game phase you are in the % chance of the resource spawning is determined
	

//Spaceship

	// Current Position - This is not transform position but relative position in galaxy not 
	//Speed
	
	//Acceleration
	
	//Fuel list LIST <Fuel>
		//FUEL CLASS
			// Amount - How much fuel the resource or the ship has
			// Level - the level of the fuel, miners need level to extract, engine need level to burn
			// Color - Change's flame color
			// Size - Hanges flame size
			
	//Fuel Tank size, this is the maximum the ship can hold

	//RareResource Count
	//RareResource inventory size

	//Radar level - resource level it can detect and MAYBE how far the radar can go?

	//Engine LIST
		//Enginer Class
			//Fuel efficiency - how much fuel is burned
			//Acceleration factor - how much acceleration is provided
			//Graphics?

	//Tractor Beam  - auto collects Resources within range of ship
		// Swing speed - how fast it moves to pick up a resource from its current position
		// beam size 
		//Draw in speed - How fast it pulls the resource in
		//strength - Level of resource it can pull in

	//Miner - mines resources that are not obtainable from space ship range
		//Speed - how fast it travels to resource
		//Level - Affects:
			//Level of resource it can mine
			//Amount of resource it can store		


	//Probes - detects resources - if we need an additional unit
		// detects resources away from the ship for the miner to go after



}
