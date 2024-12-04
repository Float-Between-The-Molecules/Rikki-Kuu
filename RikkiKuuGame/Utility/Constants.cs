/*
Utility Constants.
*/

namespace RikkiKuu;

static partial class Utility
{
	
	public const float Log2 = 0.69314718055994530941723212145817656807550013436025f;
	
	public const float Root2 = 1.4142135623730950488016887242097f;
	public const float Root3 = 1.7320508075688772935274463415059f;
	public const float Root5 = 2.2360679774997896964091736687313f;
	public const float OneThird = 1f / 3f;
	
	public const float deg2rad = Mathf.Pi / 180f;
	
	public const float HalfPi = Mathf.Pi / 2f;
	public const float QuartPi = Mathf.Pi / 4f;
	
	
	// geometry thresholds
	public const float CMP_EPSILON = 1f / 65536f; // 0.00001f;
	public const float CMP_EPSILON2 = CMP_EPSILON * CMP_EPSILON;
	public const float UNIT_EPSILON = 1f / 1024f; // 0.001f;
	
	
	// surprisingly non-trivial Null-RID value
	public static readonly Rid NullRID = new(null!);
	
}