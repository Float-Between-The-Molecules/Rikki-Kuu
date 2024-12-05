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
		// GD.Print( "RikkiKuu.Utility Static Initializer" );
		
		// best place to put a randomize call
		GD.Randomize();
		
		// locate the scene tree at this time
		Scene = FindSceneTree();
		Assert( Scene is not null );
	}
	
}