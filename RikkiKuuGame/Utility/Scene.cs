/*
Utility SceneTree Functions.

a global Scene property is created for convenience access to the scene tree as early as possible.
*/

namespace RikkiKuu;

static partial class Utility
{
	
	// convenience instance to access the scene tree
	public static readonly SceneTree Scene;
	// {get; private set;} = null;
	
	
	// there are ways to find the scene tree without relying on nodes
	public static SceneTree FindSceneTree ()
	=> (SceneTree) Engine.GetMainLoop();
	
	
}