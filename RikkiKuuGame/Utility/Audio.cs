/*
Utility Audio Things
*/

namespace RikkiKuu;

static partial class Utility
{
	
	// godot's function needs an extra curve parameter to match perception, with double precision for math only
	public static float Volume2DB (float volume)
	=> (float) Mathf.LinearToDb( Mathf.Pow((double)Clamp(volume, 0f, 1f), 2.0) );
	
}