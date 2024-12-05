/*
Utility Runtime, Debug & System Toolkit.
*/

namespace RikkiKuu;

static partial class Utility
{
	
	// find the resource path for a godotobject-inherited type
	public static string? GetScriptPathForType (Type type)
	{
		foreach (var attribute in System.Attribute.GetCustomAttributes(type))
			if (attribute is ScriptPathAttribute script_path)
				return script_path.Path;
		return null;
	}
	
	
	// another alias if you prefer a templated type
	public static string? GetTypeScriptPath<T> ()
	where T : GodotObject
	=> GetScriptPathForType( typeof(T) );
	
	
	// one more alias if you prefer to borrow an instance type
	public static string? GetVarScriptPath<T> (ref readonly T _)
	where T : GodotObject
	=> GetScriptPathForType( typeof(T) );
	
	
	// self-restart style exit, with an optional list of command line arguments to override the previous list if supplied
	public static void RestartGame (string[]? new_cmdline_args = null)
	{
		OS.SetRestartOnExit( true, new_cmdline_args ?? OS.GetCmdlineArgs() );
		Scene.Quit();
	}
	
	
	// self-documenting, run-time-available, mostly convenient bool Assert( () => (bool) cond )
	public static bool Assert (Expression<Func<bool>> condition)
	{
		if (condition.Compile().Invoke()) return true;
		Bail( condition.ToString() );
		return false;
	}
	
	
	// compiler-friendly assert
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Assert ([DoesNotReturnIf(false)] bool condition, string? comment = null)
	{
		if (!condition) Bail( comment );
	}
	
	
	// log an error and quit as gracefully as possible.
	[DoesNotReturn]
	public static void Bail (string? comment = null)
	{
		// print error message & call stack
		GD.PrintErr( DateTime.Now.ToString() );
		if (comment is not null) GD.PrintErr( comment );
		GD.PrintErr( System.Environment.StackTrace );
		
		// try quit using godot
		Scene?.Quit();
		
		// godot might not quit until after this entire tick is done
		System.Environment.Exit(1);
	}
	
	
	// boilerplate to bootstrap a godot-sdk-linked assembly given a custom loading function
	internal static Assembly? LoadGodotProjectAssembly (Func<AssemblyLoadContext,Assembly?> loader)
	{
		// locate godot's c# api assembly
		Assembly gs_assembly = typeof( Godot.Bridge.ScriptManagerBridge ).Assembly;
		
		// obtain godot's loading context, so that its attributes are taken into account when loading new assemblies
		if (AssemblyLoadContext.GetLoadContext( gs_assembly ) is not AssemblyLoadContext gs_loadcontext) {
			GD.PushError( "GodotSharp AssemblyLoadContext missing" );
			return null;
		}
		
		// hand off the actual assembly load to client code, but give them godot's loading context
		if (loader.Invoke( gs_loadcontext ) is not Assembly loaded_assembly) {
			GD.PushError( "failed to load assembly" );
			return null;
		}
		
		// have godot discover script resources in this assembly, now that attributes like ScriptPath have also loaded
		Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly( loaded_assembly );
		
		// as a courtesy, return the loaded assembly reference
		return loaded_assembly;
	}
	
}