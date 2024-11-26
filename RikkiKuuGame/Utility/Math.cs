/*
Utility Math Library.

in a lot of cases godot may provide the exact function but the cross-scripting overheads may not be worth it.
*/

namespace RikkiKuu;

static partial class Utility
{
	
	// godot's posmod function with high potential for inlining
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int PosMod (int p_x, int p_y)
	{
		int val = p_x % p_y;
		if ((val < 0 && p_y > 0) || (val > 0 && p_y < 0)) val += p_y;
		return val;
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int FastFloorToInt (float x)
	{
		var i = (int) x;
		return i <= x ? i : i - 1;
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int FastCeilToInt (float x)
	{
		var i = (int) x;
		return i < x ? i + 1 : i;
	}
	
	
	// rectify an angle to [-180,180)
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Rectify180 (float angle)
	=> Mathf.PosMod( angle + 180f, 360f ) - 180f;
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Min<T> (T a, T b)
	where T : IComparable<T>
	=> a.CompareTo(b) < 0 ? a : b;
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Max<T> (T a, T b)
	where T : IComparable<T>
	=> a.CompareTo(b) > 0 ? a : b;
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Clamp<T> (T v, T l, T u)
	where T : IComparable<T>
	=> Min( Max(v, l), u );
	
	
	
	public static float TetraVolume (in Vector3 v1, in Vector3 v2, in Vector3 v3, in Vector3 v4)
	=> Mathf.Abs( (v1 - v4).Dot((v2 - v4).Cross(v3 - v4)) / 6f );
	
	
	
	// returns trace( transpose(a) * b ) assuming pure rotation matrices
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float BasisVectorizedProduct (in Basis a, in Basis b)
	=> a.X.Dot(b.X) + a.Y.Dot(b.Y) + a.Z.Dot(b.Z); // trace( transpose(a) * b ) = sum_ij[ a_ij * b_ij ]
	
	// returns cos(angle) separating one basis from another
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float BasisCosine (in Basis a, in Basis b)
	=> BasisVectorizedProduct(in a, in b) * .5f - .5f; // trace( transpose(a) * b ) = 1 + 2 cos( theta )
	
	
	
	/*
	// c# doesn't allow adding operators to existing classes.
	// but c# does allow new methods. operator overloads are technically static methods that have
	// special associations by the compiler, they're not "just any" method as they're tied to the
	// usage of operator tokens in code. in principle so i thought.
	// this is absurd.
	
	// basis * float -> basis
	public static Basis operator * (this Basis b, float f)
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	=> b.Scaled( new Vector3( f, f, f ) );
	
	// float * basis -> basis
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Basis operator * (this float f, Basis b)
	=> b.Scaled( new Vector3( f, f, f ) );
	
	*/
	
	
	// also absurd that this is more efficient than Basis.FromScale(Vector3.One * scale)
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Basis ScaledBasis (float scale)
	=> new( scale, 0f, 0f, 0f, scale, 0f, 0f, 0f, scale );
	
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void UpdateVec3Linear (ref Vector3 current, in Vector3 target, float factor)
	=> current = current.MoveToward( target, factor );
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void UpdateVec3Exp (ref Vector3 current, in Vector3 target, float factor)
	=> current = current.Lerp( target, factor );
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void UpdateBasisLinear (ref Basis current, in Basis target, float factor)
	{
		var angle_fraction = .5f - .5f * BasisCosine( current, target );
		if (angle_fraction > 0f) {
			var f = Mathf.Clamp( factor / angle_fraction, 0f, 1f );
			current = current.Slerp( target, f );
		}
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void UpdateBasisExp (ref Basis current, in Basis target, float factor)
	=> current = current.Slerp( target, factor );
	
	
	// this is how far we are in-bewteen physics updates, to move things between two janky positions smoothly
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float LerpFrac ()
	=> (float) Engine.GetPhysicsInterpolationFraction();
	
}