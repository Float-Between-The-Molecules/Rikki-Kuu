/*
Rikki Kuu Framework

Refer to documentation for details.
*/

namespace RikkiKuu;

using static Utility;

public static partial class Framework
{
	
	// is this a case of "mod loads game"?
	public static readonly bool IsModProjectEnvironment;
	
	
	// is the user path that of the game or somewhere else?
	// public static readonly bool IsUserPathWrong;
	public static readonly bool NonStandardProjectPath;
	
	
	#pragma warning disable CA2255
	[ModuleInitializer, Conditional("DEBUG")]
	internal static void _StaticInitializerHook ()
	{
		// godot runtime takes issue with module initializers; this is purely for the benefit of the editor
		if (!GodotEditorFound) return;
		
		// _it knows_
		if (IsModProjectEnvironment) {
			
			// editor environment will have problems if it tries to look for content in other packs or dll's
			GD.PushWarning( "Rikki Kuu Modding Detected - Good Fun, Have Luck... Avoid instantiating non-local content" );
		}
		
		// a wrong user dir means it won't access the same settings as the game
		if (NonStandardProjectPath) {
			GD.PushWarning( "Project custom user dir is not set up for live game settings to be available" );
		}
	}
	#pragma warning restore CA2255
	
	
	// effectively runs just before any symbol in this module is used, including, strangely, the module initializer
	static Framework ()
	{
		
		// not terribly reliable but it's good enough unless you're determined to have a bad time
		IsModProjectEnvironment = GodotProjectName != "Rikki Kuu Game";
		
		// for modding purposes, it's best to assume the same conditions as the actual game
		NonStandardProjectPath = GodotProjectDirectory != "RikkiKuuGame";
		
	}
	
}