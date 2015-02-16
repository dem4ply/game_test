using UnityEngine;
using System.Collections;
using Controller.Motor;

namespace Controller {
	namespace Controller {
		public class Controller_base : MonoBehaviour {

			public float dead_zone_joystick = 0.1f;
			
			protected Transform _transform;
			protected Motor_base _motor;
			protected Joystick _joystick;

			protected void Awake() {
				_init_cache();
			}

			protected void Update() {
				if ( Camera.main == null ) {
					Debug.LogWarning( "Camera.main es null el controllor no hara nada hasta que se asigne una Camera.main" );
					return;
				}
				_get_inputs();
				_motor.update_motor();
			}

			/// <summary>
			/// funcio que optinee los vectores de joystick
			/// </summary>
			protected void _get_inputs() {
				_motor.move_vector = Vector3.zero;
				_joystick.update_all();
				if ( _joystick.axis_esdf.z > dead_zone_joystick || _joystick.axis_esdf.z < -dead_zone_joystick )
					_motor.move_vector += new Vector3( 0, 0, _joystick.axis_esdf.z );
				if ( _joystick.axis_esdf.x > dead_zone_joystick || _joystick.axis_esdf.x < -dead_zone_joystick )
					_motor.move_vector += new Vector3( _joystick.axis_esdf.x, 0, 0 );
			}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected virtual void _init_cache() {
				_transform = transform;
				_motor = GetComponent<Motor_base>();
				_joystick = new Joystick();
			}
		}
	}
}

