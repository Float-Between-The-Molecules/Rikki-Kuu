/*
Utility Signal Functions.
*/

namespace RikkiKuu;

static partial class Utility
{
	
	
	// wait for & get results of a signal on an object. this cannot be canceled, that's on user code to avoid
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Variant[]> OnSignal (this GodotObject target, StringName signal)
	=> await target.ToSignal( target, signal );
	
	
	public static async Task Wait (float duration)
	=> await Scene.CreateTimer(duration).OnSignal(SceneTreeTimer.SignalName.Timeout);
	
	////////////////////////////////////////////////////////////////
	
	
	// wait until before the next process frame
	public static async Task<float> NextProcessFrame ()
	=> await Scene.OnSignal( SceneTree.SignalName.ProcessFrame ) switch {
		Variant[] args when args.Length > 0 => args[0].AsSingle(),
		_ => 0f
	};
	
	
	// wait until before the next physics frame
	public static async Task<float> NextPhysicsFrame ()
	=> await Scene.OnSignal( SceneTree.SignalName.PhysicsFrame ) switch {
		Variant[] args when args.Length > 0 => args[0].AsSingle(),
		_ => 0f
	};
	
	
}