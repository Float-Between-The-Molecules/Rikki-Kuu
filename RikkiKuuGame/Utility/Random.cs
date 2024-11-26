/*
Utility Random Functions.
*/

namespace RikkiKuu;

static partial class Utility
{
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float RandRange (float lower, float upper)
	=> GD.Randf() * (upper - lower) + lower;
	
	
	// RandValue(0.8, 100) is the same as RandRange(20, 100)
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float RandValue (float randomness, float upper)
	=> (GD.Randf() * randomness - randomness + 1f) * upper;
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3 RandomUnitSphere ()
	{
		var (x, y, x2y2) = (1f, 1f, 2f);
		do {
			(x, y) = (RandRange(-1f, 1f), RandRange(-1f, 1f));
			x2y2 = x*x + y*y;
		} while (x2y2 >= 1f);
		var s1x2y2 = Mathf.Sqrt( 1f - x2y2 );
		return new Vector3( 2f * x * s1x2y2, 2f * y * s1x2y2, 1f - 2f * x2y2 );
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Basis RandomOrientation ()
	=> new( RandomUnitSphere(), GD.Randf() * Mathf.Pi );
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 RandomUnitCircle ()
	=> Vector2.Up.Rotated( GD.Randf() * Mathf.Tau );
	
	
	public static Vector3 RandomUnitCircle3D ()
	{
		var v2 = RandomUnitCircle();
		return new( v2.X, v2.Y, 0f );
	}
	
	
	public static void InPlaceShuffle<T> (List<T> list)
	{
		for (int n = list?.Count ?? 0; n --> 1;) {
			int i = GD.RandRange( 0, n );
			if (i < n) (list[i], list[n]) = (list[n], list[i]);
		}
	}
	
}

