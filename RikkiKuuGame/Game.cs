/*
class Game

Main entry point for the game and acts as an all-available global scope
*/

namespace RikkiKuu;

using static Utility;

[Tool]
public sealed partial class Game : Node
{
	
	static Game Singleton = default!;
	
	
	static Game ()
	{
		
	}
	
	
	public Game ()
	{
		Singleton = this;
		
		GD.Print("game instance created");
	}
	
	
	override public void _Ready ()
	{
		
	}
	
}