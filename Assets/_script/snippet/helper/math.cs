using UnityEngine;

namespace helper {
	public static class math {
		public static float clamp_angle( float angle, float min, float max ) {
			do {
				if ( angle > 360f ) {
					angle -= 360;
				}
				else if ( angle < -360 )
					angle += 360;
			} while ( angle > 360f || angle < -360f );
			return Mathf.Clamp(angle, min, max);
		}
	}
}
