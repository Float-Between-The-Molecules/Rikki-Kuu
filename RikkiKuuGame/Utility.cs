/*
Utility Class.
suggestive usage -> using static Utility;
*/

namespace RikkiKuu;

public static partial class Utility
{
	
	
	// static initializer runs at first usage of this class, which should be some bootstrapper in principle
	static Utility ()
	{
		
		// best place to put a randomize call, this will seed godot's random functions immediately before Utility.Random functions are called
		GD.Randomize();
		
		// locate the scene tree at this time
		Scene = FindSceneTree();
		
		// ensure scene tree is found or there will be problems
		Assert(()=> Scene != null );
		
	}
	
}