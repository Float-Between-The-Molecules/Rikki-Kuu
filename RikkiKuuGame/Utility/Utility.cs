/*
Utility Class.
suggestive usage -> using static Utility;
*/

namespace RikkiKuu;

public static partial class Utility
{
	
	// project name as reported by godot
	public static readonly string GodotProjectName;
	
	
	// user directory name as reported by godot
	public static readonly string GodotProjectDirectory;
	
	
	// was the godot editor detected
	public static readonly bool GodotEditorFound;
	
	
	// static initializer runs at first usage of this class, which should be some bootstrapper in principle
	static Utility ()
	{
		// best place to put a randomize call
		GD.Randomize();
		
		// locate the scene tree at this time
		Scene = FindSceneTree();
		Assert( Scene is not null );
		
		// collect data about godot environment
		GodotProjectName = ProjectSettings.GetSetting(@"application/config/name").AsString();
		GodotProjectDirectory = ProjectSettings.GetSetting(@"application/config/custom_user_dir_name").AsString();
		GodotEditorFound = Engine.IsEditorHint() || OS.HasFeature("editor");
	}
	
}