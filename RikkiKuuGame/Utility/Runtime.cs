/*
Utility Runtime, Debug & System Toolkit.
*/

namespace RikkiKuu;

static partial class Utility
{
	
	// todo: load dll function, required for modding
	
	
	// steam binaries are not compatible with debug builds, and other uses
	public static bool IsDebugAvailable ()
	{
		[Conditional("DEBUG")]
		static void _checker (ref bool c)
		=> c = true;
		var check = false;
		_checker(ref check);
		return check;
	}
	
	
	
	// todo: self-restart style exit
	
	
	// log an error and quit as gracefully as possible.
	[DoesNotReturn] // C# compiler does not seem to respect NotReturn attributes except for tracking throws & nullable conditions
	public static void Bail (string comment = null)
	{
		// print error message & call stack
		GD.PrintErr( DateTime.Now.ToString() );
		if (comment is not null) GD.PrintErr( comment );
		GD.PrintErr( System.Environment.StackTrace );
		// try quit using godot
		Scene?.Quit();
		// either way try to quit as a console application since godot might not quit until after this entire tick is done
		System.Environment.Exit(1);
	}
	
	// self-documenting, run-time-available, mostly convenient bool Assert( () => (bool) cond )
	public static bool Assert (Expression<Func<bool>> condition)
	{
		if (condition.Compile().Invoke()) return true;
		Bail( condition.ToString() );
		return false;
	}
	
}