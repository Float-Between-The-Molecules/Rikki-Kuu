/*
Rikki Kuu Framework

Refer to documentation for details.
*/

namespace RikkiKuu;

using static Utility;

public static partial class Framework
{
	
	// project name as reported by godot, not the most reliable property to lean on
	public static readonly string GodotProjectName;
	
	
	// user directory name as reported by godot, not the most reliable property to lean on
	public static readonly string GodotProjectDirectory;
	
	
	// is this a case of "mod loads game"?
	public static readonly bool IsModProjectEnvironment;
	
	
	// is this being loaded with the godot editor?
	public static readonly bool IsGodotEditorEnvironment;
	
	
	// is the user path that of the game or somewhere else?
	public static readonly bool IsUserPathWrong;
	
	
	#pragma warning disable CA2255
	[ModuleInitializer, Conditional("DEBUG")]
	internal static void _StaticInitializerHook ()
	{
		// godot runtime takes issue with module initializers; this is purely for the benefit of the editor
		if (!IsGodotEditorEnvironment) return;
		
		// editor environment will have problems if it tries to look for content in another pack or classes in another dll
		GD.PushWarning( "Godot Editor Detected - Avoid instantiating non-local content" );
		
		// _it knows_
		if (IsModProjectEnvironment) {
			GD.PushWarning( "Rikki Kuu Modding Detected - Good Fun, Have Luck" );
		}
		
		// a wrong user dir means it won't access the same settings as the game
		if (IsUserPathWrong) {
			GD.PushWarning( "Project custom user dir is not set up for live game settings to be available" );
		}
	}
	#pragma warning restore CA2255
	
	
	// effectively runs just before any symbol in this module is used, including, strangely, the module initializer
	static Framework ()
	{
		// collect data from environment
		GodotProjectName = ProjectSettings.GetSetting(@"application/config/name").AsString();
		GodotProjectDirectory = ProjectSettings.GetSetting(@"application/config/custom_user_dir_name").AsString();
		IsGodotEditorEnvironment = Engine.IsEditorHint() || OS.HasFeature("editor");
		
		// extrapolate other properties; not terribly reliable but it's good enough unless you're determined to have a bad time
		IsUserPathWrong = GodotProjectDirectory != "RikkiKuuGame";
		IsModProjectEnvironment = GodotProjectName != "Rikki Kuu Game";
	}
	
}